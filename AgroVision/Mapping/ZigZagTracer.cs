using AgroVision.Mapping.Distances;
using AgroVision.Utils;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace AgroVision.Mapping
{
    public class ZigZagTracer
    {

        private readonly Font font;
        private readonly Pen firstPoint;
        private readonly Pen secondPoint;
        public ZigZagTracer()
        {
            font = new Font("Segoe UI", 14, FontStyle.Bold);

            AdjustableArrowCap arrow = new AdjustableArrowCap(4, 4);
            firstPoint = new Pen(Color.FromArgb(64, 64, 64), 3)
            {
                CustomEndCap = arrow
            };
            secondPoint = new Pen(Color.White, 3)
            {
                DashStyle = DashStyle.Dash
            };
        }

        private readonly Color[] PathColors ={
            Color.Cyan,
            Color.Orange,
            Color.Magenta,
            Color.Purple
        };

        public PathPlanning TracePaths(GoogleMapsView view, float spacing, float angle, float offset, Polygon polygon, Graphics graphics)
        {
            PathPlanning plain = new PathPlanning();
            if (polygon.Points.Length > 0)
            {
                try
                {
                    Euclidean euclidean = new Euclidean();

                    //Converter
                    PointF[] points = polygon.Points.Select(p => view.CoordToPoint(p.ToCoord()).ToPoint()).ToArray();

                    //Definir margem
                    //points = PolygonUtil.Inflate(points, (float)view.MetersToPixels(offset));
                    //points = PolygonUtil.OffsetPolygon(points, offset);
                    points = PolygonUtil.NewInflatePolygon(points, (float)view.MetersToPixels(offset));

                    float minX = points.Min(point => point.X);
                    float maxX = points.Max(point => point.X);
                    float minY = points.Min(point => point.Y);
                    float maxY = points.Max(point => point.Y);

                    RectangleF rect = new RectangleF(minX, minY, maxX - minX, maxY - minY);

                    //Polygon c_p = new Polygon(pol.Points.Select(p => view.CoordToPoint(p.ToCoord())).ToArray());
                    Polygon coordinatedPolygon = new Polygon(points.Select(p => p.ToNPoint()).ToArray());

                    NPoint center = PolygonUtil.GetCentroid(coordinatedPolygon);

                    NPoint orign = new NPoint(0, 0);

                    if (angle == 90)
                        angle = 90.1f;

                    //É rotacionado
                    orign = PolygonUtil.RotatePointAt(orign, center, angle);

                    //1) Adquirir intercessões e ordenar segmentos
                    float maxSize = Math.Max(rect.Height, rect.Width) * 1.3f;

                    //Converter
                    spacing = (float)view.MetersToPixels(spacing);

                    int maxSegmentCount = (int)(maxSize / spacing);
                    maxSegmentCount += 2;

                    List<NPoint> tempPoints = new List<NPoint>();
                    for (int segmentIndex = 0; segmentIndex <= maxSegmentCount; segmentIndex++)
                    {
                        //Pontos da linha que traça
                        NPoint firstPoint = new NPoint(center[0] - (maxSize / 1.5), minY + (segmentIndex * spacing));
                        NPoint secondPoint = new NPoint(center[0] + (maxSize / 1.5), minY + (segmentIndex * spacing));

                        double top = maxSize / 6;
                        firstPoint[1] -= top;
                        secondPoint[1] -= top;

                        firstPoint = PolygonUtil.RotatePointAt(firstPoint, center, angle);
                        secondPoint = PolygonUtil.RotatePointAt(secondPoint, center, angle);

                        Segment tempSegment = new Segment();

                        for (int pointIndex = 0; pointIndex < coordinatedPolygon.Points.Length; pointIndex++)
                        {
                            NPoint a = coordinatedPolygon.Points[pointIndex];
                            NPoint b = pointIndex < coordinatedPolygon.Points.Length - 1 ? coordinatedPolygon.Points[pointIndex + 1] : coordinatedPolygon.Points[0];

                            //Se ele cruzar a uma das linhas
                            if (PolygonUtil.LineCrossLine(firstPoint, secondPoint, a, b))
                            {
                                NPoint crossPoint = PolygonUtil.GetCrossPoint(firstPoint, secondPoint, a, b);
                                tempSegment.AddPoint(crossPoint);
                            }
                        }

                        //Ordenar
                        tempSegment.OrderDistanceByOrign(orign, euclidean);

                        for (int tempPointIndex = 0; tempPointIndex < tempSegment.Points.Count; tempPointIndex++)
                        {
                            tempPoints.Add(tempSegment.Points[tempPointIndex]);

                            DrawPoint(8, tempSegment.Points[tempPointIndex], Color.Blue, graphics);
                        }
                    }

                    //Talvez remover onde a distância dos pontos é muito curta

                    //ZigZag
                    while (true)
                    {
                        MissionPath path = new MissionPath();
                        SolidBrush color = new SolidBrush(PathColors[plain.Paths.Count]);

                        int index = 0;
                        NPoint lastPoint = orign;
                        for (int tempPointIndex = 0; tempPointIndex < tempPoints.Count; tempPointIndex++)
                        {
                            if (index >= tempPoints.Count)
                                break;

                            NPoint nextPoint = tempPoints[index];

                            //Pontos reais
                            NPoint realFirstPointIndex = PolygonUtil.RotatePointAt(lastPoint, center, angle * -1);
                            NPoint realSecondPointIndex = PolygonUtil.RotatePointAt(nextPoint, center, angle * -1);

                            //Há quebra?
                            //É na mesma altura e estava descendo
                            if (Math.Abs(realFirstPointIndex[1] - realSecondPointIndex[1]) < 1e-8 && ((step + 1) % 2 != 0))
                            {
                                index += 2;

                                if (index >= tempPoints.Count)
                                    break;

                                nextPoint = tempPoints[index];
                            }

                            //Criar cópia
                            path.AddPoint(new NPoint(nextPoint.Values));

                            //Definir próximo
                            index = GetNextIndex(index);

                            //Salvar último ponto para comparação
                            lastPoint = nextPoint;
                        }

                        for (int tempPointIndex = 0; tempPointIndex < tempPoints.Count; tempPointIndex++)
                            if (path.Points.Contains(tempPoints[tempPointIndex]))
                            {
                                tempPoints.RemoveAt(tempPointIndex);
                                tempPointIndex--;
                            }

                        //Visualizar
                        if (offset != 0) //Margem
                            graphics.DrawPolygon(secondPoint, points);

                        //Pontos
                        for (int pathPointIndex = 0; pathPointIndex < path.Points.Count - 1; pathPointIndex++)
                        {
                            PointF firstPoint = path.Points[pathPointIndex].ToPoint();
                            PointF secondPoint = path.Points[pathPointIndex + 1].ToPoint();

                            graphics.DrawLine(pathPointIndex % 2 == 0 ? new Pen(color, 3) : this.firstPoint, firstPoint, secondPoint);
                        }

                        plain.Paths.Add(path);

                        //Fim
                        if (tempPoints.Count == 0)
                            break;

                        //Zerar os passos
                        ResetStep();
                    }

                    //Ponto de início e fim
                    if (plain.Paths.Count > 0)
                    {
                        MissionPath firstPath = plain.Paths[0];
                        MissionPath lastPath = plain.Paths[plain.Paths.Count - 1];

                        NPoint first = firstPath.Points[0];
                        NPoint last = lastPath.Points[lastPath.Points.Count - 1];

                        DrawPoint(50, first, Color.White, graphics);
                        DrawText(50, "I", font, first, Color.Black, graphics);
                        DrawPoint(50, last, Color.White, graphics);
                        DrawText(50, "F", font, last, Color.Black, graphics);
                    }
                }
                catch //(Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
                }

                //Converter para coordenada de novo
                for (int pathIndex = 0; pathIndex < plain.Paths.Count; pathIndex++)
                    for (int pointIndex = 0; pointIndex < plain.Paths[pathIndex].Points.Count; pointIndex++)
                        plain.Paths[pathIndex].Points[pointIndex] = view.PointToCoord(plain.Paths[pathIndex].Points[pointIndex]);
            }

            return plain;
        }

        private int step = 0;
        private int GetNextIndex(int index)
        {
            int result = index;
            switch (step)
            {
                case 0:
                    result += 1;
                    break;
                //Descer
                case 1:
                    result += 2;
                    break;
                case 2:
                    result -= 1;
                    break;
                //Descer
                case 3:
                    result += 2;
                    break;
            }

            step++;
            if (step > 3)
                step = 0;

            return result;
        }

        private void ResetStep()
        {
            step = 0;
        }

        public static int GetRotationFactor(GoogleMapsView view, PathPlanning plain, GeoTag tag)
        {
            NPoint geoPosition = view.CoordToPoint(new PointLatLng(tag.Latitude, tag.Longitude));

            //Sequência de rotações
            int[] upRotates = { 2, 3, 0, 3 };
            int[] downRotates = { 0, 1, 2, 1 };

            int firstRotate = -1;
            int lastRotate = -1;

            Euclidean euc = new Euclidean();

            double minDistance = double.MaxValue;
            int rotation = 0;
            for (int pathIndex = 0; pathIndex < plain.Paths.Count; pathIndex++)
                for (int comparePathIndex = 0; comparePathIndex < plain.Paths[pathIndex].Points.Count - 1; comparePathIndex++)
                {
                    NPoint firstPoint = plain.Paths[pathIndex].Points[comparePathIndex];
                    NPoint secondPoint = plain.Paths[pathIndex].Points[comparePathIndex + 1];

                    PointF firstTransformedPoint = view.CoordToPoint(firstPoint.ToCoord()).ToPoint();
                    PointF secondTransformedPoint = view.CoordToPoint(secondPoint.ToCoord()).ToPoint();

                    NPoint half = new NPoint((firstTransformedPoint.X + secondTransformedPoint.X) / 2, (firstTransformedPoint.Y + secondTransformedPoint.Y) / 2);

                    switch (firstRotate)
                    {
                        case 0:
                            int downRotateIndex = Array.IndexOf(downRotates, lastRotate);
                            downRotateIndex++;
                            if (downRotateIndex > 3)
                                downRotateIndex = 0;

                            lastRotate = downRotates[downRotateIndex];
                            break;
                        case 2:
                            int upRotateIndex = Array.IndexOf(upRotates, lastRotate);
                            upRotateIndex++;
                            if (upRotateIndex > 3)
                                upRotateIndex = 0;

                            lastRotate = upRotates[upRotateIndex];
                            break;
                    }

                    //Definir primeira rotação
                    if (firstRotate == -1)
                    {
                        if (secondTransformedPoint.Y > firstTransformedPoint.Y || secondTransformedPoint.X > firstTransformedPoint.X)
                            firstRotate = lastRotate = 0;
                        else if (secondTransformedPoint.Y < firstTransformedPoint.Y || secondTransformedPoint.X < firstTransformedPoint.X)
                            firstRotate = lastRotate = 2;
                    }

                    //Calcular distância para cada imagem
                    double distance = euc.Calculate(geoPosition, half);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        rotation = lastRotate;
                    }
                }

            //if (first_rotate == 0)
            //    MessageBox.Show($"Descendo, girando {rotation}");
            //if (first_rotate == 2)
            //    MessageBox.Show($"Subindo, girando {rotation}");

            return rotation;
        }

        private void DrawPoint(int point_size, NPoint n, Color color, Graphics graphics)
        {
            PointF point = n.ToPoint();
            graphics.FillEllipse(new SolidBrush(color), new RectangleF(point.X - (point_size / 2), point.Y - (point_size / 2), point_size, point_size));
        }

        private void DrawText(int point_size, string text, Font f, NPoint n, Color color, Graphics g)
        {
            PointF point = n.ToPoint();
            TextRenderer.DrawText(g, text, f, new Rectangle((int)(point.X - (point_size / 2)) + 2, (int)(point.Y - (point_size / 2)), point_size, point_size), color, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

    }
}
using AgroVision.Mapping;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows;

namespace AgroVision.Utils
{
    public static class PolygonUtil
    {

        public static NPoint ToNPoint(this System.Drawing.Point point)
        {
            return new NPoint(point.X, point.Y);
        }

        public static NPoint ToNPoint(this PointF point)
        {
            return new NPoint(point.X, point.Y);
        }

        public static PointF ToPoint(this NPoint point)
        {
            return new PointF((float)point.Values[0], (float)point.Values[1]);
        }

        public static bool PolygonContains(NPoint point, NPoint[] points)
        {
            double minX = points[0][0];//x
            double maxX = points[0][0];//x
            double minY = points[0][1];//y
            double maxY = points[0][1];//y
            for (int i = 1; i < points.Length; i++)
            {
                NPoint q = points[i];
                minX = Math.Min(q[0], minX);
                maxX = Math.Max(q[0], maxX);
                minY = Math.Min(q[1], minY);
                maxY = Math.Max(q[1], maxY);
            }

            if (point[0] < minX || point[0] > maxX || point[1] < minY || point[1] > maxY)
                return false;

            bool isInside = false;
            for (int pointIndex = 0, j = points.Length - 1; pointIndex < points.Length; j = pointIndex++)
                if ((points[pointIndex][1] > point[1]) != (points[j][1] > point[1]) && point[0] < (points[j][0] - points[pointIndex][0]) * (point[1] - points[pointIndex][1]) / (points[j][1] - points[pointIndex][1]) + points[pointIndex][0])
                    isInside = !isInside;

            return isInside;
        }

        //Verifica se um determinado ponto se encontra entre uma linha
        public static bool OnSegment(NPoint segmentFirtPoint, NPoint segmentSecondPoint, NPoint point)
        {
            if (segmentSecondPoint[0] <= Math.Max(segmentFirtPoint[0], point[0]) && segmentSecondPoint[0] >= Math.Min(segmentFirtPoint[0], point[0]) &&
                segmentSecondPoint[1] <= Math.Max(segmentFirtPoint[1], point[1]) && segmentSecondPoint[1] >= Math.Min(segmentFirtPoint[1], point[1]))
                return true;

            return false;
        }

        //Adquire a orientação entre os pontos
        public static int GetOritentation(NPoint p, NPoint q, NPoint r)
        {
            int v = (int)((q[1] - p[1]) * (r[0] - q[0]) -
                          (q[0] - p[0]) * (r[1] - q[1]));

            if (v == 0) return 0; //Colinear

            return v > 0 ? 1 : 2; //Horário ou anti-horário
        }

        //Verifica se uma linha cruza outra
        public static bool LineCrossLine(NPoint p1, NPoint p2, NPoint p3, NPoint p4)
        {
            int o1 = GetOritentation(p1, p2, p3);
            int o2 = GetOritentation(p1, p2, p4);
            int o3 = GetOritentation(p3, p4, p1);
            int o4 = GetOritentation(p3, p4, p2);

            if (o1 != o2 && o3 != o4)
                return true;

            if (o1 == 0 && OnSegment(p1, p3, p2)) return true;
            if (o2 == 0 && OnSegment(p1, p4, p2)) return true;
            if (o3 == 0 && OnSegment(p3, p1, p4)) return true;
            if (o4 == 0 && OnSegment(p3, p2, p4)) return true;

            return false;
        }

        public static NPoint GetCrossPoint(NPoint p1, NPoint p2, NPoint p3, NPoint p4)
        {
            double dy1 = p2[1] - p1[1];
            double dx1 = p2[0] - p1[0];
            double dy2 = p4[1] - p3[1];
            double dx2 = p4[0] - p3[0];

            //Verificar se as linhas não são paralelas
            if (dy1 * dx2 == dy2 * dx1)
                return NPoint.Empty;
            else
            {
                double x = ((p3[1] - p1[1]) * dx1 * dx2 + dy1 * dx2 * p1[0] - dy2 * dx1 * p3[0]) / (dy1 * dx2 - dy2 * dx1);
                double y = p1[1] + (dy1 / dx1) * (x - p1[0]);

                return new NPoint(x, y);
            }
        }

        public static NPoint RotatePointAt(NPoint point, NPoint center, double angle)
        {
            double radians = angle * (Math.PI / 180);
            double cosTheta = Math.Cos(radians);
            double sinTheta = Math.Sin(radians);
            return new NPoint(
                cosTheta * (point[0] - center[0]) - sinTheta * (point[1] - center[1]) + center[0],
                sinTheta * (point[0] - center[0]) + cosTheta * (point[1] - center[1]) + center[1]);
        }

        public static NPoint GetCentroid(Polygon poly)
        {
            // Add the first point at the end of the array.
            int num_points = poly.Points.Length;
            PointF[] pts = new PointF[num_points + 1];
            poly.Points.Select(p => p.ToPoint()).ToArray().CopyTo(pts, 0);
            pts[num_points] = poly.Points[0].ToPoint();

            // Find the centroid.
            double X = 0;
            double Y = 0;
            double second_factor;
            for (int i = 0; i < num_points; i++)
            {
                second_factor = pts[i].X * pts[i + 1].Y - pts[i + 1].X * pts[i].Y;

                X += (pts[i].X + pts[i + 1].X) * second_factor;
                Y += (pts[i].Y + pts[i + 1].Y) * second_factor;
            }

            // Divide by 6 times the polygon's area.
            double polygon_area = GetPolygonArea(poly);
            X /= (6 * polygon_area);
            Y /= (6 * polygon_area);

            // If the values are negative, the polygon is
            // oriented counterclockwise so reverse the signs.
            if (X < 0)
            {
                X = -X;
                Y = -Y;
            }

            return new NPoint(X, Y);
        }

        // Return the polygon's area in "square units."
        public static double GetPolygonArea(Polygon pol)
        {
            // Return the absolute value of the signed area.
            // The signed area is negative if the polyogn is
            // oriented clockwise.
            return Math.Abs(SignedPolygonArea(pol));
        }

        private static double SignedPolygonArea(Polygon pol)
        {
            // Add the first point to the end.
            int num_points = pol.Points.Length;
            PointF[] pts = new PointF[num_points + 1];
            pol.Points.Select(p => p.ToPoint()).ToArray().CopyTo(pts, 0);
            pts[num_points] = pol.Points[0].ToPoint();

            // Get the areas.
            double area = 0;
            for (int i = 0; i < num_points; i++)
                area += (pts[i + 1].X - pts[i].X) * (pts[i + 1].Y + pts[i].Y) / 2;

            // Return the result.
            return area;
        }

        public static NPoint[] GetBezier(NPoint[] points, int count)
        {
            NPoint[] result = new NPoint[count + 1];
            for (int i = 0; i <= count; i++)
            {
                double t = (double)i / count;
                result[i] = GetBezierPoint(t, points, 0, points.Length);
            }

            return result;
        }

        private static NPoint GetBezierPoint(double t, NPoint[] points, int index, int count)
        {
            if (count == 1)
                return points[index];

            NPoint p0 = GetBezierPoint(t, points, index, count - 1);
            NPoint P1 = GetBezierPoint(t, points, index + 1, count - 1);

            return new NPoint((1 - t) * p0[0] + t * P1[0], (1 - t) * p0[1] + t * P1[1]);
        }

        // Make an array containing Bezier curve points and control points.
        //public static NPoint[] MakeCurvePoints(NPoint[] points, double tension)
        //{
        //    if (points.Length < 2) return null;
        //    double control_scale = tension / 0.5 * 0.175;

        //    // Make a list containing the points and
        //    // appropriate control points.
        //    List<NPoint> result_points = new List<NPoint>();
        //    result_points.Add(points[0]);

        //    for (int i = 0; i < points.Length - 1; i++)
        //    {
        //        // Get the point and its neighbors.
        //        NPoint pt_before = points[Math.Max(i - 1, 0)];
        //        NPoint pt = points[i];
        //        NPoint pt_after = points[i + 1];
        //        NPoint pt_after2 = points[Math.Min(i + 2, points.Length - 1)];

        //        double dx1 = pt_after[0] - pt_before[0];
        //        double dy1 = pt_after[1] - pt_before[1];

        //        NPoint p1 = points[i];
        //        NPoint p4 = pt_after;

        //        double dx = pt_after[0] - pt_before[0];
        //        double dy = pt_after[1] - pt_before[1];
        //        NPoint p2 = new NPoint(
        //            pt[0] + control_scale * dx,
        //            pt[1] + control_scale * dy);

        //        dx = pt_after2[0] - pt[0];
        //        dy = pt_after2[1] - pt[1];
        //        NPoint p3 = new NPoint(
        //            pt_after[0] - control_scale * dx,
        //            pt_after[1] - control_scale * dy);

        //        // Save points p2, p3, and p4.
        //        result_points.Add(p2);
        //        result_points.Add(p3);
        //        result_points.Add(p4);
        //    }

        //    // Return the points.
        //    return result_points.ToArray();
        //}

        public static PointF[] OffsetPolygon(PointF[] old_points, float offset)
        {
            int num_points = old_points.Length; //grab the number of points we will be iterating over (perf measure)
            PointF[] adjusted_points = new PointF[num_points]; //create an array to hold the adjusted points

            for (int j = 0; j < num_points; j++) //loop through each point
            {
                //find the points before and after our target point.
                int i = (j - 1);
                if (i < 0)
                {
                    i += num_points;
                }

                int k = (j + 1) % num_points;

                //the next step is to push out each point based on the position of its surrounding points and then
                //figure out the intersections of the pushed out points
                PointF pij1, pij2, pjk1, pjk2;

                PointF v1 = new PointF(old_points[j].X - old_points[i].X, old_points[j].Y - old_points[i].Y);
                //v1.Normalize();
                v1.X *= offset;
                v1.Y *= offset;

                PointF n1 = new PointF(-v1.Y, v1.X);

                pij1 = new PointF(old_points[i].X + n1.X, old_points[i].Y + n1.Y);
                pij2 = new PointF(old_points[j].X + n1.X, old_points[j].Y + n1.Y);

                PointF v2 = new PointF(old_points[k].X - old_points[j].X, old_points[k].Y - old_points[j].Y);
                //v2.Normalize();
                v2.X *= offset;
                v2.Y *= offset;

                PointF n2 = new PointF(-v2.Y, v2.X);
                pjk1 = new PointF(old_points[j].X + n2.X, old_points[j].Y + n2.Y);
                pjk2 = new PointF(old_points[k].X + n2.X, old_points[k].Y + n2.Y);

                //see where the shifted lines ij and jk intersect using an infinite line intersection test (not a line segment intersection test)
                PointF intersection_point = GetCrossPoint(
                    pij1.ToNPoint(),
                    pij2.ToNPoint(),
                    pjk1.ToNPoint(),
                    pjk2.ToNPoint()).ToPoint(); //MeshLight_Utils.InfiniteLineIntersection(pij1, pij2, pjk1, pjk2);

                //add the intersection as our adjusted vert point
                adjusted_points[i] = new PointF(intersection_point.X, intersection_point.Y);
            }

            return adjusted_points;
        }

        public static PointF[] Inflate(PointF[] polygon, float width)
        {
            //Euclidean euclidean = new Euclidean();
            //NPoint center = GetCentroid(new Polygon(polygon.Select(p => p.ToNPoint()).ToArray()));
            //polygon = polygon.OrderBy(p => p.ToNPoint(), new ClockWiseComparer(center)).ToArray();

            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(polygon);

            if (width < 0)
            {
                width *= -1;
                path.Reverse();
            }

            using (var p = new GraphicsPath())
            {
                p.AddPath(path, false);
                p.CloseAllFigures();
                p.Widen(new Pen(Color.Black, width * 2));

                var position = 0;
                var result = new GraphicsPath();
                while (position < p.PointCount)
                {
                    // skip outer edge
                    position += CountNextFigure(p.PathData, position);

                    // count inner edge
                    var figureCount = CountNextFigure(p.PathData, position);

                    var points = new PointF[figureCount];
                    var types = new byte[figureCount];

                    Array.Copy(p.PathPoints, position, points, 0, figureCount);
                    Array.Copy(p.PathTypes, position, types, 0, figureCount);

                    position += figureCount;

                    result.AddPath(new GraphicsPath(points, types), false);
                }

                path.Reset();
                path.AddPath(result, false);

                return path.PathPoints;
            }
        }

        private static int CountNextFigure(PathData data, int position)
        {
            int count = 0;
            for (var typeIndex = position; typeIndex < data.Types.Length; typeIndex++)
            {
                count++;
                if (0 != (data.Types[typeIndex] & (int)PathPointType.CloseSubpath))
                    return count;
            }

            return count;
        }

        public class ClockWiseComparer : IComparer<NPoint>
        {

            private readonly NPoint reference;
            public ClockWiseComparer(NPoint reference)
            {
                this.reference = reference;
            }

            public int Compare(NPoint a, NPoint b)
            {
                if (a[0] == b[0] && a[1] == b[1])
                    return 0;

                NPoint firstOffset = new NPoint(a[0] - reference[0], a[1] - reference[1]);
                NPoint secondOffset = new NPoint(b[0] - reference[0], b[1] - reference[1]);

                double angle1 = Math.Atan2(firstOffset[0], firstOffset[1]);
                double angle2 = Math.Atan2(secondOffset[0], secondOffset[1]);

                if (angle1 < angle2)
                    return -1;

                if (angle1 > angle2)
                    return 1;

                return 0;
            }

        }

        public static PointF[] NewInflatePolygon(PointF[] points, float offset)
        {
            if (PolygonIsOrientedClockwise(points.ToList()))
            {
                List<PointF> pts = new List<PointF>();
                for (int pointIndex = points.Length - 1; pointIndex >= 0; pointIndex--)
                    pts.Add(points[pointIndex]);
                
                points = pts.ToArray();
            }

            return GetEnlargedPolygon(points.ToList(), offset).ToArray();
        }

        //
        // Return points representing an enlarged polygon.
        private static List<PointF> GetEnlargedPolygon(List<PointF> old_points, float offset)
        {
            List<PointF> enlarged_points = new List<PointF>();
            int num_points = old_points.Count;
            for (int j = 0; j < num_points; j++)
            {
                // Find the new location for point j.
                // Find the points before and after j.
                int i = (j - 1);
                if (i < 0) i += num_points;
                int k = (j + 1) % num_points;

                // Move the points by the offset.
                Vector v1 = new Vector(
                    old_points[j].X - old_points[i].X,
                    old_points[j].Y - old_points[i].Y);
                v1.Normalize();
                v1 *= offset;
                Vector n1 = new Vector(-v1.Y, v1.X);

                PointF pij1 = new PointF(
                    (float)(old_points[i].X + n1.X),
                    (float)(old_points[i].Y + n1.Y));
                PointF pij2 = new PointF(
                    (float)(old_points[j].X + n1.X),
                    (float)(old_points[j].Y + n1.Y));

                Vector v2 = new Vector(
                    old_points[k].X - old_points[j].X,
                    old_points[k].Y - old_points[j].Y);
                v2.Normalize();
                v2 *= offset;
                Vector n2 = new Vector(-v2.Y, v2.X);

                PointF pjk1 = new PointF(
                    (float)(old_points[j].X + n2.X),
                    (float)(old_points[j].Y + n2.Y));
                PointF pjk2 = new PointF(
                    (float)(old_points[k].X + n2.X),
                    (float)(old_points[k].Y + n2.Y));

                // See where the shifted lines ij and jk intersect.
                bool lines_intersect, segments_intersect;
                PointF poi, close1, close2;
                FindIntersection(pij1, pij2, pjk1, pjk2,
                    out lines_intersect, out segments_intersect,
                    out poi, out close1, out close2);
                if (lines_intersect)
                    enlarged_points.Add(poi);
            }

            return enlarged_points;
        }

        // Find the point of intersection between
        // the lines p1 --> p2 and p3 --> p4.
        private static void FindIntersection(
            PointF p1, PointF p2, PointF p3, PointF p4,
            out bool lines_intersect, out bool segments_intersect,
            out PointF intersection,
            out PointF close_p1, out PointF close_p2)
        {
            // Get the segments' parameters.
            float dx12 = p2.X - p1.X;
            float dy12 = p2.Y - p1.Y;
            float dx34 = p4.X - p3.X;
            float dy34 = p4.Y - p3.Y;

            // Solve for t1 and t2
            float denominator = (dy12 * dx34 - dx12 * dy34);
            bool lines_parallel = (Math.Abs(denominator) < 0.001);

            float t1 =
                ((p1.X - p3.X) * dy34 + (p3.Y - p1.Y) * dx34)
                    / denominator;
            if (float.IsNaN(t1) || float.IsInfinity(t1))
                lines_parallel = true;

            if (lines_parallel)
            {
                // The lines are parallel (or close enough to it).
                lines_intersect = false;
                segments_intersect = false;
                intersection = new PointF(float.NaN, float.NaN);
                close_p1 = new PointF(float.NaN, float.NaN);
                close_p2 = new PointF(float.NaN, float.NaN);
                return;
            }
            lines_intersect = true;

            float t2 =
                ((p3.X - p1.X) * dy12 + (p1.Y - p3.Y) * dx12)
                    / -denominator;

            // Find the point of intersection.
            intersection = new PointF(p1.X + dx12 * t1, p1.Y + dy12 * t1);

            // The segments intersect if t1 and t2 are between 0 and 1.
            segments_intersect =
                ((t1 >= 0) && (t1 <= 1) &&
                 (t2 >= 0) && (t2 <= 1));

            // Find the closest points on the segments.
            if (t1 < 0)
            {
                t1 = 0;
            }
            else if (t1 > 1)
            {
                t1 = 1;
            }

            if (t2 < 0)
            {
                t2 = 0;
            }
            else if (t2 > 1)
            {
                t2 = 1;
            }

            close_p1 = new PointF(p1.X + dx12 * t1, p1.Y + dy12 * t1);
            close_p2 = new PointF(p3.X + dx34 * t2, p3.Y + dy34 * t2);
        }

        // Return true if the polygon is oriented clockwise.
        public static bool PolygonIsOrientedClockwise(List<PointF> points)
        {
            return (SignedPolygonArea(points) < 0);
        }

        // Return the polygon's area in "square units."
        // The value will be negative if the polygon is
        // oriented clockwise.
        private static float SignedPolygonArea(List<PointF> points)
        {
            // Add the first point to the end.
            int num_points = points.Count;
            PointF[] pts = new PointF[num_points + 1];
            points.CopyTo(pts, 0);
            pts[num_points] = points[0];

            // Get the areas.
            float area = 0;
            for (int i = 0; i < num_points; i++)
            {
                area +=
                    (pts[i + 1].X - pts[i].X) *
                    (pts[i + 1].Y + pts[i].Y) / 2;
            }

            // Return the result.
            return area;
        }

    }
}

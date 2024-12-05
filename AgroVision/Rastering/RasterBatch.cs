using AgroVision.Mapping;
using AgroVision.Rastering;
using AgroVision.Utils;
using DHS.Imaging;
using DHS.Tasking;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace AgroVision
{
    public class RasterBatch
    {

        #region Propriedades
        public float FlyHeight { get; set; }

        public float Angle { get; set; }

        public Polygon Polygon { get; set; }

        public Bitmap Thumbnail { get; set; }

        public List<RasterImage> Images { get; set; }

        public ColorBlender Blender { get; set; }

        public string Equation { get; set; }

        public double Min { get; set; }

        public double Max { get; set; }

        public bool EnableClip { get; set; }
        #endregion

        #region Inicializar
        public event Action SettingUpImages;
        public event Action RenderingStart;
        public event Action RenderCompleted;

        public RasterBatch()
        {
            Images = new List<RasterImage>();
            EnableClip = true;
        }
        #endregion

        #region Eventos
        private int lastNoteIndex;
        private ToolTip tip;
        private void OnMouseMove_mapsView(object sender, MouseEventArgs e)
        {
            for (int imageIndex = 0; imageIndex < Images.Count; imageIndex++)
            {
                RasterImage image = Images[imageIndex];
                if (image.Rectangle.Contains(e.Location) && lastNoteIndex != imageIndex)
                {
                    if (tip != null)
                        tip.Dispose();

                    tip = new ToolTip
                    {
                        IsBalloon = true
                    };

                    Point point = e.Location;
                    point.X -= 15;
                    point.Y -= 45;

                    tip.Show($"Mapeamento de {image.GPSPosition.Date.ToShortDateString()}", mapsView, point);

                    lastNoteIndex = imageIndex;
                    break;
                }
                else if (!image.Rectangle.Contains(e.Location) && lastNoteIndex == imageIndex)
                {
                    lastNoteIndex = -1;

                    if (tip != null)
                        tip.Dispose();
                }
            }
        }

        void OnDrawMap_mapsView(object sender, PaintEventArgs e)
        {
            AddClip(e.Graphics);

            DrawImages(e.Graphics);

            RestoreClip(e.Graphics);

            SaveThumb();
        }
        #endregion

        #region Métodos
        public void AddBatchImage(RasterImageData imageData)
        {
            SettingUpImages?.Invoke();

            if (File.Exists(imageData.Path))
            {
                RasterImage img = new RasterImage();
                img.InitImage(imageData.Path);
                img.Rotation = imageData.Rotation;
                img.IsVisible = imageData.IsVisible;

                Images.Add(img);
            }
            else
                MessageBox.Show($"O arquivo '{imageData.Path}' não está mais disponível.");
        }

        public void AddImage(string imagePath)
        {
            SettingUpImages?.Invoke();

            if (File.Exists(imagePath))
            {
                RasterImage rasterImage = new RasterImage();
                rasterImage.InitImage(imagePath);
                Images.Add(rasterImage);
            }
            else
                MessageBox.Show($"O arquivo '{imagePath}' não está mais disponível.");
        }

        private GoogleMapsView mapsView;
        public void RegisterEvents(GoogleMapsView mapsView)
        {
            this.mapsView = mapsView;

            if (mapsView != null)
            {
                mapsView.OnDrawMap += OnDrawMap_mapsView;
                mapsView.MouseMove += OnMouseMove_mapsView;
            }
        }

        public void UnregisterEvents()
        {
            if (mapsView != null)
            {
                mapsView.OnDrawMap -= OnDrawMap_mapsView;
                mapsView.MouseMove -= OnMouseMove_mapsView;
            }
        }

        public void UpdateEquation(string equation, Action onEndCalculate)
        {
            Equation = equation;

            RenderingStart?.Invoke();

            AsyncWorker worker = new AsyncWorker();
            worker.DoWork += delegate
            {
                Min = double.MaxValue;
                Max = double.MinValue;

                //Processar
                for (int imageIndex = 0; imageIndex < Images.Count; imageIndex++)
                {
                    RasterImage rasterImage = Images[imageIndex];
                    rasterImage.LoadAndProcess(equation);

                    //Achar valores mínimos e máximos
                    if (rasterImage.MinValue < Min)
                        Min = rasterImage.MinValue;

                    if (rasterImage.MaxValue > Max)
                        Max = rasterImage.MaxValue;
                }
            };
            worker.OnWorkerCompleted += delegate
            {
                RenderCompleted?.Invoke();

                onEndCalculate?.Invoke();
            };
            worker.Start();
        }

        public void UpdateColors(Action onEndColoring)
        {
            RenderingStart?.Invoke();

            AsyncWorker worker = new AsyncWorker();
            worker.DoWork += delegate
            {
                //Desenhar imagem
                for (int imageIndex = 0; imageIndex < Images.Count; imageIndex++)
                {
                    RasterImage rasterImage = Images[imageIndex];

                    if (rasterImage.GPSPosition != null)
                    {
                        //img.ColorizeAndSaveImage(Blender);
                        rasterImage.ColorizeAndSaveImage(Blender, Min, Max);
                    }
                };
            };
            worker.OnWorkerCompleted += delegate
            {
                RenderCompleted?.Invoke();

                onEndColoring?.Invoke();
            };
            worker.Start();
        }

        public void RemoveImage(RasterImage image)
        {
            Images.Remove(image);

            UpdateEquation(Equation, delegate
            {
                UpdateColors(null);
            });
        }

        public void Clear()
        {
            //Limpar a memória
            for (int imageIndex = 0; imageIndex < Images.Count; imageIndex++)
                Images[imageIndex].OriginalImage?.Recycle();

            Images.Clear();
        }

        private void AddClip(Graphics graphics)
        {
            if (Polygon != null && EnableClip)
                if (Polygon.Points.Length > 0)
                {
                    List<PointF> drawedPolygonPoints = new List<PointF>();
                    foreach (NPoint point in Polygon.Points)
                        drawedPolygonPoints.Add(mapsView.CoordToPoint(point.ToCoord()).ToPoint());

                    GraphicsPath path = new GraphicsPath();
                    path.AddPolygon(drawedPolygonPoints.ToArray());

                    graphics.Clip = new Region(path);
                }
        }

        private void DrawImages(Graphics graphics)
        {
            for (int imageIndex = 0; imageIndex < Images.Count; imageIndex++)
            {
                RasterImage rasterImage = Images[imageIndex];
                if (!rasterImage.IsVisible) continue;

                GeoTag geoTag = rasterImage.GPSPosition;

                //Precisa ter a localização
                if (geoTag == null)
                    continue;

                NPoint firstPoint = mapsView.CoordToPoint(new PointLatLng(geoTag.Latitude, geoTag.Longitude));

                NSize size = GetRealSize(FlyHeight, mapsView);

                RectangleF rect = new RectangleF((float)(firstPoint[0] - (size[0] / 2)), (float)(firstPoint[1] - (size[1] / 2)), (float)size[0], (float)size[1]);

                rasterImage.Rectangle = rect;

                rasterImage.Center = new PointF((float)firstPoint[0], (float)firstPoint[1]);

                GraphicsState state = graphics.Save();

                //Girar
                Matrix matrix = new Matrix();
                float angle = Angle + 90 + (rasterImage.Rotation * 90);
                matrix.RotateAt(angle, rasterImage.Center);

                graphics.Transform = matrix;

                graphics.DrawImage(Properties.Resources.loading, new RectangleF((float)firstPoint[0] - 12, (float)firstPoint[1] - 12, 24, 24));

                if (rasterImage.Source != null)
                    graphics.DrawImage(rasterImage.Source, rect);

                graphics.Restore(state);
            }
        }

        private void SaveThumb()
        {
            //Salvar miniatura
            if (saveThumb)
            {
                float left = float.MaxValue;
                float top = float.MaxValue;
                float right = float.MinValue;
                float bottom = float.MinValue;

                //Calcular retângulo mínimo
                //for (int i = 0; i < Images.Count; i++)
                //{
                //    RectangleF rect = Images[i].Rectangle;

                //    if (rect.X < left)
                //        left = rect.X;

                //    if (rect.Y < top)
                //        top = rect.Top;

                //    if (rect.X + rect.Width > right)
                //        right = rect.X + rect.Width;

                //    if (rect.Y + rect.Height > bottom)
                //        bottom = rect.Y + rect.Height;
                //}

                //Calcular retângulo mínimo
                for (int polygonIndex = 0; polygonIndex < Polygon.Points.Length; polygonIndex++)
                {
                    PointF point = mapsView.CoordToPoint(Polygon.Points[polygonIndex].ToCoord()).ToPoint();

                    if (point.X < left)
                        left = point.X;

                    if (point.Y < top)
                        top = point.Y;

                    if (point.X > right)
                        right = point.X;

                    if (point.Y > bottom)
                        bottom = point.Y;
                }

                int width = (int)(right - left);
                int height = (int)(bottom - top);

                //int s_w = w / 3;
                //int s_h = h / 3;

                int horizontalMargin = 10;
                int verticalMargin = 10;

                if (width >= 1 && height >= 1)
                {
                    left -= horizontalMargin;
                    top -= verticalMargin;

                    Bitmap thumb = new Bitmap(width + (horizontalMargin * 2), height + (verticalMargin * 2));

                    using (Graphics graphicsPath = Graphics.FromImage(thumb))
                    {
                        //Tirar a rebarba
                        if (Polygon != null && EnableClip)
                        {
                            List<PointF> drawedPolygonPoints = new List<PointF>();
                            foreach (NPoint point in Polygon.Points)
                            {
                                PointF drawPoint = mapsView.CoordToPoint(point.ToCoord()).ToPoint();
                                drawedPolygonPoints.Add(new PointF(drawPoint.X - left, drawPoint.Y - top));
                            }

                            GraphicsPath path = new GraphicsPath();
                            path.AddPolygon(drawedPolygonPoints.ToArray());
                            graphicsPath.Clip = new Region(path);
                        }

                        //Desenhar imagem
                        for (int imageIndex = 0; imageIndex < Images.Count; imageIndex++)
                        {
                            RasterImage rasterImage = Images[imageIndex];
                            if (!rasterImage.IsVisible) continue;

                            RectangleF rect = rasterImage.Rectangle;

                            GraphicsState state = graphicsPath.Save();

                            //Girar
                            Matrix matrix = new Matrix();
                            float angle = Angle + 90 + (rasterImage.Rotation * 90);
                            matrix.RotateAt(angle, new PointF(rasterImage.Center.X - left, rasterImage.Center.Y - top));

                            graphicsPath.Transform = matrix;

                            if (rasterImage.Source != null)
                                graphicsPath.DrawImage(rasterImage.Source, new RectangleF(rect.X - left, rect.Y - top, rect.Width, rect.Height));

                            graphicsPath.Restore(state);
                        }

                        graphicsPath.ResetClip();
                    }

                    Thumbnail = thumb;
                }

                saveThumb = false;
            }
        }

        private void RestoreClip(Graphics g)
        {
            if (EnableClip)
                g.ResetClip();
        }

        private bool saveThumb;
        public void CreateThumb()
        {
            double lastZoom = mapsView.Zoom;
            mapsView.Zoom = 20;
            mapsView.Enabled = false;

            saveThumb = true;
            mapsView.Refresh();

            mapsView.Zoom = lastZoom;
            mapsView.Enabled = true;
        }

        public NSize GetRealSize(double flyHeight, GoogleMapsView view)
        {
            flyHeight += 29; //Deslocamento
            double widthMeters = view.MetersToPixels(.5 * flyHeight); //4000px
            double heightMeters = view.MetersToPixels(.375 * flyHeight); //3000px

            return new NSize(widthMeters, heightMeters);
        }
        #endregion

    }
}
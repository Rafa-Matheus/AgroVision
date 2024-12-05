using AgroVision.Utils;
using DHS.Imaging;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace AgroVision
{
    public class RasterImage
    {

        #region Proprieadades
        public string TempName { get; set; }

        public string ImagePath { get; set; }

        public Image Source { get; set; }

        public FastBitmap OriginalImage { get; set; }

        public GeoTag GPSPosition { get; set; }

        public RectangleF Rectangle { get; set; }

        public PointF Center { get; set; }

        public double MinValue { get; set; }

        public double MaxValue { get; set; }

        public double[] Values { get; set; }

        public bool IsVisible { get; set; }

        public int Rotation { get; set; }
        #endregion

        #region Inicializar
        private readonly float downFactor;
        public RasterImage()
        {
            downFactor = 10;

            IsVisible = true;
        }
        #endregion

        #region Inicializar imagem
        public void InitImage(string imagePath)
        {
            ImagePath = imagePath;

            GeoTag geo = new GeoTag(ImagePath);
            if (geo.Latitude != double.NaN)
                GPSPosition = geo;

            Random random = new Random();
            DateTime date = DateTime.Now;

            TempName = $"temp_{date.Day}_{date.Month}_{date.Year}_{random.Next(0, 1000)}.png";
        }
        #endregion

        #region Processar
        public void LoadAndProcess(string equation)
        {
            LoadImage();

            MinValue = double.MaxValue;
            MaxValue = double.MinValue;

            //Reduzir tamanho
            int newWidth = (int)(OriginalImage.Width / downFactor);
            int newHeight = (int)(OriginalImage.Height / downFactor);

            Values = new double[newWidth * newHeight];

            for (int y = 0; y < newHeight; y++)
                for (int x = 0; x < newWidth; x++)
                {
                    double indexValue = RasterUtil.Compute(equation, OriginalImage.GetPixel((int)(x * downFactor), (int)(y * downFactor)));
                    if (double.IsNaN(indexValue))
                    {
                        MessageBox.Show("A equação inserida não é válida.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    //Valores mínimo e máximo
                    if (indexValue < MinValue)
                        MinValue = indexValue;

                    if (indexValue > MaxValue)
                        MaxValue = indexValue;

                    Values[GetIndex(x, y, newWidth)] = indexValue;
                }

            TempUtil.CreateTempDirectoryIfNotExists();
            TempUtil.DeleteTempFile(TempName);

            OriginalImage.Recycle();
        }
        #endregion

        #region Carregar imagem
        private void LoadImage()
        {
            if (!File.Exists(ImagePath))
            {
                MessageBox.Show($"Desculpe a imagem no caminho '{ImagePath}' não está mais disponível.", "Imagem Ausente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Stream stream = new FileStream(ImagePath, FileMode.Open, FileAccess.Read, FileShare.Read);

            string ext = Path.GetExtension(ImagePath).ToLower();
            if (ext.Equals(".tiff") || ext.Equals(".tif"))
            {
                TiffBitmapDecoder decoder = new TiffBitmapDecoder(stream, BitmapCreateOptions.None, BitmapCacheOption.Default); //Deve estar como BitmapCreateOptions.None

                //Imagem (Onde há o carregamento de memória)
                if (decoder.Frames.Count > 0)
                    OriginalImage = new FastBitmap(decoder.Frames[0]);

                stream.Close();
            }
            else if (ext.Equals(".jpg"))
                OriginalImage = new FastBitmap(new Bitmap(stream));
        }
        #endregion

        #region Colorir e salvar
        public void ColorizeAndSaveImage(ColorBlender blender, double minValue, double maxValue)
        {
            int w = (int)(OriginalImage.Width / downFactor);
            int h = (int)(OriginalImage.Height / downFactor);

            FastBitmap result = new FastBitmap(new Bitmap(w, h));
            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                {
                    double scaledColorValue = Scale((float)Values[GetIndex(x, y, w)], minValue, maxValue, 0, 1);

                    result.SetPixel(x, y, GetMostNearColor(scaledColorValue, blender));
                }

            //Efeito na imagem
            Blur blur = new Blur(result, 3);
            result = blur.Process();

            //Erode erode = new Erode(result, blender.GetColor(.5f), 3);
            //result = erode.Process();

            string tempImagePath = TempUtil.GetTempFilePath(TempName);

            result.ToBitmap().Save(tempImagePath, ImageFormat.Png);

            FileStream stream = null;
            Source = null;

            try
            {
                stream = new FileStream(tempImagePath, FileMode.Open, FileAccess.Read);
                Source = Image.FromStream(stream);
            }
            finally
            {
                stream.Close();
            }
        }
        #endregion

        #region Métodos
        private static BitmapColor GetMostNearColor(double value, ColorBlender blender)
        {
            double minDistance = float.MaxValue;
            int index = 0;
            for (int colorIndex = 0; colorIndex < blender.Colors.Count; colorIndex++)
            {
                double distance = Math.Abs(blender.Positions[colorIndex] - value);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    index = colorIndex;
                }
            }

            Color color = blender.Colors[index];

            return new BitmapColor(color.A, color.R, color.G, color.B);
        }

        public int GetIndex(int x, int y, int width)
        {
            return (width * y) + x;
        }

        public static double Scale(double value, double oldMin, double oldMax, double newMin, double newMax)
        {
            double oldRange = oldMax - oldMin;
            double newRange = newMax - newMin;
            return ((value - oldMin) * newRange / oldRange) + newMin;
        }
        #endregion

    }
}
using DHS.Imaging;
using System.Collections.Generic;
using System.Drawing;

namespace AgroVision.Forms
{
    public class MetricsHelper
    {

        public MetricsHelper()
        {
            Ranges = new List<RangeValue>();
        }

        public List<RangeValue> Ranges { get; set; }

        public int Total { get; set; }

        public void Count(Bitmap thumb, ColorBlender blender)
        {
            if (thumb == null)
                return;

            //Zerar todos os valores
            for (int rangeIndex = 0; rangeIndex < Ranges.Count; rangeIndex++)
                Ranges[rangeIndex].Count = 0;

            FastBitmap bmp = new FastBitmap(thumb);

            //Contar valores
            int total = 0;
            for (int y = 0; y < bmp.Height; y++)
                for (int x = 0; x < bmp.Width; x++)
                {
                    BitmapColor color = bmp.GetPixel(x, y);
                    if (color.A == 255)
                    {
                        double pixelScaledColor = RasterImage.Scale(blender.GetPosition(color), 0, 1, -1, 1);

                        //Para cada intervalo
                        for (int rangeIndex = 0; rangeIndex < Ranges.Count; rangeIndex++)
                        {
                            RangeValue range = Ranges[rangeIndex];

                            if (pixelScaledColor >= range.MinValue && pixelScaledColor <= range.MaxValue)
                            {
                                range.Count++;
                                break;
                            }
                        }

                        total++;
                    }
                }

            Total = total;
        }

        //private float GetMostNearPosition(float value, ColorBlender blender)
        //{
        //    float min_d = float.MaxValue;
        //    int index = 0;
        //    for (int i = 0; i < blender.Positions.Count; i++)
        //    {
        //        float d = Math.Abs(blender.Positions[i] - value);
        //        if (d < min_d)
        //        {
        //            min_d = d;
        //            index = i;
        //        }
        //    }

        //    return blender.Positions[index];
        //}

    }
}

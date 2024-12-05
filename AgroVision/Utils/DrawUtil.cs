using System.Drawing;

namespace AgroVision.Utils
{
    public static class DrawUtil
    {

        public static Color ChangeBrightness(Color color, int brightness)
        {
            int r = color.R + brightness;
            int g = color.G + brightness;
            int b = color.B + brightness;

            r = r > 255 ? 255 : r < 0 ? 0 : r;
            g = g > 255 ? 255 : g < 0 ? 0 : g;
            b = b > 255 ? 255 : b < 0 ? 0 : b;

            return Color.FromArgb(color.A, r, g, b);
        }

    }
}

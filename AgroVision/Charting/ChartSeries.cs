using DHS.Imaging;
using System.Drawing;

namespace AgroVision
{
    public class ChartSeries
    {

        public ChartSeries(string title, double value, Color firstColor, Color secondColor)
        {
            Title = title;
            Value = value;
            FirstColor = firstColor;
            SecondColor = secondColor;

            Gradient = new ColorBlender();
            Gradient.AddColor(firstColor, 0);
            Gradient.AddColor(secondColor, 1);
            Gradient.Blend();
        }

        public string Title { get; set; }

        public double Value { get; set; }

        public Color FirstColor { get; set; }

        public Color SecondColor { get; set; }

        public ColorBlender Gradient { get; set; }

        public override string ToString()
        {
            return Title;
        }

    }
}
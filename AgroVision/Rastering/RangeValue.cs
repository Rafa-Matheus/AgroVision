namespace AgroVision
{
    public class RangeValue
    {

        public RangeValue(string title, double minValue, double maxValue)
        {
            Title = title;
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public string Title { get; set; }

        public double MinValue { get; set; }

        public double MaxValue { get; set; }
        public int Count { get; set; }

        public override string ToString()
        {
            return $"{Title} - ({MinValue}-{MaxValue})";
        }

    }
}
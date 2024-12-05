namespace AgroVision.Mapping
{
    public struct NSize
    {

        public static readonly NSize Empty = new NSize();

        public NSize(params double[] values)
        {
            Values = values;
        }

        public double[] Values;

        public double this[int index]
        {
            get { return Values[index]; }
            set { Values[index] = value; }
        }

    }
}
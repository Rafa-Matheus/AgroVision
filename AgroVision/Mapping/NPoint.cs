using System;

namespace AgroVision.Mapping
{
    [Serializable]
    public class NPoint
    {

        public static NPoint Empty = new NPoint();

        public NPoint(params double[] values)
        {
            Values = values;
        }

        public double[] Values { get; set; }

        public double this[int index]
        {
            get { return Values[index]; }
            set { Values[index] = value; }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            NPoint point = (NPoint)obj;
            for (int valueIndex = 0; valueIndex < Math.Min(point.Values.Length, Values.Length); valueIndex++)
                if (Math.Abs(point[valueIndex] - this[valueIndex]) > 1e-8)
                    return false;

            return true;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override string ToString()
        {
            return $"{{ {string.Join(", ", Values)} }}";
        }

    }
}
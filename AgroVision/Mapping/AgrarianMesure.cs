namespace AgroVision
{
    public class AgrarianMesure
    {

        public AgrarianMesure(double factor, string unit, string plural)
        {
            Factor = factor;
            Unit = unit;
            Plural = plural;
        }

        public double Factor { get; set; }

        public string Unit { get; set; }

        public string Plural { get; set; }

        public string Format(double meters)
        {
            return $"{meters / Factor:N2} {Unit}{(meters > 1 ? Plural : "")}";
        }

    }
}
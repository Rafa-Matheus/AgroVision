using AgroVision.Utils;
using System;

namespace AgroVision.Mapping.Distances
{
    public class Haversine : IDistance
    {

        public double Calculate(params NPoint[] points)
        {
            double sum = 0;

            for (int pointIndex = 0; pointIndex < points.Length - 1; pointIndex++)
                sum += MeasureLatLngInMeters(points[pointIndex], points[pointIndex + 1]);

            return sum;
        }

        public double MeasureLatLngInMeters(NPoint p1, NPoint p2)
        {  
            double dLat = p2[0] * Math.PI / 180 - p1[0] * Math.PI / 180;
            double dLng = p2[1] * Math.PI / 180 - p1[1] * Math.PI / 180;

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(p1[0] * Math.PI / 180) * Math.Cos(p2[0] * Math.PI / 180) * Math.Sin(dLng / 2) * Math.Sin(dLng / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = MetricsUtil.EARTH_RADIUS_IN_METERS * c;

            return d; //Metros
        }

    }
}
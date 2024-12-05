using AgroVision.Mapping;
using AgroVision.Mapping.Distances;
using GMap.NET;
using System;

namespace AgroVision.Utils
{
    public static class MetricsUtil
    {

        public const double EARTH_RADIUS_IN_METERS = 6378137;

        //public static double Div(double a, double b)
        //{
        //    return b > 0 ? a / (double)b : 0;
        //}

        public static PointLatLng ToCoord(this NPoint point)
        {
            return new PointLatLng(point[0], point[1]);
        }

        //Adquiri distância total a ser percorrida
        public static double GetTotalDistance(PathPlanning plain, IDistance distance)
        {
            double totalDistance = 0;
            for (int pathIndex = 0; pathIndex < plain.Paths.Count; pathIndex++)
            {
                MissionPath path = plain.Paths[pathIndex];
                for (int pointIndex = 0; pointIndex < path.Points.Count - 1; pointIndex++)
                {
                    NPoint firstPoint = path.Points[pointIndex];
                    NPoint secondPoint = path.Points[pointIndex + 1];

                    totalDistance += distance.Calculate(firstPoint, secondPoint);
                }
            }

            return totalDistance;
        }

        //Adquirir tempo estimado de chegada (Retorna em segundos)
        public static int GetEstimatedTimeArrival(double distance, double speedInMs)
        {
            return (int)(distance / speedInMs);
        }

        //Adquirir a quantidade de baterias necessárias para o voo
        public static int GetRequiredBatteriesCount(int eta, int maxSecondsPerBattery)
        {
            double count = eta / maxSecondsPerBattery;

            //Não existe meia bateria
            if (eta % maxSecondsPerBattery != 0)
                count++;

            return (int)Math.Max(count, 1);
        }

        //Formatar para o tempo
        public static string FormatSecondsToTimeString(int totalSeconds)
        {
            double hours = totalSeconds / 3600;
            double minutes = (totalSeconds % 3600) / 60;
            double seconds = (totalSeconds % 3600) % 60;

            return $"{hours:00}:{minutes:00}:{seconds:00}s";
        }

        //Adquirir o tamanho da amostra do solo (Ground Sample Distance) centímetros/pixel
        public static double GetGSD(int imageWidth, double sensorWidthInMm, double focalLengthInMm, double heightInMeters)
        {
            return (sensorWidthInMm * heightInMeters * 100) / (focalLengthInMm * imageWidth);
        }

        //Adquirir tamanho de um único ponto no solo (Em metros)
        public static NSize GetSingleGSDPointInMeters(int imageWidth, int imageHeight, double sensorWidthInMm, double focalLengthInMm, double heightInMeters)
        {
            double gsd = GetGSD(imageWidth, sensorWidthInMm, focalLengthInMm, heightInMeters);
            return new NSize((gsd * imageWidth) / 100, (gsd * imageHeight) / 100);
        }

        //Adquirir área pelas Latitudes e Longitudes

        private static double ToRadians(double angle)
        {
            return angle / 180.0 * Math.PI;
        }

        public static double GetArea(NPoint[] path)
        {
            return ComputeSignedArea(path, EARTH_RADIUS_IN_METERS);
        }

        private static double ComputeSignedArea(NPoint[] path, double radius)
        {
            int size = path.Length;

            if (size < 3) { return 0; }

            double total = 0;
            var prev = path[size - 1];
            double prevTanLat = Math.Tan((Math.PI / 2 - ToRadians(prev[0])) / 2);
            double prevLng = ToRadians(prev[1]);

            foreach (var point in path)
            {
                double tanLat = Math.Tan((Math.PI / 2 - ToRadians(point[0])) / 2);
                double lng = ToRadians(point[1]);
                total += PolarTriangleArea(tanLat, lng, prevTanLat, prevLng);
                prevTanLat = tanLat;
                prevLng = lng;
            }

            return Math.Abs(total * (radius * radius));
        }

        private static double PolarTriangleArea(double tan1, double lng1, double tan2, double lng2)
        {
            double deltaLng = lng1 - lng2;
            double t = tan1 * tan2;
            return 2 * Math.Atan2(t * Math.Sin(deltaLng), 1 + t * Math.Cos(deltaLng));
        }

    }
}
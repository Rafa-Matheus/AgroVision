using System;

namespace AgroVision.Mapping.Distances
{
    public class Euclidean : IDistance
    {

        public double Calculate(params NPoint[] points)
        {
            double sum = 0;

            //Quantidade de dimensões
            double dims = points[0].Values.Length;

            for (int pointIndex = 0; pointIndex < points.Length - 1; pointIndex++)
            {
                NPoint firstPoint = points[pointIndex];
                NPoint secondPoint = points[pointIndex + 1];

                for (int dimensionIndex = 0; dimensionIndex < dims; dimensionIndex++)
                    sum += Math.Pow(firstPoint[dimensionIndex] - secondPoint[dimensionIndex], 2);
            }

            return Math.Sqrt(sum);
        }

    }
}
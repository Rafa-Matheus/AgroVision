using AgroVision.Mapping.Distances;
using System.Linq;

namespace AgroVision.Mapping
{
    public class Segment : NPointList
    {

        //Ordenar distância pela origem
        public void OrderDistanceByOrign(NPoint orign, IDistance distance)
        {
            Points = Points.OrderBy(s => distance.Calculate(orign, s)).ToList();
        }

    }
}
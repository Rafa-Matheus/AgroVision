using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroVision.Mapping
{
    public abstract class NPointList
    {

        public NPointList()
        {
            Points = new List<NPoint>();
        }

        public List<NPoint> Points { get; set; }

        public void AddPoint(NPoint point)
        {
            Points.Add(point);
        }

        public void RemoveAt(int index)
        {
            Points.RemoveAt(index);
        }

    }
}

using System.Collections.Generic;
using System.Linq;

namespace AgroVision.Mapping
{
    public class Polygon
    {

        public Polygon(NPoint[] points)
        {
            Points = points.ToArray();
        }

        public Polygon(List<NPoint> points)
        {
            Points = points.ToArray();
        }

        public NPoint[] Points { get; set; }

        //private Color color = Color.SkyBlue;
        //public Color Color { get { return color; } set { color = value; } }

        //public bool IsSelected { get; set; }

        //private List<NPoint> last_points = new List<NPoint>();
        //public void SavePoints(float x, float y)
        //{
        //    last_points.Clear();
        //    foreach (NPoint p in Points)
        //        last_points.Add(new NPoint(p.X - x, p.Y - y));
        //}

    }
}

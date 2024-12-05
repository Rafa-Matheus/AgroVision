using System.Collections.Generic;

namespace AgroVision.Mapping
{
    public class PathPlanning
    {

        public PathPlanning()
        {
            Paths = new List<MissionPath>();
        }

        public List<MissionPath> Paths { get; set; }

        public void AddPath(MissionPath path)
        {
            Paths.Add(path);
        }

        public void RemoveAt(int index)
        {
            Paths.RemoveAt(index);
        }

    }
}
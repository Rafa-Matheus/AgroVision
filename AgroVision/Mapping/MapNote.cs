using AgroVision.Mapping;
using System;
using System.Drawing;

namespace AgroVision
{
    [Serializable]
    public class MapNote
    {

        public MapNote(string content, NPoint point)
        {
            Content = content;
            Point = point;
        }

        public string Content { get; set; }

        public NPoint Point { get; set; }

        public RectangleF Rectangle { get; set; }

    }
}

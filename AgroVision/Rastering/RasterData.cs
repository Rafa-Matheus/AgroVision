using AgroVision.Rastering;
using DHS.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace AgroVision
{
    [Serializable]
    public class RasterData
    {

        public DateTime Date { get; set; }

        public List<MapNote> Notes { get; set; }

        public string Equation { get; set; }

        public ColorBlender Blender { get; set; }

        public List<RasterImageData> Images;

        public Bitmap Thumbnail { get; set; }

        public double Min { get; set; }

        public double Max { get; set; }

    }
}
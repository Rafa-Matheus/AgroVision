using GMap.NET;
using System;
using System.Drawing;

namespace AgroVision.Views
{
    public class OnMapClickEventArgs : EventArgs
    {
        public PointLatLng PointInMap { get; set; }
        public Point ControlPoint { get; set; }
    }
}
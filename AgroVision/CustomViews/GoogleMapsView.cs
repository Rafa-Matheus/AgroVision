using AgroVision.Mapping;
using AgroVision.Properties;
using AgroVision.Views;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net;
using System.Windows.Forms;

namespace AgroVision
{
    public class GoogleMapsView : GMapControl
    {

        public event EventHandler<OnMapClickEventArgs> OnMapClick;
        public event EventHandler<PaintEventArgs> OnDrawMap;
        public event EventHandler<Keys> OnKeyCmdPress;

        private readonly Image shadow;

        public GoogleMapsView()
        {
            try
            {
                IPHostEntry entry = Dns.GetHostEntry("www.google.com");
            }
            catch
            {
                //Mudar modo para cache
                Manager.Mode = AccessMode.CacheOnly;
                MessageBox.Show("Sem conexão com a internet, entrando em modo de cache.");
            }

            //Configurar mapa
            ShowCenter = false; //Não mostrar cruz de centro
            MarkersEnabled = false; //Desligar marcadores

            DragButton = MouseButtons.Left;
            MapProvider = GMapProviders.GoogleChinaMap;
            //Position = new PointLatLng(54.6961334816182, 25.2985095977783);
            //MinZoom = 20;
            //MaxZoom = 24;
            //Zoom = 10;

            shadow = Resources.shadow;
        }

        public void Goto(double lat, double lng)
        {
            Position = new PointLatLng(lat, lng);
            Zoom = 18;
        }

        public bool LockMove { get; set; }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!LockMove)
                base.OnMouseDown(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (!LockMove)
                base.OnMouseClick(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            OnMapClick?.Invoke(this, new OnMapClickEventArgs() { PointInMap = FromLocalToLatLng(e.X, e.Y), ControlPoint = e.Location });
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            OnDrawMap?.Invoke(this, e);

            //Desenhar sombra
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
            e.Graphics.DrawImage(shadow, new RectangleF(0, 0, shadow.Width, ClientSize.Height));
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            OnKeyCmdPress?.Invoke(this, keyData);

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnMove(EventArgs e)
        {
            base.OnMouseHover(e);
        }

        public NPoint CoordToPoint(PointLatLng coord)
        {
            return ToNPoint(FromLatLngToLocal(coord));
        }

        public NPoint PointToCoord(NPoint point)
        {
            PointLatLng latLng = FromLocalToLatLng((int)point[0], (int)point[1]);
            return new NPoint(latLng.Lat, latLng.Lng);
        }

        private NPoint ToNPoint(GPoint gpoint)
        {
            return new NPoint(gpoint.X, gpoint.Y);
        }

        //Transforma o espaçamento
        public double MetersToPixels(double meters)
        {
            return meters / GetGroundResolution();
        }

        public double PixelsToMeters(double pixels)
        {
            return pixels / MetersToPixels(1);
        }

        private double GetGroundResolution()
        {
            return MapProvider.Projection.GetGroundResolution((int)Zoom, ViewArea.Bottom);
        }

    }

}

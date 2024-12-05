using GMap.NET.MapProviders;
using System;
using System.Windows.Forms;

namespace AgroVision
{
    public partial class MapView : UserControl
    {

        public MapView()
        {
            InitializeComponent();

            mapModeCbx.SelectedIndexChanged += delegate
            {
                switch (mapModeCbx.SelectedIndex)
                {
                    //Normal
                    case 0:
                        googleMapsVw.MapProvider = GMapProviders.GoogleMap;
                        break;
                    //Terreno
                    case 1:
                        googleMapsVw.MapProvider = GMapProviders.GoogleTerrainMap;
                        break;
                    //Satélite
                    case 2:
                        googleMapsVw.MapProvider = GMapProviders.GoogleSatelliteMap;
                        break;
                }
            };
            mapModeCbx.SelectedIndex = 2;

            Load += delegate
            {
                mapModeCbx.Location = new System.Drawing.Point(Width - mapModeCbx.Width - 10, Height - mapModeCbx.Height - 10);
            };
        }

        public GoogleMapsView Map
        {
            get { return googleMapsVw; }
        }

        private void OnClick_searchBtn(object sender, EventArgs e)
        {
            //InputName inputName = new InputName();
            //inputName.Text = "Pesquisar";
            //if (inputName.ShowDialog() == DialogResult.OK)
            //{
            //    CitySearch search = new CitySearch("EnM28TQnyZG4vOG7GQQ5PkrMQA7YQAwZ");
            //    if (search.Search(inputName.Value, "pt-BR", 0, "local", true))
            //    {
            //        googleMapsVw.Position = new PointLatLng(search.Latitude, search.Longitude);
            //        googleMapsVw.Zoom = 12;
            //    }
            //    else
            //        MessageBox.Show("Local não encontrado, ou o número de solicitações foi excedido.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

    }
}

using AgroVision.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AgroVision.CustomViews
{
    public partial class ImageEditView : UserControl
    {

        #region Propriedades
        public string Title { get { return titleLbl.Text; } set { titleLbl.Text = value; } }

        public string GeoLocation { get { return geoLbl.Text; } set { geoLbl.Text = value; } }

        private RasterImage image;
        public RasterImage Image { get { return image; } set { image = value; SetButtonImage(); } }
        #endregion

        public event Action DeleteImage;
        public event Action GoUpImage;
        public event Action GoDownImage;
        public event Action ViewUpdated;

        public ImageEditView()
        {
            InitializeComponent();
            isVisible = true;
        }

        private void OnClick_showHideBtn(object sender, EventArgs e)
        {
            Image.IsVisible = !Image.IsVisible;

            SetButtonImage();
        }

        private bool isVisible;
        private void SetButtonImage()
        {
            isVisible = Image.IsVisible;

            showHideBtn.BackgroundImage = isVisible ? Resources.mostrar : Resources.ocultar;

            ViewUpdated?.Invoke();
        }

        private void OnClick_deleteBtn(object sender, EventArgs e)
        {
            DeleteImage?.Invoke();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            //Borda inferior
            e.Graphics.DrawLine(Pens.Gray, new PointF(0, ClientSize.Height - 1), new PointF(ClientSize.Width - 1, ClientSize.Height - 1));
        }

        private void OnClick_rotateBtn(object sender, EventArgs e)
        {
            Image.Rotation++;
            if (Image.Rotation > 3)
                Image.Rotation = 0;

            ViewUpdated?.Invoke();
        }

        public bool IsVisible()
        {
            return isVisible;
        }

        private void OnClick_upBtn(object sender, EventArgs e)
        {
            GoUpImage?.Invoke();
        }

        private void OnClick_downBtn(object sender, EventArgs e)
        {
            GoDownImage?.Invoke();
        }

    }
}
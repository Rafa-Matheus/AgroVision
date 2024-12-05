using System;
using System.Drawing;
using System.Windows.Forms;

namespace AgroVision.CustomViews
{
    public partial class ListItemView : UserControl
    {

        public event Action OnPressActionButton;
        public event Action OnButtonClick;

        public ListItemView()
        {
            InitializeComponent();
        }

        public Image Icon
        {
            get { return iconPbx.Image; }
            set { iconPbx.Image = value; }
        }

        public string Title
        {
            get { return titleLbl.Text; }
            set { titleLbl.Text = value; }
        }

        public string Description
        {
            get { return descLbl.Text; }
            set { descLbl.Text = value; }
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            //Borda inferior
            e.Graphics.DrawLine(Pens.Gray, new PointF(0, ClientSize.Height - 1), new PointF(ClientSize.Width - 1, ClientSize.Height - 1));
        }

        public bool ActionButtonEnabled
        {
            get { return actionIconPbx.Visible; }
            set { actionIconPbx.Visible = value; }
        }

        private void OnActionClick(object sender, EventArgs e)
        {
            OnPressActionButton?.Invoke();
        }

        private void OnClick(object sender, EventArgs e)
        {
            OnButtonClick?.Invoke();
        }

    }
}

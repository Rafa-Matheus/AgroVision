using System;
using System.Drawing;
using System.Windows.Forms;

namespace AgroVision.Forms
{
    public partial class BoxInfoView : UserControl
    {

        public event Action LeftClick;
        public event Action RightClick;

        public event Action OnPressActionButton;

        public BoxInfoView()
        {
            InitializeComponent();

            Load += delegate
            {
                actionIconPbx.Location = new Point(Width - actionIconPbx.Width - 5, 5);
                leftBtn.Location = new Point(0, Height - leftBtn.Height);
                rightBtn.Location = new Point(Width - rightBtn.Width, Height - rightBtn.Height);
            };
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            TextRenderer.DrawText(e.Graphics, desc, new Font("Segoe UI", 12, FontStyle.Regular), new Rectangle(0, 40, ClientSize.Width, 30), Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

            //Borda inferior
            e.Graphics.DrawLine(Pens.Gray, new PointF(0, ClientSize.Height - 1), new PointF(ClientSize.Width - 1, ClientSize.Height - 1));
        }

        public void AddControl(Control control)
        {
            controlPnl.Visible = true;

            control.Dock = DockStyle.Fill;
            controlPnl.Controls.Add(control);
        }

        public Control GetControl()
        {
            if (controlPnl.Controls.Count > 0)
                return controlPnl.Controls[0];

            return null;
        }

        public string Title
        {
            get { return titleLbl.Text; }
            set { titleLbl.Text = value; }
        }

        public Label TitleControl
        {
            get { return titleLbl; }
        }

        public Image Icon
        {
            get { return iconPbx.Image; }
            set { iconPbx.Image = value; }
        }

        public Image ActionIcon
        {
            get { return actionIconPbx.Image; }
            set
            {
                if ((actionIconPbx.Image = value) != null)
                    actionIconPbx.Visible = true;
            }
        }

        private string desc;
        public string Description
        {
            get { return desc; }
            set { desc = value; Refresh(); }
        }

        public bool SideButtonsVisible
        {
            get { return leftBtn.Visible; }
            set { leftBtn.Visible = rightBtn.Visible = value; }
        }

        private void OnClick_actionBtn(object sender, EventArgs e)
        {
            OnPressActionButton?.Invoke();
        }

        private void OnClick_leftBtn(object sender, EventArgs e)
        {
            LeftClick?.Invoke();
        }

        private void OnClick_rightBtn(object sender, EventArgs e)
        {
            RightClick?.Invoke();
        }

    }
}
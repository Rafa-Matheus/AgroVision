using System;
using System.Drawing;
using System.Windows.Forms;

namespace AgroVision.Forms
{
    public partial class SwitchView : UserControl
    {

        public event Action OnValueChange;

        public SwitchView()
        {
            InitializeComponent();

            colorBar.Value = 0;
            colorBar.Minimum = 0;
            colorBar.Maximum = 100;
        }

        public bool Value
        {
            get { return enabled; }
            set
            {
                enabled = value;
                UpdateValue();
                UpdateView();
            }
        }

        private bool enabled;
        private void OnScroll(object sender, ScrollEventArgs e)
        {
            bool change = colorBar.Value > 50;

            if (enabled != change)
                UpdateValue();

            enabled = change;
        }

        private void UpdateValue()
        {
            OnValueChange?.Invoke();
        }

        public void UpdateView()
        {
            colorBar.Value = enabled ? 100 : 0;

            colorBar.ThumbInnerColor = colorBar.ThumbOuterColor = enabled ? Color.FromArgb(0, 155, 219) : Color.Gray;
            colorBar.ThumbPenColor = enabled ? Color.DarkSlateGray : Color.Black;
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (lastPoint == e.Location)
            {
                enabled = !enabled;

                UpdateValue();
            }

            UpdateView();
        }

        private Point lastPoint;
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = e.Location;
        }

    }
}
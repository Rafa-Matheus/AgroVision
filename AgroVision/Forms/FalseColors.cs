using DHS.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace AgroVision
{
    public partial class FalseColors : Form
    {

        #region Propriedades
        public List<Color> Colors { get; set; }

        public List<float> Positions { get; set; }
        #endregion

        #region Inicializar
        private readonly List<RectangleF> rects;
        private readonly Font font;

        public FalseColors()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.UserPaint
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.ResizeRedraw, true);

            Colors = new List<Color>();
            Positions = new List<float>();
            rects = new List<RectangleF>();

            Colors.Add(Color.Black);
            Positions.Add(0);
            Colors.Add(Color.White);
            Positions.Add(1);

            font = new Font("Segoe UI", 12, FontStyle.Regular);

            UpdateColors();
        }
        #endregion

        public void UpdateColors()
        {
            ColorBlender blender = new ColorBlender();

            if (Positions[0] > 0)
            {
                Colors.Insert(0, Colors[0]);
                Positions.Insert(0, 0);
            }

            if (Positions[Positions.Count - 1] < 1)
            {
                Colors.Add(Colors[Colors.Count - 1]);
                Positions.Add(1);
            }

            for (int colorIndex = 0; colorIndex < Colors.Count; colorIndex++)
                blender.AddColor(Colors[colorIndex], Positions[colorIndex]);

            blender.Blend();
            blendPbx.Image = blender.GetHorizontalBar(blendPbx.Width, blendPbx.Height);

            Refresh();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            rects.Clear();
            for (int colorIndex = 0; colorIndex < Colors.Count; colorIndex++)
            {
                float position = Positions[colorIndex];
                PointF point = new PointF(blendPbx.Left + (position * (blendPbx.Width - 1)), blendPbx.Bottom);
                DrawSelector(point, Colors[colorIndex], colorIndex == selectedIndex, e.Graphics);
            }

            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            e.Graphics.DrawString("-1.0", font, Brushes.White, new PointF(blendPbx.Left - 15, blendPbx.Top - 25));
            e.Graphics.DrawString("1.0", font, Brushes.White, new PointF(blendPbx.Left + blendPbx.Width - 15, blendPbx.Top - 25));
        }

        private float deltaX;
        private float lastPosition;
        private int selectedIndex;
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            deltaX = e.X;

            selectedIndex = -1;
            for (int rectIndex = 0; rectIndex < rects.Count; rectIndex++)
                if (rects[rectIndex].Contains(e.Location))
                {
                    selectedIndex = rectIndex;
                    lastPosition = Positions[selectedIndex];
                    break;
                }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                if (selectedIndex != -1)
                {
                    float x = (lastPosition * blendPbx.Width) - (deltaX - e.X);
                    float position = x / blendPbx.Width;

                    //Limitar
                    position = position > 1 ? 1 : position < 0 ? 0 : position;

                    Positions[selectedIndex] = position;

                    Refresh();
                }
        }

        private void DrawSelector(PointF pt, Color color, bool is_selected, Graphics g)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddLine(pt.X, pt.Y, pt.X - 10, pt.Y + 10);
            path.AddLine(pt.X - 10, pt.Y + 10, pt.X - 10, pt.Y + 30);
            path.AddLine(pt.X - 10, pt.Y + 30, pt.X + 10, pt.Y + 30);
            path.AddLine(pt.X + 10, pt.Y + 30, pt.X + 10, pt.Y + 10);
            path.AddLine(pt.X + 10, pt.Y + 10, pt.X, pt.Y);

            g.FillPath(is_selected ? Brushes.Black : Brushes.Gray, path);
            g.DrawPath(Pens.DarkGray, path);

            RectangleF rect = new RectangleF(pt.X - 7, pt.Y + 13, 15, 15);
            rects.Add(rect);

            g.FillRectangle(new SolidBrush(color), rect);
        }

        private void OnClick_addColorBtn(object sender, EventArgs e)
        {
            if (selectedIndex != -1)
            {
                if (selectedIndex < Colors.Count - 1)
                {
                    float firstPosition = Positions[selectedIndex];
                    float secondPosition = Positions[selectedIndex + 1];

                    Colors.Insert(selectedIndex + 1, GetHalfColor(Colors[selectedIndex], Colors[selectedIndex + 1]));
                    Positions.Insert(selectedIndex + 1, Positions[selectedIndex] + Math.Abs(secondPosition - firstPosition) / 2);
                }
                else
                {
                    Colors.Insert(selectedIndex, Colors[selectedIndex]);
                    Positions.Insert(selectedIndex, .5f);
                }

                UpdateColors();
            }
        }

        private Color GetHalfColor(Color a, Color b)
        {
            return Color.FromArgb(Math.Abs(a.R - b.R) / 2, Math.Abs(a.G - b.G) / 2, Math.Abs(a.B - b.B) / 2);
        }

        private void OnClick_removeClickBtn(object sender, EventArgs e)
        {
            RemoveColor();
        }

        private void RemoveColor()
        {
            if (selectedIndex != -1)
            {
                Colors.RemoveAt(selectedIndex);
                Positions.RemoveAt(selectedIndex);

                UpdateColors();
            }
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            UpdateColors();
        }

        private void OnDoubleClick(object sender, EventArgs e)
        {
            for (int rectIndex = 0; rectIndex < rects.Count; rectIndex++)
                if (selectedIndex != -1)
                {
                    ColorDialog colorDialog = new ColorDialog
                    {
                        Color = Colors[selectedIndex]
                    };
                    if (colorDialog.ShowDialog() == DialogResult.OK)
                    {
                        Colors[selectedIndex] = colorDialog.Color;

                        UpdateColors();
                    }

                    break;
                }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Delete)
                RemoveColor();

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void OnClick_applyBtn(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void OnClick_resetBtn(object sender, EventArgs e)
        {
            Colors.Clear();
            Positions.Clear();

            Colors.Add(Color.Red);
            Positions.Add(0);
            Colors.Add(Color.Yellow);
            Positions.Add(.5f);
            Colors.Add(Color.LimeGreen);
            Positions.Add(1);

            UpdateColors();
        }

    }
}
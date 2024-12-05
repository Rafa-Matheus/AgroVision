using DHS.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Text;
using System.Windows.Forms;

namespace AgroVision
{
    public partial class ChartView : UserControl
    {

        public event Action OnCloseButton;

        private readonly Font firstFont;
        private readonly Font secondFont;
        public ChartView()
        {
            InitializeComponent();

            Series = new List<ChartSeries>();

            firstFont = new Font("Segoe UI", 11, FontStyle.Regular);
            secondFont = new Font("Segoe UI", 14, FontStyle.Regular);
        }

        public List<ChartSeries> Series { get; set; }

        public double[] Percentages { get; set; }

        public void UpdateData()
        {
            //for (int i = 0; i < Series.Count; i++)
            //    Total += Series[i].Value;

            Percentages = new double[Series.Count];
            for (int percentageIndex = 0; percentageIndex < Series.Count; percentageIndex++)
            {
                ChartSeries c = Series[percentageIndex];

                //if (c.Value > 0)
                    Percentages[percentageIndex] = (c.Value / Total) * 100;
            }

            Refresh();
        }

        public DateTime Date { get; set; }

        public double Total { get; set; }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            RectangleF rect = new RectangleF(20, 35, 100, 100);

            //Data
            e.Graphics.DrawString($"Mapeamento de {Date.ToShortDateString()}", firstFont, Brushes.Gray, new Point(10, 5));

            //Pizza
            float lastAngle = 270;
            for (int i = 0; i < Series.Count; i++)
            {
                ChartSeries c = Series[i];

                BitmapColor bc = c.Gradient.GetColor(.5f);

                Pen pen = new Pen(new SolidBrush(Color.FromArgb(bc.R, bc.G, bc.B)), 10f);
                //float angle = (float)((c.Value / Total) * 360);
                float angle = (float)((Percentages[i] / 100f) * 360);

                //Evitar ângulos muito pequenos
                angle = angle < 0 ? 0 : angle;

                e.Graphics.DrawArc(pen, rect, (int)lastAngle, (int)angle);

                lastAngle += angle;

                //Eliminar o lápis
                pen.Dispose();
            }

            StringBuilder builder = new StringBuilder();
            for (int seriesIndex = 0; seriesIndex < Series.Count; seriesIndex++)
            {
                ChartSeries c = Series[seriesIndex];

                e.Graphics.DrawImage(c.Gradient.GetHorizontalBar(30, 20), new RectangleF(135, 38 + (25 * seriesIndex), 30, 20));

                if (seriesIndex > 0)
                    builder.Append("\n");

                builder.Append($"{Percentages[seriesIndex]:N5}% - {c.Title}");
            }

            e.Graphics.DrawString(builder.ToString(), secondFont, Brushes.White, new Point(170, 35));
        }

        private void OnClick_closeBtn(object sender, EventArgs e)
        {
            OnCloseButton?.Invoke();
        }

        public bool CloseButtonVisible { get { return closeBtn.Visible; } set { closeBtn.Visible = value; } }

    }
}

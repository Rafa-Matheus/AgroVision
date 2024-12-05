using AgroVision.Forms;
using DHS.Imaging;
using DHS.IO;
using DHS.Reporting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AgroVision
{
    public partial class MetricsForm : Form
    {

        private readonly Font font;
        public MetricsForm()
        {
            InitializeComponent();

            //Partes mortas da plantação
            //Melhores partes da Plantação
            //Localização das estradas
            //Extensão da plantação
            //Piores partes do solo

            title = "Monitoramento Espectro-Temporal";

            font = new Font("Segoe UI", 20, FontStyle.Regular);
        }

        private ColorBlender blender;
        public void SetColors(ColorBlender blender)
        {
            this.blender = blender;
        }

        private readonly string title;
        private bool firstItem;
        public void AddToColumn(ChartView chart, Bitmap thumb_img)
        {
            //Adicionar definir tamanhos
            if (firstItem)
            {
                compareTbl.ColumnCount++;
                compareTbl.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 0));
            }
            else
                chart.CloseButtonVisible = false;

            int index = compareTbl.ColumnCount - 1;

            Panel panel = new Panel() { Dock = DockStyle.Fill };
            compareTbl.Controls.Add(panel, index, 0);

            if (!firstItem)
                panel.BackColor = Color.FromArgb(64, 64, 64);

            //Miniatura
            PictureBox thumb = new PictureBox() { Dock = DockStyle.Top, BorderStyle = BorderStyle.FixedSingle, BackgroundImageLayout = ImageLayout.Zoom };
            thumb.BackgroundImage = thumb_img;
            thumb.Height = 200;

            //Comparações
            CompareView comp = new CompareView() { Dock = DockStyle.Top, BorderStyle = BorderStyle.FixedSingle };
            comp.Text = "Comparação";
            comp.Height = 110;

            chart.OnCloseButton += delegate
            {
                compareTbl.ColumnCount--;
                compareTbl.ColumnStyles.RemoveAt(Math.Min(index, compareTbl.ColumnCount - 1));

                panel.Dispose();
                chart.Dispose();
                comp.Dispose();

                if (compareTbl.ColumnCount == 1)
                    firstItem = true;

                UpdateData();
            };

            panel.Controls.Add(thumb);
            panel.Controls.Add(chart);
            chart.BringToFront();
            panel.Controls.Add(comp);
            comp.BringToFront();

            firstItem = true;

            UpdateData();
        }

        private void UpdateData()
        {
            for (int compareItemIndex = 0; compareItemIndex < compareTbl.ColumnCount; compareItemIndex++)
                compareTbl.ColumnStyles[compareItemIndex] = new ColumnStyle(SizeType.Percent, 1 / (float)compareTbl.ColumnCount);

            Compare();

            Refresh();
        }

        private void Compare()
        {
            ChartView firstView = (ChartView)compareTbl.GetControlFromPosition(0, 0).Controls[1];
            int count = firstView.Series.Count;

            //Outros
            if (compareTbl.ColumnCount > 1)
            {
                ((CompareView)compareTbl.GetControlFromPosition(0, 0).Controls[0])
                    .CompareText = "*Todas as comparações ao lado\nsão baseadas nessa coluna.";

                for (int compareItemIndex = 1; compareItemIndex < compareTbl.ColumnCount; compareItemIndex++)
                {
                    CompareView compare = (CompareView)compareTbl.GetControlFromPosition(compareItemIndex, 0).Controls[0];
                    ChartView serie = (ChartView)compareTbl.GetControlFromPosition(compareItemIndex, 0).Controls[1];

                    if (serie.Series.Count != count)
                    {
                        MessageBox.Show("A outra coluna não bate com o número de séries.");
                        return;
                    }

                    StringBuilder builder = new StringBuilder();
                    for (int seriesIndex = 0; seriesIndex < serie.Series.Count; seriesIndex++)
                    {
                        ChartSeries series = serie.Series[seriesIndex];
                        if (series.Title != firstView.Series[seriesIndex].Title)
                        {
                            MessageBox.Show("A sequência de séries não combina.");
                            return;
                        }

                        //Somente se tiver porcentagens
                        if (serie.Percentages == null)
                            continue;

                        double firstPercentage = firstView.Percentages[seriesIndex];
                        double secondPercentage = serie.Percentages[seriesIndex];

                        double dif = secondPercentage - firstPercentage;

                        if (seriesIndex > 0)
                            builder.Append("\n");

                        builder.Append($"{series.Title}: ");

                        if (dif != 0)
                            builder.Append($"{(dif > 0 ? "+" : "")}{dif:N5}%");
                        else
                            builder.Append("(Nenhuma alteração)");

                        builder.Append(" ");
                    }

                    compare.CompareText = builder.ToString();
                }
            }

            //Reordenar pela data
            //Criar lista
            List<Control> columns = new List<Control>();
            for (int compareItemIndex = 0; compareItemIndex < compareTbl.ColumnCount; compareItemIndex++)
                columns.Add(compareTbl.Controls[compareItemIndex]);

            columns.Sort((a, b) => DateTime.Compare(((ChartView)(((Panel)a).Controls[1])).Date, ((ChartView)(((Panel)b).Controls[1])).Date));

            compareTbl.Controls.Clear();

            for (int compareItemIndex = 0; compareItemIndex < columns.Count; compareItemIndex++)
                compareTbl.Controls.Add(columns[compareItemIndex], compareItemIndex, 0);
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            TextRenderer.DrawText(e.Graphics, title, font, topPnl.ClientRectangle, Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private void OnResize(object sender, EventArgs e)
        {
            topPnl.Refresh();
        }

        private void OnClick_compareBtn(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "Mapeamento do AgroVision|*.agvm"
            };
            if (open.ShowDialog() == DialogResult.OK)
            {
                RasterData data = Binary.ReadFromBinary<RasterData>(open.FileName);

                MetricsHelper helper = new MetricsHelper();

                if (blender == null)
                    SetColors(data.Blender);

                helper.Ranges.Add(new RangeValue("Sem Vegetação/Morta", -1, 0));
                helper.Ranges.Add(new RangeValue("Precisa de Intervenção", 0, .33));
                helper.Ranges.Add(new RangeValue("Bom", .33, .66));
                helper.Ranges.Add(new RangeValue("Muito Bom", .66, 1));

                helper.Count(data.Thumbnail, data.Blender);

                AddFromMetricsHelper(helper, data.Thumbnail, data.Date);
            }
        }

        public void AddFromMetricsHelper(MetricsHelper helper, Bitmap thumb, DateTime date)
        {
            ChartView chart = new ChartView
            {
                Date = date,
                Dock = DockStyle.Top
            };

            //float r = 1 / (float)helper.Ranges.Count;
            for (int rangeIndex = 0; rangeIndex < helper.Ranges.Count; rangeIndex++)
            {
                RangeValue range = helper.Ranges[rangeIndex];

                //MessageBox.Show($"Intervalo {range.Title} {range.Count} {helper.Total}");

                //float g = i / (float)helper.Ranges.Count;

                float firstScaledColor = (float)RasterImage.Scale(range.MinValue, -1, 1, 0, 1);
                float secondScaledColor = (float)RasterImage.Scale(range.MaxValue, -1, 1, 0, 1);

                BitmapColor firstBitmapColor = blender.GetColor(firstScaledColor);
                Color firstColor = Color.FromArgb(firstBitmapColor.R, firstBitmapColor.G, firstBitmapColor.B);

                BitmapColor secondBitmapColor = blender.GetColor(secondScaledColor);
                Color secondColor = Color.FromArgb(secondBitmapColor.R, secondBitmapColor.G, secondBitmapColor.B);

                chart.Series.Add(new ChartSeries(range.Title, range.Count, firstColor, secondColor));
            }

            //Ir com base no total do helper
            chart.Total = helper.Total;

            chart.UpdateData();

            AddToColumn(chart, thumb);
        }

        private void OnClick_printBtn(object sender, EventArgs e)
        {
            if (compareTbl.Controls.Count == 0) return;

            ReportPage report = new ReportPage
            {
                Header = "",
                Footer = "AgroVision v1.0\nSforza Tec.",
                Title = $"{"Relatório"}\n{DateTime.Now.ToShortDateString()}"
            };

            //report.Tables.Add(ReportItem.GenerateFromListView(, ""));

            //Outras informações
            ReportItem titulo = new ReportItem("Monitoramento Espectro-Temporal:");

            ReportItem datas = new ReportItem("Data");
            ReportItem valores = new ReportItem("Valores");
            ReportItem comparacoes = new ReportItem("Comparação");

            for (int compareItemIndex = 0; compareItemIndex < compareTbl.ColumnCount; compareItemIndex++)
            {
                CompareView compare = (CompareView)compareTbl.GetControlFromPosition(compareItemIndex, 0).Controls[0];
                ChartView seriesView = (ChartView)compareTbl.GetControlFromPosition(compareItemIndex, 0).Controls[1];

                datas.SubItems.Add(new ReportItem(seriesView.Date.ToShortDateString()));

                StringBuilder builder = new StringBuilder();
                for (int seriesIndex = 0; seriesIndex < seriesView.Series.Count; seriesIndex++)
                {
                    ChartSeries series = seriesView.Series[seriesIndex];

                    builder.Append($"{series.Title} {series.Value / seriesView.Total * 100:N5}%\n");
                }

                valores.SubItems.Add(new ReportItem(builder.ToString()));
                comparacoes.SubItems.Add(new ReportItem(compare.CompareText));
            }

            titulo.SubItems.Add(datas);
            titulo.SubItems.Add(valores);
            titulo.SubItems.Add(comparacoes);

            report.Tables.Add(titulo);

            report.Print();
        }

    }
}

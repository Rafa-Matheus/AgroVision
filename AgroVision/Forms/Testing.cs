using AgroVision.Mapping;
using AgroVision.Utils;
using DHS.Imaging;
using DHS.IO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace AgroVision
{
    public partial class Testing : Form
    {


        private readonly ZigZagTracer zigZag;
        public Testing()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer
                            | ControlStyles.UserPaint
                            | ControlStyles.AllPaintingInWmPaint
                            | ControlStyles.ResizeRedraw, true);

            zigZag = new ZigZagTracer();

            this.MouseClick += (o, args) =>
            {
                if (args.Button == MouseButtons.Right)
                {
                    ContextMenu menu = new ContextMenu();
                    AddToContextMenu(menu);
                    menu.Show(this, args.Location);
                }
            };
        }

        private bool isEditing;
        private Polygon backupBlock;
        private readonly List<NPoint> tempPoints = new List<NPoint>();
        private int selectedPointIndex = -1;
        private NPoint[] savedTempPoints;
        private readonly float pointSize = 5;
        private int mouseCursor = -1;
        private float lastX = 0;
        private float lastY = 0;

        public bool EnableDraw { get; set; }

        public void AddToContextMenu(ContextMenu menu)
        {
            if (EnableDraw)
            {
                if (selectedBlock != null)
                {
                    List<MenuItem> listItems = new List<MenuItem>
                    {
                        new MenuItem("Editar Polígono",
                                    delegate
                                    {
                                        tempPoints.AddRange(selectedBlock.Points.Select(p => p).ToArray());

                                        backupBlock = selectedBlock;

                                        blocks.Remove(selectedBlock);

                                        selectedBlock = null;

                                        isEditing = true;
                                    })
                    };

                    menu.MenuItems.AddRange(listItems.ToArray());
                }
            }
        }

        //private Image processedImage;
        private void ondraw(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            //if (processedImage != null)
            //    e.Graphics.DrawImage(processedImage, new Rectangle(0, 0, 300, 220));

            List<PointF> drawBlockPoints = new List<PointF>();
            foreach (NPoint p in tempPoints)
                drawBlockPoints.Add(p.ToPoint());

            if (tempPoints.Count > 2)
            {
                e.Graphics.FillPolygon(new SolidBrush(Color.FromArgb(32, 0, 0, 0)), drawBlockPoints.ToArray());
                e.Graphics.DrawPolygon(Pens.Black, drawBlockPoints.ToArray());
            }

            foreach (NPoint tempPoint in tempPoints)
            {
                PointF point = tempPoint.ToPoint();
                e.Graphics.FillEllipse(new SolidBrush(Color.Red), new RectangleF(point.X - (pointSize / 2), point.Y - (pointSize / 2), pointSize, pointSize));
            }

            //Blocos
            foreach (Polygon block in blocks)
            {
                PointF[] drawPoints = block.Points.Select(p => p.ToPoint()).ToArray();

                bool isClicked = block == selectedBlock;

                e.Graphics.FillPolygon(new SolidBrush(isClicked ? Color.Yellow : Color.SkyBlue), drawPoints);
                e.Graphics.DrawPolygon(isClicked ? Pens.Transparent : new Pen(new SolidBrush(DrawUtil.ChangeBrightness(Color.SkyBlue, -100)), 2f), drawPoints);

                //Caminho
                CreatePathInsideBlock(block, e.Graphics);
            }

            //Mostrar as 10 cores mais usadas
            if (colors != null)
            {
                int colorIndex = 0;
                int pickStep = colors.Count / 8;
                //List<KeyValuePair<BitmapColor, int>> range_values = colors.Where(c => CheckColorRange(c.Key.R, c.Key.G, c.Key.B)).ToList();
                for (int offsetIndex = 0; offsetIndex < Math.Min(colors.Count, 10); offsetIndex++, colorIndex += pickStep)
                {
                    if (colorIndex >= colors.Count) break;

                    KeyValuePair<BitmapColor, int> pair = colors.ElementAt(colorIndex);
                    int r = pair.Key.R;
                    int g = pair.Key.G;
                    int b = pair.Key.B;

                    RectangleF rect = new RectangleF(10 + offsetIndex * (20 + 5), 10, 20, 20);
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(r, g, b)), rect);
                    e.Graphics.DrawRectangles(Pens.White, new[] { rect });
                }
            }
        }

        public void CreatePathInsideBlock(Polygon pol, Graphics g)
        {
            zigZag.TracePaths(null, spacingTcb.Value, angleTcb.Value, 0, pol, g);
        }

        private Polygon selectedBlock;
        private readonly List<Polygon> blocks = new List<Polygon>();
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Enter:
                    if (tempPoints.Count < 3)
                        break;

                    //Adiciona o novo bloco ao mapa
                    blocks.Add(new Polygon(tempPoints));

                    isEditing = false;

                    tempPoints.Clear();
                    backupBlock = null;

                    Refresh();
                    break;
                case Keys.Escape:
                    tempPoints.Clear();

                    if (isEditing)
                    {
                        blocks.Add(backupBlock);

                        backupBlock = null;
                    }

                    Refresh();
                    break;
                case Keys.Delete:
                    if (selectedPointIndex != -1)
                    {
                        tempPoints.RemoveAt(selectedPointIndex);

                        if (tempPoints.Count == 0)
                            Cursor = Cursors.Default;

                        Refresh();
                    }
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            PointF position = e.Location;
            switch (e.Button)
            {
                case MouseButtons.Left:
                    //Mover o bloco inteiro
                    if (mouseCursor == 0)
                    {
                        if (savedTempPoints.Length > 0)
                            for (int i = 0; i < tempPoints.Count; i++)
                                tempPoints[i] = new NPoint(savedTempPoints[i][0] + (position.X - lastX), savedTempPoints[i][1] + (position.Y - lastY));
                    }

                    //Mover apenas um ponto
                    else if (mouseCursor == 4)
                    {
                        if (selectedPointIndex != -1)
                            tempPoints[selectedPointIndex] = position.ToNPoint();
                    }

                    Refresh();
                    break;
                default:
                    Cursor = Cursors.Cross;
                    selectedPointIndex = -1;
                    mouseCursor = -1;

                    if (tempPoints.Count > 2)
                    {
                        NPoint[] drawedTempPoints = new NPoint[tempPoints.Count];
                        savedTempPoints = new NPoint[tempPoints.Count];

                        for (int tempPointIndex = 0; tempPointIndex < tempPoints.Count; tempPointIndex++)
                        {
                            drawedTempPoints[tempPointIndex] = new NPoint(tempPoints[tempPointIndex][0], tempPoints[tempPointIndex][1]);
                            savedTempPoints[tempPointIndex] = new NPoint(tempPoints[tempPointIndex][0], tempPoints[tempPointIndex][1]);
                        }

                        lastX = position.X;
                        lastY = position.Y;

                        if (PolygonUtil.PolygonContains(position.ToNPoint(), drawedTempPoints))
                        {
                            Cursor = Cursors.SizeAll;
                            mouseCursor = 0;
                        }
                    }

                    for (int tempPointIndex = 0; tempPointIndex < tempPoints.Count; tempPointIndex++)
                    {
                        PointF point = tempPoints[tempPointIndex].ToPoint();

                        if (position.X > point.X - 5 && position.X < point.X + 5 &&
                            position.Y > point.Y - 5 && position.Y < point.Y + 5)
                        {
                            selectedPointIndex = tempPointIndex;

                            Cursor = Cursors.Hand;
                            mouseCursor = 4;

                            break;
                        }
                    }
                    break;
            }
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (mouseCursor == -1)
                tempPoints.Add(e.Location.ToNPoint());

            Refresh();
        }

        private void OnScroll_trackBar(object sender, EventArgs e)
        {
            Refresh();
        }

        private Dictionary<BitmapColor, int> colors;
        private void button1_Click(object sender, EventArgs e)
        {
            if (BackgroundImage != null)
            {
                colors = new Dictionary<BitmapColor, int>();
                FastBitmap src = new FastBitmap((Bitmap)BackgroundImage);
                for (int x = 0; x < src.Width; x++)
                    for (int y = 0; y < src.Height; y++)
                    {
                        BitmapColor color = src.GetPixel(x, y);

                        if (color.A < 250) continue;

                        if (colors.ContainsKey(color))
                            colors[color]++;
                        else
                            colors.Add(color, 1);
                    }

                //Ordenar do maior pro menor
                colors.OrderByDescending(k => k.Value);

                Refresh();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] tiffFilePaths = Dialogs.OpenFileDialog("Arquivo de Imagem Tagueada|*.tif;*.tiff;*.jpg");
            if (tiffFilePaths.Length > 0)
            {
                RasterImage rasterImage = new RasterImage();
                //MessageBox.Show(tiffFilePaths[0]);
                rasterImage.InitImage(tiffFilePaths[0]);
                rasterImage.LoadAndProcess("(B-R)/(B+R)");

                ColorBlender colorBlender = new ColorBlender();

                colorBlender.AddColor(Color.Maroon, 0);
                colorBlender.AddColor(Color.Red, .5f);
                colorBlender.AddColor(Color.Yellow, .6f);
                colorBlender.AddColor(Color.LimeGreen, .75f);
                colorBlender.AddColor(Color.Green, 1);
                colorBlender.Blend();

                rasterImage.ColorizeAndSaveImage(colorBlender, rasterImage.MinValue, rasterImage.MaxValue);

                //MessageBox.Show(rasterImage.OriginalImage.ToString());

                //processedImage = rasterImage.OriginalImage.ToBitmap();
                //Refresh();
            }
        }

    }
}
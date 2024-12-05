using AgroVision.CustomViews;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AgroVision.Forms
{
    public partial class ManageImagesForm : Form
    {

        public event Action OnViewUpdated;

        //private int last_index;
        //private int count_down;
        public ManageImagesForm()
        {
            InitializeComponent();

            //t = new Timer
            //{
            //    Interval = 500
            //};
            //t.Tick += delegate
            //{
            //    count_down--;

            //    if (count_down < 0)
            //    {
            //        ChangeImagesState(last_index);
            //        count_down = 1;
            //        t.Stop();
            //    }
            //};
        }

        //private Timer t;
        private RasterBatch batch;
        public RasterBatch Batch
        {
            get { return batch; }
            set
            {
                batch = value;
                batch.EnableClip = false;
                OnViewUpdated?.Invoke();

                for (int batchImageIndex = 0; batchImageIndex < batch.Images.Count; batchImageIndex++)
                {
                    RasterImage img = batch.Images[batchImageIndex];

                    //int index = i;
                    ImageEditView edit = new ImageEditView
                    {
                        Title = Path.GetFileNameWithoutExtension(img.ImagePath),
                        GeoLocation = $"{img.GPSPosition.Latitude} {img.GPSPosition.Longitude}",
                        Dock = DockStyle.Top,
                        Image = batch.Images[batchImageIndex]
                    };
                    edit.MouseDown += (o, args) =>
                    {
                        if (args.Button == MouseButtons.Left)
                            edit.BackColor = Color.Gray;
                    };
                    edit.MouseUp += (o, args) =>
                    {
                        if (args.Button == MouseButtons.Left)
                            edit.BackColor = Color.Transparent;
                    };
                    //edit.MouseEnter += delegate
                    //{
                    //    if (edit.IsVisible())
                    //        if (t != null)
                    //        {
                    //            last_index = index;
                    //            t.Start();
                    //        }
                    //};
                    //edit.MouseLeave += delegate
                    //{
                    //    if (t != null)
                    //    {
                    //        t.Stop();
                    //        count_down = 1;
                    //    }

                    //    if (edit.IsVisible())
                    //        ChangeImagesState(-1);
                    //};
                    edit.ViewUpdated += delegate
                    {
                        OnViewUpdated?.Invoke();
                    };
                    edit.DeleteImage += delegate
                    {
                        if (MessageBox.Show("Deseja mesmo apagar essa imagem?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            edit.Dispose();

                            batch.RemoveImage(img);

                            OnViewUpdated?.Invoke();
                        }
                    };
                    edit.GoUpImage += delegate
                    {
                        int imageIndex = imagesPnl.Controls.IndexOf(edit);
                        if (imageIndex + 1 < imagesPnl.Controls.Count)
                            imagesPnl.Controls.SetChildIndex(edit, imageIndex + 1);

                        int newBatchImageIndex = batch.Images.IndexOf(img);
                        if (newBatchImageIndex - 1 >= 0)
                        {
                            RasterImage clone = batch.Images[newBatchImageIndex];
                            batch.Images.RemoveAt(newBatchImageIndex);
                            batch.Images.Insert(newBatchImageIndex - 1, clone);

                            OnViewUpdated?.Invoke();
                        }
                    };
                    edit.GoDownImage += delegate
                    {
                        int imageIndex = imagesPnl.Controls.IndexOf(edit);
                        if (imageIndex - 1 >= 0)
                            imagesPnl.Controls.SetChildIndex(edit, imageIndex - 1);

                        int newBatchImageIndex = batch.Images.IndexOf(img);
                        if (newBatchImageIndex + 1 < batch.Images.Count)
                        {
                            RasterImage clone = batch.Images[newBatchImageIndex];
                            batch.Images.RemoveAt(newBatchImageIndex);
                            batch.Images.Insert(newBatchImageIndex + 1, clone);

                            OnViewUpdated?.Invoke();
                        }
                    };

                    imagesPnl.Controls.Add(edit);

                    //Reordenar
                    imagesPnl.OnReOrderIndex += (o, args) =>
                    {
                        int oldImageIndex = batch.Images.Count - (args.OldIndex + 1);
                        int newImageIndex = batch.Images.Count - (args.NewIndex + 1);

                        RasterImage clone = batch.Images[oldImageIndex];
                        batch.Images.RemoveAt(oldImageIndex);
                        batch.Images.Insert(newImageIndex, clone);

                        OnViewUpdated?.Invoke();
                    };

                    edit.BringToFront();
                }
            }
        }

        //private void ChangeImagesState(int index)
        //{
        //    for (int j = 0; j < batch.Images.Count; j++)
        //        batch.Images[j].Visible = index > -1 ? j == index : true;

        //    OnViewUpdated?.Invoke();
        //}

        private void OnClosing(object sender, FormClosingEventArgs e)
        {
            if (batch != null)
                batch.EnableClip = true;

            OnViewUpdated?.Invoke();
        }

    }
}
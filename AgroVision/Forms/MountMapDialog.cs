using System;
using System.Windows.Forms;

namespace AgroVision
{
    public partial class MountMapDialog : Form
    {

        public event Action TiffButton;
        public event Action FileButton;

        public MountMapDialog()
        {
            InitializeComponent();
        }

        private void PhotosBtn_Click(object sender, EventArgs e)
        {
            if (TiffButton != null)
            {
                TiffButton();
                Close();
            }
        }

        private void FileBtn_Click(object sender, EventArgs e)
        {
            if (FileButton != null)
            {
                FileButton();
                Close();
            }
        }

    }
}

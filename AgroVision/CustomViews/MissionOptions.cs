using System;
using System.Windows.Forms;

namespace AgroVision.Forms
{
    public partial class MissionOpions : UserControl
    {

        public event Action DrawButton;
        public event Action MountButton;

        public MissionOpions()
        {
            InitializeComponent();
        }

        public string Mode { get; set; }


        private void DrawBtn_Click(object sender, EventArgs e)
        {
            DrawButton?.Invoke();
        }

        private void MountMapBtn_Click(object sender, EventArgs e)
        {
            MountButton?.Invoke();
        }

    }
}
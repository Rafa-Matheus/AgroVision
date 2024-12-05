using System.Windows.Forms;

namespace AgroVision
{
    public partial class HelpForm : Form
    {

        public HelpForm()
        {
            InitializeComponent();

            helpRch.LoadFile(@".\ajuda.rtf");
        }

    }
}

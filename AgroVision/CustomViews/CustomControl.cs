using System.Drawing;
using System.Windows.Forms;

namespace AgroVision.Views
{
    public class CustomControl
    {

        public CustomControl(string title, string help, Image icon, Control control, Control parent)
        {
            Title = title;
            Help = help;
            Icon = icon;
            Control = control;
            Parent = parent;
        }

        public string Title { get; set; }

        public string Help { get; set; }

        public Image Icon { get; set; }

        public Control Control { get; set; }

        public Control Parent { get; set; }

    }
}
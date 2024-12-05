using System.Drawing;
using System.Windows.Forms;

namespace AgroVision
{
    public class CompareView : Panel
    {

        public CompareView()
        {
            Font font = new Font("Segoe UI", 14, FontStyle.Regular);

            Paint += (o, args) =>
            {
                TextRenderer.DrawText(args.Graphics, CompareText, font, ClientRectangle, Color.White, TextFormatFlags.Default);
            };
        }

        public string CompareText { get; set; }

    }
}

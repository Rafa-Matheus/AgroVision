using System.Drawing;
using System.Windows.Forms;

namespace AgroVision
{
    public class CustomButton : Button
    {

        public CustomButton()
        {
            FlatAppearance.BorderSize = 0;
            FlatStyle = FlatStyle.Flat;
            Font = new Font("Segoe UI", 8, FontStyle.Bold);
            ForeColor = Color.White;
            BackColor = Color.FromArgb(0, 155, 219);
        }

    }
}

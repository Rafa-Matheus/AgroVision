using System;
using System.Windows.Forms;

namespace AgroVision.Forms
{
    public partial class DemoForm : Form
    {

        public DemoForm()
        {
            InitializeComponent();
        }

        private DateTime expireDate;
        public DateTime ExpireDate { get {return expireDate; } set
            {
                expireDate = value;

                infoLbl.Text = "Você está usando uma versão trial do software AgroVision e do aplicativo Controle do AgroVision, " +
                "desenvolvido pela SFORZA TEC. (CNPJ 33.383.463.0001/39)." +
                $"\nO prazo de validade para uso do sistema é até {expireDate.ToShortDateString()}.";
            }
        }

        private void OnClosed(object sender, FormClosedEventArgs e)
        {
            if (DateTime.Now > ExpireDate)
                Application.Exit();
        }

        private void OnClick_closeBtn(object sender, EventArgs e)
        {
            Close();
        }

    }
}
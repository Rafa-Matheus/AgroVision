using System;
using System.Windows.Forms;

namespace AgroVision
{
    public partial class InputName : Form
    {

        public InputName()
        {
            InitializeComponent();
        }

        public string Value
        {
            get { return valueTbx.Text; }
            set { valueTbx.Text = value; }
        }

        public new string Text
        {
            get { return base.Text; }
            set { nameLbl.Text = $"{value}:"; base.Text = value; }
        }

        private void OnClick_okBtn(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
                okBtn.PerformClick();

            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
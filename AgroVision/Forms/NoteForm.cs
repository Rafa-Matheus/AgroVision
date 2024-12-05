using System;
using System.Windows.Forms;

namespace AgroVision.Forms
{
    public partial class NoteForm : Form
    {

        public event Action NoteDeleted;

        public NoteForm()
        {
            InitializeComponent();

            noteRch.Text = "(Digite sua nota aqui)";
            noteRch.SelectAll();
        }

        public string Content
        {
            get { return noteRch.Text; }
            set
            {
                noteRch.Text = value;
                noteRch.SelectAll();
                deleteBtn.Visible = true;
            }
        }

        private void OnDeativate(object sender, EventArgs e)
        {
            Opacity = .5f;
        }

        private void OnActivate(object sender, EventArgs e)
        {
            Opacity = 1f;
        }

        private void OnClick_okBtn(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void OnClick_deleteBtn(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja mesmo apagar a nota?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                NoteDeleted?.Invoke();
                Close();
            }
        }

    }
}
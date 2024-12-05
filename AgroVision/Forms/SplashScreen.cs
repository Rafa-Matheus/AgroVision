using System.Drawing;
using System.Windows.Forms;

namespace AgroVision
{
    public partial class SplashScreen : Form
    {

        //Efeito sombra e aceleração gráfica
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle = 0x20000;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        private readonly string[] messages = {
            "Carregando mapas...",
            "Carregando métricas...",
            "Carregando recomendações...",
            "Carregando telas do ambiente..."
        };

        private int messageIndex = 0;
        public SplashScreen()
        {
            InitializeComponent();

            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer,
                true);

            this.Shown += delegate
            {
                Timer timer = new Timer
                {
                    Interval = 800 //Isso é o que determina o quanto a tela de exibição irá ser mostrada
                };

                timer.Tick += delegate
                {
                    if (messageIndex < messages.Length)
                    {
                        messageLbl.Text = messages[messageIndex];

                        messageIndex++;
                    }
                    else
                        Close();
                };

                timer.Start();
            };
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            //Borda da janela
            e.Graphics.DrawRectangle(Pens.Gray, new Rectangle(0, 0, ClientSize.Width - 1, ClientSize.Height - 1));
        }

    }
}
using AgroVision.CustomViews;
using AgroVision.Forms;
using AgroVision.Utils;
using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace AgroVision
{
    public partial class Main : Form
    {

        #region Inicializar
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        public static string CURRENT_USER;

        public Main()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.UserPaint
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.ResizeRedraw, true);

            //Tela de exibição
            new SplashScreen().ShowDialog();

            ControlUtil.RegisterNotifications(notificationPnl);

            NotificationManager.Info("Bem vindo de volta!", "Gerencie suas missões utilizando o painel lateral esquerdo");

            RefreshList();

            Timer expireTimer = new Timer
            {
                Interval = 5000
            };
            expireTimer.Tick += delegate
            {
                expireTimer.Stop();

                DemoForm demo = new DemoForm
                {
                    ExpireDate = new DateTime(2021, 7, 31)
                };
                demo.ShowDialog();
            };
            //expireTimer.Start();
        }
        #endregion

        #region Listar usuários e arquivos
        private void RefreshList()
        {
            itemsPnl.Controls.Clear();

            string path = CreateUsersFolderIfNotExists();

            if (!IsCurrentUserActive())
                LoadUsers(path);
            else
                LoadMissions(path);
        }
        #endregion

        #region Métodos
        private string CreateUsersFolderIfNotExists()
        {
            string path = @".\users\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }

        private bool IsCurrentUserActive()
        {
            return CURRENT_USER != null;
        }

        private void SetCurrentUser(string user)
        {
            CURRENT_USER = user;
        }

        private void LoadUsers(string path)
        {
            string[] users = Directory.GetDirectories(path);
            for (int userIndex = 0; userIndex < users.Length; userIndex++)
            {
                string user = users[userIndex];
                string friendlyName = Path.GetFileName(user);

                ListItemView item = new ListItemView
                {
                    Icon = Properties.Resources.usuario,
                    Dock = DockStyle.Top,
                    ActionButtonEnabled = true
                };
                item.OnPressActionButton += delegate
                {
                    ContextMenu contextMenu = new ContextMenu(new[] { new MenuItem("Apagar Usuário", 
                                    //Apagar usuário
                                    delegate {
                                        if (MessageBox.Show("Deseja mesmo apagar esse usuário?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes){
                                            Directory.Delete(user, true);
                                            RefreshList();
                                        }
                                    }),
                                    //Renomear usuário
                                    new MenuItem("Renomear", delegate {
                                        InputName name = new InputName { Text = "Renomear",
                                        Value = friendlyName };
                                        if(name.ShowDialog() == DialogResult.OK){
                                            Directory.Move(user, $"{Path.GetDirectoryName(user)}\\{name.Value}");

                                            RefreshList();
                                        }
                                    })
                                });

                    contextMenu.Show(item, item.PointToClient(Cursor.Position));
                };
                item.OnButtonClick += delegate
                {
                    SetCurrentUser(friendlyName);

                    RefreshList();
                };
                item.Title = friendlyName;

                itemsPnl.Controls.Add(item);
                item.BringToFront();
            }

            newItemBtn.Text = "Novo Usuário";
            logoMainPbx.Left = 10;
            backBtn.Visible = false;
        }

        private void LoadMissions(string path)
        {
            string user_path = $"{path}{CURRENT_USER}";
            if (Directory.Exists(user_path))
            {
                string[] files = Directory.GetFiles(user_path);
                for (int fileIndex = 0; fileIndex < files.Length; fileIndex++)
                {
                    string file = files[fileIndex];
                    if (file.ToLower().EndsWith(".agv"))
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(file);

                        XmlNode root = doc.SelectSingleNode("//mission");

                        ListItemView item = new ListItemView
                        {
                            Icon = Properties.Resources.editar,
                            Dock = DockStyle.Top,
                            ActionButtonEnabled = true
                        };
                        item.OnPressActionButton += delegate
                        {
                            ContextMenu contextMenu = new ContextMenu(new[] {
                                    new MenuItem("Editar",
                                    //Editar missão
                                    delegate {
                                        ChoiceMode(file, null);
                                    }),
                                    new MenuItem("Transferir Missão",
                                    //Transferir missão
                                    delegate {
                                        TransferUtil.Transfer(file);
                                    }),
                                    //Apagar missão
                                    new MenuItem("Excluir", delegate {
                                        if (MessageBox.Show("Deseja mesmo excluir a missão?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes){
                                            File.Delete(file);

                                            RefreshList();
                                        }
                                    }),
                                });
                            contextMenu.Show(item, item.PointToClient(Cursor.Position));
                        };
                        item.OnButtonClick += delegate
                        {
                            ChoiceMode(file, null);
                        };
                        item.Title = root.Attributes["name"].InnerText;
                        item.Description = $"{root.Attributes["user"].InnerText} {root.Attributes["model"].InnerText} {root.Attributes["date"].InnerText}";

                        itemsPnl.Controls.Add(item);
                        item.BringToFront();
                    }
                }
            }

            newItemBtn.Text = "Nova Missão";
            logoMainPbx.Left = 80;
            backBtn.Visible = true;
        }

        private MissionOpions options;
        private void ChoiceMode(string filePath, string name)
        {
            options = new MissionOpions
            {
                Dock = DockStyle.Fill
            };
            mainPnl.Controls.Add(options);
            options.BringToFront();

            //
            options.DrawButton += delegate
            {
                OpenMission("DRAW", filePath, name);
            };
            options.MountButton += delegate
            {
                OpenMission("MOUNT", filePath, name);
            };
        }

        private void OpenMission(string mode, string filePath, string name)
        {
            newMission = new NewMission();
            newMission.SetMap(mode, mapView);

            if (filePath != null)
                newMission.OpenMission(filePath);

            if (name != null)
                newMission.SetMissionName(name);

            mainPnl.Controls.Add(newMission);
            newMission.Dock = DockStyle.Fill;
            newMission.BringToFront();

            newMission.FileIsSaved = true;

            options.Dispose();
            options = null;
        }
        #endregion

        #region Botões
        private NewMission newMission;
        private void OnClick_newItemBtn(object sender, EventArgs e)
        {
            bool isUserActive = IsCurrentUserActive();

            InputName inputName = new InputName
            {
                Text = $"Nome {(isUserActive ? "da Missão" : "do Usuário")}"
            };

            if (inputName.ShowDialog() == DialogResult.OK)
            {
                if (isUserActive)
                    ChoiceMode(null, inputName.Value);
                else
                {
                    Directory.CreateDirectory($".\\users\\{inputName.Value}");

                    RefreshList();
                }
            }
        }

        private void OnClick_backBtn(object sender, EventArgs e)
        {
            if (options != null)
            {
                options.Dispose();

                options = null;
            }
            else if (newMission != null)
            {
                //Perguntar primeiro
                if (!newMission.FileIsSaved)
                    if (MessageBox.Show("Deseja salvar antes de voltar?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        newMission.SaveMission();
                        newMission.SaveImages();
                    }

                newMission.Exit();

                newMission = null;
            }
            else if (IsCurrentUserActive())
                SetCurrentUser(null);

            RefreshList();
        }

        private void OnClick_settingsBtn(object sender, EventArgs e)
        {
            new Settings().ShowDialog();
        }

        private void OnClick_helpBtn(object sender, EventArgs e)
        {
            new HelpForm().ShowDialog();
        }
        #endregion

        #region Eventos
        private void OnClosing(object sender, FormClosingEventArgs e)
        {
            if (newMission != null)
            {
                //Perguntar primeiro
                if (!newMission.FileIsSaved)
                    if (MessageBox.Show("Deseja salvar a missão antes de voltar?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        newMission.SaveMission();
            }

            //Apagar todos os arquivos temporários
            TempUtil.ClearTemp();
        }
        #endregion

    }
}
using AgroVision.Forms;
using AgroVision.Views;
using System.Drawing;
using System.Windows.Forms;

namespace AgroVision.Utils
{
    public static class ControlUtil
    {

        public static void RegisterNotifications(Control parent)
        {
            NotificationManager.NewNotification += (o, args) =>
            {
                if (args.Type != -1)
                {
                    Panel pnl = new Panel
                    {
                        Height = 40,
                        Dock = DockStyle.Top
                    };

                    PictureBox pic = new PictureBox
                    {
                        Left = 3,
                        Top = 3,
                        Size = new Size(32, 32)
                    };
                    switch (args.Type)
                    {
                        case 0:
                            pic.Image = Properties.Resources.success;
                            pnl.BackColor = Color.Green;
                            break;
                        case 1:
                            pic.Image = Properties.Resources.info;
                            break;
                        case 2:
                            pic.Image = Properties.Resources.warning;
                            pnl.BackColor = Color.Orange;
                            break;
                        case 3:
                            pic.Image = Properties.Resources.error;
                            pnl.BackColor = Color.Maroon;
                            break;
                    }

                    pnl.Controls.Add(pic);

                    Label titleLabel = new Label
                    {
                        AutoSize = true,
                        ForeColor = Color.White,
                        Text = args.Title,
                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                        Left = 35,
                        Top = 2
                    };

                    Label descLabel = new Label
                    {
                        AutoSize = true,
                        ForeColor = Color.White,
                        Text = args.Description,
                        Font = new Font("Segoe UI", 8, FontStyle.Regular),
                        Left = 36,
                        Top = 19
                    };

                    pnl.Controls.Add(titleLabel);
                    pnl.Controls.Add(descLabel);

                    parent.Controls.Add(pnl);
                }
                else
                    parent.Controls.Clear();
            };
        }

        public static void AddCustomField(CustomControl custom)
        {
            BoxInfoView box = new BoxInfoView
            {
                Dock = DockStyle.Top
            };

            if (custom.Icon != null)
                box.Icon = custom.Icon;

            box.Title = custom.Title + (!string.IsNullOrWhiteSpace(custom.Help) ? "*" : "");

            //Ajuda
            if (!string.IsNullOrWhiteSpace(custom.Help))
            {
                ToolTip descTip = new ToolTip();
                descTip.SetToolTip(box.TitleControl, custom.Help);
                descTip.AutoPopDelay = 10000;
            }

            box.AddControl(custom.Control);

            custom.Parent.Controls.Add(box);
            box.BringToFront();
        }

        public static BoxInfoView AddSession(string title, Image icon, Control parent, params CustomControl[] controls)
        {
            Panel expandPanel = new Panel
            {
                Visible = false,
                Dock = DockStyle.Top,
                AutoSize = true
            };

            for (int controlIndex = 0; controlIndex < controls.Length; controlIndex++)
            {
                CustomControl custom = controls[controlIndex];
                custom.Parent = expandPanel;
                AddCustomField(custom);
            }

            BoxInfoView topBoxInfoView = new BoxInfoView
            {
                ActionIcon = Properties.Resources.expand
            };
            topBoxInfoView.OnPressActionButton += delegate
            {
                expandPanel.Visible = !expandPanel.Visible;
                if (expandPanel.Visible)
                    topBoxInfoView.ActionIcon = Properties.Resources.collapse;
                else
                    topBoxInfoView.ActionIcon = Properties.Resources.expand;
            };
            topBoxInfoView.Dock = DockStyle.Top;
            topBoxInfoView.Icon = icon;
            topBoxInfoView.Title = title;
            topBoxInfoView.Height = 50;

            expandPanel.Controls.Add(topBoxInfoView);

            parent.Controls.Add(topBoxInfoView);
            topBoxInfoView.BringToFront();
            parent.Controls.Add(expandPanel);
            expandPanel.BringToFront();

            return topBoxInfoView;
        }

    }
}
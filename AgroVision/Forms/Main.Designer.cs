namespace AgroVision
{
    partial class Main
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">verdade se for necessário descartar os recursos gerenciados; caso contrário, falso.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte do Designer - não modifique
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.mainPnl = new System.Windows.Forms.Panel();
            this.itemsPnl = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.helpBtn = new System.Windows.Forms.Button();
            this.notificationPnl = new System.Windows.Forms.Panel();
            this.settingsBtn = new System.Windows.Forms.Button();
            this.logoMainPbx = new System.Windows.Forms.PictureBox();
            this.backBtn = new System.Windows.Forms.Button();
            this.mapView = new AgroVision.MapView();
            this.newItemBtn = new AgroVision.CustomButton();
            this.mainPnl.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoMainPbx)).BeginInit();
            this.SuspendLayout();
            // 
            // mainPnl
            // 
            this.mainPnl.Controls.Add(this.itemsPnl);
            this.mainPnl.Controls.Add(this.newItemBtn);
            this.mainPnl.Dock = System.Windows.Forms.DockStyle.Left;
            this.mainPnl.Location = new System.Drawing.Point(0, 49);
            this.mainPnl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mainPnl.Name = "mainPnl";
            this.mainPnl.Size = new System.Drawing.Size(440, 765);
            this.mainPnl.TabIndex = 0;
            // 
            // itemsPnl
            // 
            this.itemsPnl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.itemsPnl.AutoScroll = true;
            this.itemsPnl.Location = new System.Drawing.Point(20, 47);
            this.itemsPnl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.itemsPnl.Name = "itemsPnl";
            this.itemsPnl.Size = new System.Drawing.Size(400, 706);
            this.itemsPnl.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.Controls.Add(this.helpBtn);
            this.panel3.Controls.Add(this.notificationPnl);
            this.panel3.Controls.Add(this.settingsBtn);
            this.panel3.Controls.Add(this.logoMainPbx);
            this.panel3.Controls.Add(this.backBtn);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(989, 49);
            this.panel3.TabIndex = 4;
            // 
            // helpBtn
            // 
            this.helpBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.helpBtn.BackgroundImage = global::AgroVision.Properties.Resources.help;
            this.helpBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.helpBtn.FlatAppearance.BorderSize = 0;
            this.helpBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.helpBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpBtn.ForeColor = System.Drawing.Color.White;
            this.helpBtn.Location = new System.Drawing.Point(895, 6);
            this.helpBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.helpBtn.Name = "helpBtn";
            this.helpBtn.Size = new System.Drawing.Size(40, 37);
            this.helpBtn.TabIndex = 27;
            this.helpBtn.UseVisualStyleBackColor = false;
            this.helpBtn.Click += new System.EventHandler(this.OnClick_helpBtn);
            // 
            // notificationPnl
            // 
            this.notificationPnl.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.notificationPnl.AutoScroll = true;
            this.notificationPnl.Location = new System.Drawing.Point(245, 0);
            this.notificationPnl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.notificationPnl.Name = "notificationPnl";
            this.notificationPnl.Size = new System.Drawing.Size(587, 49);
            this.notificationPnl.TabIndex = 26;
            // 
            // settingsBtn
            // 
            this.settingsBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.settingsBtn.BackgroundImage = global::AgroVision.Properties.Resources.settings;
            this.settingsBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.settingsBtn.FlatAppearance.BorderSize = 0;
            this.settingsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settingsBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingsBtn.ForeColor = System.Drawing.Color.White;
            this.settingsBtn.Location = new System.Drawing.Point(943, 6);
            this.settingsBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.settingsBtn.Name = "settingsBtn";
            this.settingsBtn.Size = new System.Drawing.Size(40, 37);
            this.settingsBtn.TabIndex = 25;
            this.settingsBtn.UseVisualStyleBackColor = false;
            this.settingsBtn.Click += new System.EventHandler(this.OnClick_settingsBtn);
            // 
            // logoMainPbx
            // 
            this.logoMainPbx.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("logoMainPbx.BackgroundImage")));
            this.logoMainPbx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.logoMainPbx.Dock = System.Windows.Forms.DockStyle.Left;
            this.logoMainPbx.Location = new System.Drawing.Point(100, 0);
            this.logoMainPbx.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.logoMainPbx.Name = "logoMainPbx";
            this.logoMainPbx.Size = new System.Drawing.Size(67, 49);
            this.logoMainPbx.TabIndex = 14;
            this.logoMainPbx.TabStop = false;
            // 
            // backBtn
            // 
            this.backBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.backBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("backBtn.BackgroundImage")));
            this.backBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.backBtn.Dock = System.Windows.Forms.DockStyle.Left;
            this.backBtn.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.backBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backBtn.Location = new System.Drawing.Point(0, 0);
            this.backBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(100, 49);
            this.backBtn.TabIndex = 0;
            this.backBtn.UseVisualStyleBackColor = false;
            this.backBtn.Click += new System.EventHandler(this.OnClick_backBtn);
            // 
            // mapView
            // 
            this.mapView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapView.Location = new System.Drawing.Point(440, 49);
            this.mapView.Margin = new System.Windows.Forms.Padding(5);
            this.mapView.Name = "mapView";
            this.mapView.Size = new System.Drawing.Size(549, 765);
            this.mapView.TabIndex = 5;
            // 
            // newItemBtn
            // 
            this.newItemBtn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.newItemBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(155)))), ((int)(((byte)(219)))));
            this.newItemBtn.FlatAppearance.BorderSize = 0;
            this.newItemBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.newItemBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.newItemBtn.ForeColor = System.Drawing.Color.White;
            this.newItemBtn.Location = new System.Drawing.Point(20, 11);
            this.newItemBtn.Margin = new System.Windows.Forms.Padding(4);
            this.newItemBtn.Name = "newItemBtn";
            this.newItemBtn.Size = new System.Drawing.Size(400, 28);
            this.newItemBtn.TabIndex = 3;
            this.newItemBtn.Text = "Novo";
            this.newItemBtn.UseVisualStyleBackColor = false;
            this.newItemBtn.Click += new System.EventHandler(this.OnClick_newItemBtn);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(989, 814);
            this.Controls.Add(this.mapView);
            this.Controls.Add(this.mainPnl);
            this.Controls.Add(this.panel3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(941, 849);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AgroVision";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
            this.mainPnl.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logoMainPbx)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPnl;
        private System.Windows.Forms.PictureBox logoMainPbx;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button backBtn;
        private System.Windows.Forms.Button settingsBtn;
        private System.Windows.Forms.Panel notificationPnl;
        private CustomButton newItemBtn;
        private System.Windows.Forms.Panel itemsPnl;
        private System.Windows.Forms.Button helpBtn;
        private MapView mapView;
    }
}


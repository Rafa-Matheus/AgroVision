namespace AgroVision.Forms
{
    partial class NewMission
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

        #region código gerado pelo Component Designer

        /// <summary> 
        /// Método necessário para o suporte do Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.settings = new System.Windows.Forms.Panel();
            this.topPnl = new System.Windows.Forms.Panel();
            this.infoFlp = new System.Windows.Forms.FlowLayoutPanel();
            this.namePnl = new System.Windows.Forms.Panel();
            this.nameTbx = new System.Windows.Forms.TextBox();
            this.nameLbl = new System.Windows.Forms.Label();
            this.droneInfoBx = new AgroVision.Forms.BoxInfoView();
            this.etaBx = new AgroVision.Forms.BoxInfoView();
            this.reqBatBx = new AgroVision.Forms.BoxInfoView();
            this.gsdBx = new AgroVision.Forms.BoxInfoView();
            this.areaBx = new AgroVision.Forms.BoxInfoView();
            this.topPnl.SuspendLayout();
            this.infoFlp.SuspendLayout();
            this.namePnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // settings
            // 
            this.settings.AutoScroll = true;
            this.settings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settings.Location = new System.Drawing.Point(0, 440);
            this.settings.Margin = new System.Windows.Forms.Padding(4);
            this.settings.Name = "settings";
            this.settings.Size = new System.Drawing.Size(440, 126);
            this.settings.TabIndex = 6;
            // 
            // topPnl
            // 
            this.topPnl.Controls.Add(this.infoFlp);
            this.topPnl.Controls.Add(this.namePnl);
            this.topPnl.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPnl.Location = new System.Drawing.Point(0, 0);
            this.topPnl.Name = "topPnl";
            this.topPnl.Size = new System.Drawing.Size(440, 440);
            this.topPnl.TabIndex = 7;
            // 
            // infoFlp
            // 
            this.infoFlp.Controls.Add(this.droneInfoBx);
            this.infoFlp.Controls.Add(this.etaBx);
            this.infoFlp.Controls.Add(this.reqBatBx);
            this.infoFlp.Controls.Add(this.gsdBx);
            this.infoFlp.Controls.Add(this.areaBx);
            this.infoFlp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoFlp.Location = new System.Drawing.Point(0, 95);
            this.infoFlp.Margin = new System.Windows.Forms.Padding(0);
            this.infoFlp.Name = "infoFlp";
            this.infoFlp.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.infoFlp.Size = new System.Drawing.Size(440, 345);
            this.infoFlp.TabIndex = 5;
            // 
            // namePnl
            // 
            this.namePnl.Controls.Add(this.nameTbx);
            this.namePnl.Controls.Add(this.nameLbl);
            this.namePnl.Dock = System.Windows.Forms.DockStyle.Top;
            this.namePnl.Location = new System.Drawing.Point(0, 0);
            this.namePnl.Name = "namePnl";
            this.namePnl.Size = new System.Drawing.Size(440, 95);
            this.namePnl.TabIndex = 6;
            // 
            // nameTbx
            // 
            this.nameTbx.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nameTbx.BackColor = System.Drawing.Color.Black;
            this.nameTbx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nameTbx.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameTbx.ForeColor = System.Drawing.Color.White;
            this.nameTbx.Location = new System.Drawing.Point(15, 47);
            this.nameTbx.Margin = new System.Windows.Forms.Padding(4);
            this.nameTbx.Name = "nameTbx";
            this.nameTbx.Size = new System.Drawing.Size(411, 29);
            this.nameTbx.TabIndex = 6;
            // 
            // nameLbl
            // 
            this.nameLbl.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nameLbl.AutoSize = true;
            this.nameLbl.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLbl.ForeColor = System.Drawing.Color.White;
            this.nameLbl.Location = new System.Drawing.Point(14, 19);
            this.nameLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.nameLbl.Name = "nameLbl";
            this.nameLbl.Size = new System.Drawing.Size(67, 25);
            this.nameLbl.TabIndex = 5;
            this.nameLbl.Text = "Nome:";
            // 
            // droneInfoBx
            // 
            this.droneInfoBx.ActionIcon = global::AgroVision.Properties.Resources.more;
            this.droneInfoBx.BackColor = System.Drawing.Color.Transparent;
            this.droneInfoBx.Description = null;
            this.droneInfoBx.Icon = global::AgroVision.Properties.Resources.drone;
            this.droneInfoBx.Location = new System.Drawing.Point(20, 5);
            this.droneInfoBx.Margin = new System.Windows.Forms.Padding(5);
            this.droneInfoBx.Name = "droneInfoBx";
            this.droneInfoBx.SideButtonsVisible = false;
            this.droneInfoBx.Size = new System.Drawing.Size(392, 92);
            this.droneInfoBx.TabIndex = 0;
            this.droneInfoBx.Title = "Drone";
            // 
            // etaBx
            // 
            this.etaBx.ActionIcon = null;
            this.etaBx.BackColor = System.Drawing.Color.Transparent;
            this.etaBx.Description = null;
            this.etaBx.Icon = global::AgroVision.Properties.Resources.tempo;
            this.etaBx.Location = new System.Drawing.Point(20, 107);
            this.etaBx.Margin = new System.Windows.Forms.Padding(5);
            this.etaBx.Name = "etaBx";
            this.etaBx.SideButtonsVisible = false;
            this.etaBx.Size = new System.Drawing.Size(192, 101);
            this.etaBx.TabIndex = 1;
            this.etaBx.Title = "Tempo Estim.";
            // 
            // reqBatBx
            // 
            this.reqBatBx.ActionIcon = null;
            this.reqBatBx.BackColor = System.Drawing.Color.Transparent;
            this.reqBatBx.Description = null;
            this.reqBatBx.Icon = global::AgroVision.Properties.Resources.bateria;
            this.reqBatBx.Location = new System.Drawing.Point(222, 107);
            this.reqBatBx.Margin = new System.Windows.Forms.Padding(5);
            this.reqBatBx.Name = "reqBatBx";
            this.reqBatBx.SideButtonsVisible = false;
            this.reqBatBx.Size = new System.Drawing.Size(192, 101);
            this.reqBatBx.TabIndex = 2;
            this.reqBatBx.Title = "Baterias Nec.";
            // 
            // gsdBx
            // 
            this.gsdBx.ActionIcon = global::AgroVision.Properties.Resources.more;
            this.gsdBx.BackColor = System.Drawing.Color.Transparent;
            this.gsdBx.Description = null;
            this.gsdBx.Icon = global::AgroVision.Properties.Resources.gsd;
            this.gsdBx.Location = new System.Drawing.Point(20, 218);
            this.gsdBx.Margin = new System.Windows.Forms.Padding(5);
            this.gsdBx.Name = "gsdBx";
            this.gsdBx.SideButtonsVisible = false;
            this.gsdBx.Size = new System.Drawing.Size(192, 101);
            this.gsdBx.TabIndex = 3;
            this.gsdBx.Title = "GSD";
            // 
            // areaBx
            // 
            this.areaBx.ActionIcon = null;
            this.areaBx.BackColor = System.Drawing.Color.Transparent;
            this.areaBx.Description = null;
            this.areaBx.Icon = global::AgroVision.Properties.Resources.area;
            this.areaBx.Location = new System.Drawing.Point(222, 218);
            this.areaBx.Margin = new System.Windows.Forms.Padding(5);
            this.areaBx.Name = "areaBx";
            this.areaBx.SideButtonsVisible = false;
            this.areaBx.Size = new System.Drawing.Size(192, 101);
            this.areaBx.TabIndex = 4;
            this.areaBx.Title = "Área";
            // 
            // NewMission
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.settings);
            this.Controls.Add(this.topPnl);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "NewMission";
            this.Size = new System.Drawing.Size(440, 566);
            this.topPnl.ResumeLayout(false);
            this.infoFlp.ResumeLayout(false);
            this.namePnl.ResumeLayout(false);
            this.namePnl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel settings;
        private System.Windows.Forms.Panel topPnl;
        private System.Windows.Forms.FlowLayoutPanel infoFlp;
        private BoxInfoView droneInfoBx;
        private BoxInfoView etaBx;
        private BoxInfoView reqBatBx;
        private BoxInfoView gsdBx;
        private BoxInfoView areaBx;
        private System.Windows.Forms.Panel namePnl;
        private System.Windows.Forms.TextBox nameTbx;
        private System.Windows.Forms.Label nameLbl;
    }
}

namespace AgroVision.Forms
{
    partial class MissionOpions
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.optionsFlp = new System.Windows.Forms.FlowLayoutPanel();
            this.drawBtn = new AgroVision.CustomButton();
            this.mountMapBtn = new AgroVision.CustomButton();
            this.optionsFlp.SuspendLayout();
            this.SuspendLayout();
            // 
            // optionsFlp
            // 
            this.optionsFlp.Controls.Add(this.drawBtn);
            this.optionsFlp.Controls.Add(this.mountMapBtn);
            this.optionsFlp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optionsFlp.Location = new System.Drawing.Point(0, 0);
            this.optionsFlp.Name = "optionsFlp";
            this.optionsFlp.Padding = new System.Windows.Forms.Padding(10);
            this.optionsFlp.Size = new System.Drawing.Size(330, 460);
            this.optionsFlp.TabIndex = 0;
            // 
            // drawBtn
            // 
            this.drawBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(155)))), ((int)(((byte)(219)))));
            this.drawBtn.BackgroundImage = global::AgroVision.Properties.Resources.desenhar_mapa;
            this.drawBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.drawBtn.FlatAppearance.BorderSize = 0;
            this.drawBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.drawBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.drawBtn.ForeColor = System.Drawing.Color.White;
            this.drawBtn.Location = new System.Drawing.Point(13, 13);
            this.drawBtn.Name = "drawBtn";
            this.drawBtn.Padding = new System.Windows.Forms.Padding(3);
            this.drawBtn.Size = new System.Drawing.Size(100, 100);
            this.drawBtn.TabIndex = 0;
            this.drawBtn.Text = "Plano de Voo";
            this.drawBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.drawBtn.UseVisualStyleBackColor = false;
            this.drawBtn.Click += new System.EventHandler(this.DrawBtn_Click);
            // 
            // mountMapBtn
            // 
            this.mountMapBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(155)))), ((int)(((byte)(219)))));
            this.mountMapBtn.BackgroundImage = global::AgroVision.Properties.Resources.montar;
            this.mountMapBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.mountMapBtn.FlatAppearance.BorderSize = 0;
            this.mountMapBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mountMapBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.mountMapBtn.ForeColor = System.Drawing.Color.White;
            this.mountMapBtn.Location = new System.Drawing.Point(119, 13);
            this.mountMapBtn.Name = "mountMapBtn";
            this.mountMapBtn.Padding = new System.Windows.Forms.Padding(3);
            this.mountMapBtn.Size = new System.Drawing.Size(100, 100);
            this.mountMapBtn.TabIndex = 1;
            this.mountMapBtn.Text = "Montar Mapa";
            this.mountMapBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.mountMapBtn.UseVisualStyleBackColor = false;
            this.mountMapBtn.Click += new System.EventHandler(this.MountMapBtn_Click);
            // 
            // MissionOpions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.optionsFlp);
            this.Name = "MissionOpions";
            this.Size = new System.Drawing.Size(330, 460);
            this.optionsFlp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel optionsFlp;
        private CustomButton drawBtn;
        private CustomButton mountMapBtn;
    }
}

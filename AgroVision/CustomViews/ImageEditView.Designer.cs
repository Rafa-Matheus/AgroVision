namespace AgroVision.CustomViews
{
    partial class ImageEditView
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
            this.titleLbl = new System.Windows.Forms.Label();
            this.geoLbl = new System.Windows.Forms.Label();
            this.rotateBtn = new System.Windows.Forms.Button();
            this.downBtn = new System.Windows.Forms.Button();
            this.upBtn = new System.Windows.Forms.Button();
            this.deleteBtn = new System.Windows.Forms.Button();
            this.showHideBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // titleLbl
            // 
            this.titleLbl.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.titleLbl.AutoSize = true;
            this.titleLbl.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLbl.ForeColor = System.Drawing.Color.White;
            this.titleLbl.Location = new System.Drawing.Point(139, 14);
            this.titleLbl.Name = "titleLbl";
            this.titleLbl.Size = new System.Drawing.Size(0, 21);
            this.titleLbl.TabIndex = 2;
            // 
            // geoLbl
            // 
            this.geoLbl.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.geoLbl.AutoSize = true;
            this.geoLbl.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.geoLbl.ForeColor = System.Drawing.Color.Gray;
            this.geoLbl.Location = new System.Drawing.Point(139, 40);
            this.geoLbl.Name = "geoLbl";
            this.geoLbl.Size = new System.Drawing.Size(0, 17);
            this.geoLbl.TabIndex = 3;
            // 
            // rotateBtn
            // 
            this.rotateBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rotateBtn.ForeColor = System.Drawing.Color.Transparent;
            this.rotateBtn.Location = new System.Drawing.Point(75, 24);
            this.rotateBtn.Name = "rotateBtn";
            this.rotateBtn.Size = new System.Drawing.Size(39, 23);
            this.rotateBtn.TabIndex = 4;
            this.rotateBtn.Text = "Girar";
            this.rotateBtn.UseVisualStyleBackColor = true;
            this.rotateBtn.Click += new System.EventHandler(this.OnClick_rotateBtn);
            // 
            // downBtn
            // 
            this.downBtn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.downBtn.BackgroundImage = global::AgroVision.Properties.Resources.down;
            this.downBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.downBtn.FlatAppearance.BorderSize = 0;
            this.downBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.downBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.downBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.downBtn.ForeColor = System.Drawing.Color.White;
            this.downBtn.Location = new System.Drawing.Point(371, 20);
            this.downBtn.Name = "downBtn";
            this.downBtn.Size = new System.Drawing.Size(30, 30);
            this.downBtn.TabIndex = 6;
            this.downBtn.UseVisualStyleBackColor = true;
            this.downBtn.Click += new System.EventHandler(this.OnClick_downBtn);
            // 
            // upBtn
            // 
            this.upBtn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.upBtn.BackgroundImage = global::AgroVision.Properties.Resources.up;
            this.upBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.upBtn.FlatAppearance.BorderSize = 0;
            this.upBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.upBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.upBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.upBtn.ForeColor = System.Drawing.Color.White;
            this.upBtn.Location = new System.Drawing.Point(335, 20);
            this.upBtn.Name = "upBtn";
            this.upBtn.Size = new System.Drawing.Size(30, 30);
            this.upBtn.TabIndex = 5;
            this.upBtn.UseVisualStyleBackColor = true;
            this.upBtn.Click += new System.EventHandler(this.OnClick_upBtn);
            // 
            // deleteBtn
            // 
            this.deleteBtn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.deleteBtn.BackgroundImage = global::AgroVision.Properties.Resources.excluir;
            this.deleteBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.deleteBtn.FlatAppearance.BorderSize = 0;
            this.deleteBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.deleteBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.deleteBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteBtn.ForeColor = System.Drawing.Color.White;
            this.deleteBtn.Location = new System.Drawing.Point(39, 20);
            this.deleteBtn.Name = "deleteBtn";
            this.deleteBtn.Size = new System.Drawing.Size(30, 30);
            this.deleteBtn.TabIndex = 1;
            this.deleteBtn.UseVisualStyleBackColor = true;
            this.deleteBtn.Click += new System.EventHandler(this.OnClick_deleteBtn);
            // 
            // showHideBtn
            // 
            this.showHideBtn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.showHideBtn.BackgroundImage = global::AgroVision.Properties.Resources.mostrar;
            this.showHideBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.showHideBtn.FlatAppearance.BorderSize = 0;
            this.showHideBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.showHideBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.showHideBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showHideBtn.ForeColor = System.Drawing.Color.White;
            this.showHideBtn.Location = new System.Drawing.Point(3, 20);
            this.showHideBtn.Name = "showHideBtn";
            this.showHideBtn.Size = new System.Drawing.Size(30, 30);
            this.showHideBtn.TabIndex = 0;
            this.showHideBtn.UseVisualStyleBackColor = true;
            this.showHideBtn.Click += new System.EventHandler(this.OnClick_showHideBtn);
            // 
            // ImageEditView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.downBtn);
            this.Controls.Add(this.upBtn);
            this.Controls.Add(this.rotateBtn);
            this.Controls.Add(this.geoLbl);
            this.Controls.Add(this.titleLbl);
            this.Controls.Add(this.deleteBtn);
            this.Controls.Add(this.showHideBtn);
            this.Name = "ImageEditView";
            this.Size = new System.Drawing.Size(404, 70);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button showHideBtn;
        private System.Windows.Forms.Button deleteBtn;
        private System.Windows.Forms.Label titleLbl;
        private System.Windows.Forms.Label geoLbl;
        private System.Windows.Forms.Button rotateBtn;
        private System.Windows.Forms.Button downBtn;
        private System.Windows.Forms.Button upBtn;
    }
}

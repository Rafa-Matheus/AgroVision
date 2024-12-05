namespace AgroVision.CustomViews
{
    partial class ListItemView
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
            this.iconPbx = new System.Windows.Forms.PictureBox();
            this.titleLbl = new System.Windows.Forms.Label();
            this.descLbl = new System.Windows.Forms.Label();
            this.actionIconPbx = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.iconPbx)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.actionIconPbx)).BeginInit();
            this.SuspendLayout();
            // 
            // iconPbx
            // 
            this.iconPbx.Location = new System.Drawing.Point(19, 15);
            this.iconPbx.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.iconPbx.Name = "iconPbx";
            this.iconPbx.Size = new System.Drawing.Size(67, 62);
            this.iconPbx.TabIndex = 0;
            this.iconPbx.TabStop = false;
            this.iconPbx.Click += new System.EventHandler(this.OnClick);
            // 
            // titleLbl
            // 
            this.titleLbl.AutoSize = true;
            this.titleLbl.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLbl.ForeColor = System.Drawing.Color.White;
            this.titleLbl.Location = new System.Drawing.Point(93, 15);
            this.titleLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.titleLbl.Name = "titleLbl";
            this.titleLbl.Size = new System.Drawing.Size(0, 28);
            this.titleLbl.TabIndex = 1;
            this.titleLbl.Click += new System.EventHandler(this.OnClick);
            // 
            // descLbl
            // 
            this.descLbl.AutoSize = true;
            this.descLbl.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descLbl.ForeColor = System.Drawing.Color.DimGray;
            this.descLbl.Location = new System.Drawing.Point(93, 50);
            this.descLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.descLbl.Name = "descLbl";
            this.descLbl.Size = new System.Drawing.Size(0, 28);
            this.descLbl.TabIndex = 2;
            this.descLbl.Click += new System.EventHandler(this.OnClick);
            // 
            // actionIconPbx
            // 
            this.actionIconPbx.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.actionIconPbx.Image = global::AgroVision.Properties.Resources.more;
            this.actionIconPbx.Location = new System.Drawing.Point(317, 4);
            this.actionIconPbx.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.actionIconPbx.Name = "actionIconPbx";
            this.actionIconPbx.Size = new System.Drawing.Size(40, 37);
            this.actionIconPbx.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.actionIconPbx.TabIndex = 5;
            this.actionIconPbx.TabStop = false;
            this.actionIconPbx.Visible = false;
            this.actionIconPbx.Click += new System.EventHandler(this.OnActionClick);
            // 
            // ListItemView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.actionIconPbx);
            this.Controls.Add(this.descLbl);
            this.Controls.Add(this.titleLbl);
            this.Controls.Add(this.iconPbx);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ListItemView";
            this.Size = new System.Drawing.Size(361, 91);
            this.Click += new System.EventHandler(this.OnClick);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            ((System.ComponentModel.ISupportInitialize)(this.iconPbx)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.actionIconPbx)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox iconPbx;
        private System.Windows.Forms.Label titleLbl;
        private System.Windows.Forms.Label descLbl;
        private System.Windows.Forms.PictureBox actionIconPbx;
    }
}

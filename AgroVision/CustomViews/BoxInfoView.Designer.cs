namespace AgroVision.Forms
{
    partial class BoxInfoView
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
            this.rightBtn = new System.Windows.Forms.Button();
            this.leftBtn = new System.Windows.Forms.Button();
            this.actionIconPbx = new System.Windows.Forms.PictureBox();
            this.controlPnl = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.iconPbx = new System.Windows.Forms.PictureBox();
            this.titleLbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.actionIconPbx)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPbx)).BeginInit();
            this.SuspendLayout();
            // 
            // rightBtn
            // 
            this.rightBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.rightBtn.BackgroundImage = global::AgroVision.Properties.Resources.right;
            this.rightBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.rightBtn.FlatAppearance.BorderSize = 0;
            this.rightBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.rightBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.rightBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rightBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rightBtn.ForeColor = System.Drawing.Color.White;
            this.rightBtn.Location = new System.Drawing.Point(403, 70);
            this.rightBtn.Margin = new System.Windows.Forms.Padding(4);
            this.rightBtn.Name = "rightBtn";
            this.rightBtn.Size = new System.Drawing.Size(33, 55);
            this.rightBtn.TabIndex = 20;
            this.rightBtn.UseVisualStyleBackColor = true;
            this.rightBtn.Visible = false;
            this.rightBtn.Click += new System.EventHandler(this.OnClick_rightBtn);
            // 
            // leftBtn
            // 
            this.leftBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.leftBtn.BackgroundImage = global::AgroVision.Properties.Resources.left;
            this.leftBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.leftBtn.FlatAppearance.BorderSize = 0;
            this.leftBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.leftBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.leftBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.leftBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leftBtn.ForeColor = System.Drawing.Color.White;
            this.leftBtn.Location = new System.Drawing.Point(4, 70);
            this.leftBtn.Margin = new System.Windows.Forms.Padding(4);
            this.leftBtn.Name = "leftBtn";
            this.leftBtn.Size = new System.Drawing.Size(33, 55);
            this.leftBtn.TabIndex = 19;
            this.leftBtn.UseVisualStyleBackColor = true;
            this.leftBtn.Visible = false;
            this.leftBtn.Click += new System.EventHandler(this.OnClick_leftBtn);
            // 
            // actionIconPbx
            // 
            this.actionIconPbx.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.actionIconPbx.Location = new System.Drawing.Point(380, 5);
            this.actionIconPbx.Margin = new System.Windows.Forms.Padding(4);
            this.actionIconPbx.Name = "actionIconPbx";
            this.actionIconPbx.Size = new System.Drawing.Size(40, 37);
            this.actionIconPbx.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.actionIconPbx.TabIndex = 18;
            this.actionIconPbx.TabStop = false;
            this.actionIconPbx.Visible = false;
            this.actionIconPbx.Click += new System.EventHandler(this.OnClick_actionBtn);
            // 
            // controlPnl
            // 
            this.controlPnl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.controlPnl.Location = new System.Drawing.Point(20, 49);
            this.controlPnl.Margin = new System.Windows.Forms.Padding(4);
            this.controlPnl.Name = "controlPnl";
            this.controlPnl.Size = new System.Drawing.Size(400, 73);
            this.controlPnl.TabIndex = 17;
            this.controlPnl.Visible = false;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(197, 79);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 16);
            this.label2.TabIndex = 16;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // iconPbx
            // 
            this.iconPbx.Location = new System.Drawing.Point(20, 5);
            this.iconPbx.Margin = new System.Windows.Forms.Padding(4);
            this.iconPbx.Name = "iconPbx";
            this.iconPbx.Size = new System.Drawing.Size(40, 37);
            this.iconPbx.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.iconPbx.TabIndex = 14;
            this.iconPbx.TabStop = false;
            // 
            // titleLbl
            // 
            this.titleLbl.AutoSize = true;
            this.titleLbl.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLbl.ForeColor = System.Drawing.Color.White;
            this.titleLbl.Location = new System.Drawing.Point(59, 8);
            this.titleLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.titleLbl.Name = "titleLbl";
            this.titleLbl.Size = new System.Drawing.Size(0, 28);
            this.titleLbl.TabIndex = 15;
            // 
            // BoxInfoView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.rightBtn);
            this.Controls.Add(this.leftBtn);
            this.Controls.Add(this.actionIconPbx);
            this.Controls.Add(this.controlPnl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.iconPbx);
            this.Controls.Add(this.titleLbl);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "BoxInfoView";
            this.Size = new System.Drawing.Size(440, 130);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            ((System.ComponentModel.ISupportInitialize)(this.actionIconPbx)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPbx)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button rightBtn;
        private System.Windows.Forms.Button leftBtn;
        private System.Windows.Forms.PictureBox actionIconPbx;
        private System.Windows.Forms.Panel controlPnl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox iconPbx;
        private System.Windows.Forms.Label titleLbl;
    }
}

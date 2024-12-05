namespace AgroVision.Forms
{
    partial class SeekBarView
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
            this.valueTbx = new System.Windows.Forms.TextBox();
            this.colorBar = new AgroVision.ColorSlider();
            this.SuspendLayout();
            // 
            // valueTbx
            // 
            this.valueTbx.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.valueTbx.BackColor = System.Drawing.Color.Black;
            this.valueTbx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.valueTbx.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueTbx.ForeColor = System.Drawing.Color.White;
            this.valueTbx.Location = new System.Drawing.Point(244, 10);
            this.valueTbx.Name = "valueTbx";
            this.valueTbx.Size = new System.Drawing.Size(50, 25);
            this.valueTbx.TabIndex = 1;
            this.valueTbx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.valueTbx.TextChanged += new System.EventHandler(this.OnTextChanged);
            this.valueTbx.Leave += new System.EventHandler(this.OnMouseLeave);
            // 
            // colorBar
            // 
            this.colorBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.colorBar.BackColor = System.Drawing.Color.Transparent;
            this.colorBar.BarInnerColor = System.Drawing.Color.Black;
            this.colorBar.BarOuterColor = System.Drawing.Color.Black;
            this.colorBar.BorderRoundRectSize = new System.Drawing.Size(1, 1);
            this.colorBar.DrawSemitransparentThumb = false;
            this.colorBar.ElapsedInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(155)))), ((int)(((byte)(219)))));
            this.colorBar.ElapsedOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(155)))), ((int)(((byte)(219)))));
            this.colorBar.LargeChange = ((uint)(5u));
            this.colorBar.Location = new System.Drawing.Point(12, 14);
            this.colorBar.Name = "colorBar";
            this.colorBar.Size = new System.Drawing.Size(215, 17);
            this.colorBar.SmallChange = ((uint)(5u));
            this.colorBar.TabIndex = 2;
            this.colorBar.Text = "colorSlider";
            this.colorBar.ThumbInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(155)))), ((int)(((byte)(219)))));
            this.colorBar.ThumbOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(155)))), ((int)(((byte)(219)))));
            this.colorBar.ThumbPenColor = System.Drawing.Color.DarkSlateGray;
            this.colorBar.ThumbRoundRectSize = new System.Drawing.Size(15, 15);
            this.colorBar.Value = 0;
            this.colorBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.OnScroll);
            // 
            // SeekBarView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.colorBar);
            this.Controls.Add(this.valueTbx);
            this.Name = "SeekBarView";
            this.Size = new System.Drawing.Size(310, 45);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox valueTbx;
        private ColorSlider colorBar;
    }
}

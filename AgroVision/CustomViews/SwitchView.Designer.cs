namespace AgroVision.Forms
{
    partial class SwitchView
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
            this.colorBar = new AgroVision.ColorSlider();
            this.SuspendLayout();
            // 
            // colorBar
            // 
            this.colorBar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.colorBar.BackColor = System.Drawing.Color.Transparent;
            this.colorBar.BarInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.colorBar.BarOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.colorBar.BorderRoundRectSize = new System.Drawing.Size(1, 1);
            this.colorBar.DrawSemitransparentThumb = false;
            this.colorBar.ElapsedInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(155)))), ((int)(((byte)(219)))));
            this.colorBar.ElapsedOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(155)))), ((int)(((byte)(219)))));
            this.colorBar.LargeChange = ((uint)(5u));
            this.colorBar.Location = new System.Drawing.Point(130, 6);
            this.colorBar.Name = "colorBar";
            this.colorBar.Size = new System.Drawing.Size(50, 32);
            this.colorBar.SmallChange = ((uint)(5u));
            this.colorBar.TabIndex = 2;
            this.colorBar.Text = "colorSlider";
            this.colorBar.ThumbInnerColor = System.Drawing.Color.Gray;
            this.colorBar.ThumbOuterColor = System.Drawing.Color.Gray;
            this.colorBar.ThumbPenColor = System.Drawing.Color.Black;
            this.colorBar.ThumbRoundRectSize = new System.Drawing.Size(30, 30);
            this.colorBar.ThumbSize = 30;
            this.colorBar.Value = 0;
            this.colorBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.OnScroll);
            this.colorBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.colorBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            // 
            // SwitchView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.colorBar);
            this.Name = "SwitchView";
            this.Size = new System.Drawing.Size(310, 45);
            this.ResumeLayout(false);

        }

        #endregion

        private ColorSlider colorBar;
    }
}

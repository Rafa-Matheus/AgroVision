namespace AgroVision
{
    partial class FalseColors
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.blendPbx = new System.Windows.Forms.PictureBox();
            this.removeBtn = new AgroVision.CustomButton();
            this.addBtn = new AgroVision.CustomButton();
            this.applyBtn = new AgroVision.CustomButton();
            this.resetBtn = new AgroVision.CustomButton();
            this.warnLbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.blendPbx)).BeginInit();
            this.SuspendLayout();
            // 
            // blendPbx
            // 
            this.blendPbx.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.blendPbx.Location = new System.Drawing.Point(52, 65);
            this.blendPbx.Name = "blendPbx";
            this.blendPbx.Size = new System.Drawing.Size(681, 68);
            this.blendPbx.TabIndex = 0;
            this.blendPbx.TabStop = false;
            // 
            // removeBtn
            // 
            this.removeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.removeBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(155)))), ((int)(((byte)(219)))));
            this.removeBtn.FlatAppearance.BorderSize = 0;
            this.removeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.removeBtn.ForeColor = System.Drawing.Color.White;
            this.removeBtn.Location = new System.Drawing.Point(108, 226);
            this.removeBtn.Name = "removeBtn";
            this.removeBtn.Size = new System.Drawing.Size(90, 23);
            this.removeBtn.TabIndex = 4;
            this.removeBtn.Text = "Remover Cor";
            this.removeBtn.UseVisualStyleBackColor = false;
            this.removeBtn.Click += new System.EventHandler(this.OnClick_removeClickBtn);
            // 
            // addBtn
            // 
            this.addBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(155)))), ((int)(((byte)(219)))));
            this.addBtn.FlatAppearance.BorderSize = 0;
            this.addBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.addBtn.ForeColor = System.Drawing.Color.White;
            this.addBtn.Location = new System.Drawing.Point(12, 226);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(90, 23);
            this.addBtn.TabIndex = 3;
            this.addBtn.Text = "Adicionar Cor";
            this.addBtn.UseVisualStyleBackColor = false;
            this.addBtn.Click += new System.EventHandler(this.OnClick_addColorBtn);
            // 
            // applyBtn
            // 
            this.applyBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.applyBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(155)))), ((int)(((byte)(219)))));
            this.applyBtn.FlatAppearance.BorderSize = 0;
            this.applyBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.applyBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.applyBtn.ForeColor = System.Drawing.Color.White;
            this.applyBtn.Location = new System.Drawing.Point(682, 226);
            this.applyBtn.Name = "applyBtn";
            this.applyBtn.Size = new System.Drawing.Size(90, 23);
            this.applyBtn.TabIndex = 5;
            this.applyBtn.Text = "Aplicar";
            this.applyBtn.UseVisualStyleBackColor = false;
            this.applyBtn.Click += new System.EventHandler(this.OnClick_applyBtn);
            // 
            // resetBtn
            // 
            this.resetBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.resetBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(155)))), ((int)(((byte)(219)))));
            this.resetBtn.FlatAppearance.BorderSize = 0;
            this.resetBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.resetBtn.ForeColor = System.Drawing.Color.White;
            this.resetBtn.Location = new System.Drawing.Point(204, 226);
            this.resetBtn.Name = "resetBtn";
            this.resetBtn.Size = new System.Drawing.Size(90, 23);
            this.resetBtn.TabIndex = 6;
            this.resetBtn.Text = "Restaurar";
            this.resetBtn.UseVisualStyleBackColor = false;
            this.resetBtn.Click += new System.EventHandler(this.OnClick_resetBtn);
            // 
            // warnLbl
            // 
            this.warnLbl.AutoSize = true;
            this.warnLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.warnLbl.ForeColor = System.Drawing.Color.Silver;
            this.warnLbl.Location = new System.Drawing.Point(363, 198);
            this.warnLbl.MaximumSize = new System.Drawing.Size(250, 0);
            this.warnLbl.Name = "warnLbl";
            this.warnLbl.Size = new System.Drawing.Size(248, 51);
            this.warnLbl.TabIndex = 7;
            this.warnLbl.Text = "*Qualquer mudança na escala de cores pode influenciar na interpretação da saúde d" +
    "as plantas.";
            this.warnLbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // FalseColors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(784, 261);
            this.Controls.Add(this.warnLbl);
            this.Controls.Add(this.resetBtn);
            this.Controls.Add(this.applyBtn);
            this.Controls.Add(this.removeBtn);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.blendPbx);
            this.MinimumSize = new System.Drawing.Size(800, 300);
            this.Name = "FalseColors";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Editar Cores";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            this.DoubleClick += new System.EventHandler(this.OnDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.blendPbx)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox blendPbx;
        private CustomButton addBtn;
        private CustomButton removeBtn;
        private CustomButton applyBtn;
        private CustomButton resetBtn;
        private System.Windows.Forms.Label warnLbl;
    }
}
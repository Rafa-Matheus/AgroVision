namespace AgroVision
{
    partial class MetricsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MetricsForm));
            this.topPnl = new System.Windows.Forms.Panel();
            this.printBtn = new AgroVision.CustomButton();
            this.compareBtn = new AgroVision.CustomButton();
            this.compareTbl = new System.Windows.Forms.TableLayoutPanel();
            this.topPnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPnl
            // 
            this.topPnl.Controls.Add(this.printBtn);
            this.topPnl.Controls.Add(this.compareBtn);
            this.topPnl.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPnl.Location = new System.Drawing.Point(0, 0);
            this.topPnl.Name = "topPnl";
            this.topPnl.Size = new System.Drawing.Size(984, 112);
            this.topPnl.TabIndex = 2;
            this.topPnl.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            // 
            // printBtn
            // 
            this.printBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.printBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(155)))), ((int)(((byte)(219)))));
            this.printBtn.FlatAppearance.BorderSize = 0;
            this.printBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.printBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.printBtn.ForeColor = System.Drawing.Color.White;
            this.printBtn.Location = new System.Drawing.Point(120, 83);
            this.printBtn.Name = "printBtn";
            this.printBtn.Size = new System.Drawing.Size(111, 23);
            this.printBtn.TabIndex = 1;
            this.printBtn.Text = "Imprimir";
            this.printBtn.UseVisualStyleBackColor = false;
            this.printBtn.Click += new System.EventHandler(this.OnClick_printBtn);
            // 
            // compareBtn
            // 
            this.compareBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.compareBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(155)))), ((int)(((byte)(219)))));
            this.compareBtn.FlatAppearance.BorderSize = 0;
            this.compareBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.compareBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.compareBtn.ForeColor = System.Drawing.Color.White;
            this.compareBtn.Location = new System.Drawing.Point(3, 83);
            this.compareBtn.Name = "compareBtn";
            this.compareBtn.Size = new System.Drawing.Size(111, 23);
            this.compareBtn.TabIndex = 0;
            this.compareBtn.Text = "Comparar +";
            this.compareBtn.UseVisualStyleBackColor = false;
            this.compareBtn.Click += new System.EventHandler(this.OnClick_compareBtn);
            // 
            // compareTbl
            // 
            this.compareTbl.ColumnCount = 1;
            this.compareTbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.compareTbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.compareTbl.Location = new System.Drawing.Point(0, 112);
            this.compareTbl.Name = "compareTbl";
            this.compareTbl.RowCount = 1;
            this.compareTbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.compareTbl.Size = new System.Drawing.Size(984, 559);
            this.compareTbl.TabIndex = 3;
            // 
            // MetricsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(984, 671);
            this.Controls.Add(this.compareTbl);
            this.Controls.Add(this.topPnl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(520, 450);
            this.Name = "MetricsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Métricas";
            this.Resize += new System.EventHandler(this.OnResize);
            this.topPnl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel topPnl;
        private CustomButton compareBtn;
        private System.Windows.Forms.TableLayoutPanel compareTbl;
        private CustomButton printBtn;
    }
}
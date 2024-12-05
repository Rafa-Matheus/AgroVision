namespace AgroVision
{
    partial class Testing
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
            this.spacingTcb = new System.Windows.Forms.TrackBar();
            this.angleTcb = new System.Windows.Forms.TrackBar();
            this.drawCurvesChk = new System.Windows.Forms.CheckBox();
            this.tensionTcb = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.spacingTcb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.angleTcb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tensionTcb)).BeginInit();
            this.SuspendLayout();
            // 
            // spacingTcb
            // 
            this.spacingTcb.Location = new System.Drawing.Point(12, 31);
            this.spacingTcb.Maximum = 150;
            this.spacingTcb.Minimum = 20;
            this.spacingTcb.Name = "spacingTcb";
            this.spacingTcb.Size = new System.Drawing.Size(206, 45);
            this.spacingTcb.TabIndex = 0;
            this.spacingTcb.Value = 20;
            this.spacingTcb.Scroll += new System.EventHandler(this.OnScroll_trackBar);
            // 
            // angleTcb
            // 
            this.angleTcb.Location = new System.Drawing.Point(12, 109);
            this.angleTcb.Maximum = 270;
            this.angleTcb.Name = "angleTcb";
            this.angleTcb.Size = new System.Drawing.Size(206, 45);
            this.angleTcb.TabIndex = 1;
            this.angleTcb.Scroll += new System.EventHandler(this.OnScroll_trackBar);
            // 
            // drawCurvesChk
            // 
            this.drawCurvesChk.AutoSize = true;
            this.drawCurvesChk.Location = new System.Drawing.Point(12, 160);
            this.drawCurvesChk.Name = "drawCurvesChk";
            this.drawCurvesChk.Size = new System.Drawing.Size(108, 17);
            this.drawCurvesChk.TabIndex = 2;
            this.drawCurvesChk.Text = "Desenhar Curvas";
            this.drawCurvesChk.UseVisualStyleBackColor = true;
            this.drawCurvesChk.CheckedChanged += new System.EventHandler(this.OnScroll_trackBar);
            // 
            // tensionTcb
            // 
            this.tensionTcb.Location = new System.Drawing.Point(12, 222);
            this.tensionTcb.Maximum = 200;
            this.tensionTcb.Minimum = 1;
            this.tensionTcb.Name = "tensionTcb";
            this.tensionTcb.Size = new System.Drawing.Size(206, 45);
            this.tensionTcb.TabIndex = 3;
            this.tensionTcb.Value = 10;
            this.tensionTcb.Scroll += new System.EventHandler(this.OnScroll_trackBar);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Espaçamento:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Ângulo:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 206);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Tensão:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 612);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(167, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Contar Cores";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 583);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(167, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Abrir Imagem";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Testing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(995, 647);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tensionTcb);
            this.Controls.Add(this.drawCurvesChk);
            this.Controls.Add(this.angleTcb);
            this.Controls.Add(this.spacingTcb);
            this.Name = "Testing";
            this.Text = "Testing";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ondraw);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.spacingTcb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.angleTcb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tensionTcb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar spacingTcb;
        private System.Windows.Forms.TrackBar angleTcb;
        private System.Windows.Forms.CheckBox drawCurvesChk;
        private System.Windows.Forms.TrackBar tensionTcb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}
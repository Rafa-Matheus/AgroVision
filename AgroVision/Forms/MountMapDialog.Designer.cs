namespace AgroVision
{
    partial class MountMapDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MountMapDialog));
            this.label1 = new System.Windows.Forms.Label();
            this.fileBtn = new AgroVision.CustomButton();
            this.photosBtn = new AgroVision.CustomButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(27, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 21);
            this.label1.TabIndex = 4;
            this.label1.Text = "Como deseja começar?";
            // 
            // fileBtn
            // 
            this.fileBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(155)))), ((int)(((byte)(219)))));
            this.fileBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("fileBtn.BackgroundImage")));
            this.fileBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.fileBtn.FlatAppearance.BorderSize = 0;
            this.fileBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fileBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.fileBtn.ForeColor = System.Drawing.Color.White;
            this.fileBtn.Location = new System.Drawing.Point(165, 62);
            this.fileBtn.Name = "fileBtn";
            this.fileBtn.Padding = new System.Windows.Forms.Padding(3);
            this.fileBtn.Size = new System.Drawing.Size(128, 100);
            this.fileBtn.TabIndex = 3;
            this.fileBtn.Text = "Arquivo Pronto";
            this.fileBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.fileBtn.UseVisualStyleBackColor = false;
            this.fileBtn.Click += new System.EventHandler(this.FileBtn_Click);
            // 
            // photosBtn
            // 
            this.photosBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(155)))), ((int)(((byte)(219)))));
            this.photosBtn.BackgroundImage = global::AgroVision.Properties.Resources.montar;
            this.photosBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.photosBtn.FlatAppearance.BorderSize = 0;
            this.photosBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.photosBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.photosBtn.ForeColor = System.Drawing.Color.White;
            this.photosBtn.Location = new System.Drawing.Point(31, 62);
            this.photosBtn.Name = "photosBtn";
            this.photosBtn.Padding = new System.Windows.Forms.Padding(3);
            this.photosBtn.Size = new System.Drawing.Size(128, 100);
            this.photosBtn.TabIndex = 2;
            this.photosBtn.Text = "Arquivo(s) TIFF";
            this.photosBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.photosBtn.UseVisualStyleBackColor = false;
            this.photosBtn.Click += new System.EventHandler(this.PhotosBtn_Click);
            // 
            // MountMapDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(325, 196);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fileBtn);
            this.Controls.Add(this.photosBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MountMapDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomButton photosBtn;
        private CustomButton fileBtn;
        private System.Windows.Forms.Label label1;
    }
}
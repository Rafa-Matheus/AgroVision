namespace AgroVision
{
    partial class MapView
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
            this.mapModeCbx = new System.Windows.Forms.ComboBox();
            this.searchBtn = new System.Windows.Forms.Button();
            this.googleMapsVw = new AgroVision.GoogleMapsView();
            this.SuspendLayout();
            // 
            // mapModeCbx
            // 
            this.mapModeCbx.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mapModeCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mapModeCbx.FormattingEnabled = true;
            this.mapModeCbx.Items.AddRange(new object[] {
            "Normal",
            "Terreno",
            "Satélite"});
            this.mapModeCbx.Location = new System.Drawing.Point(796, 721);
            this.mapModeCbx.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mapModeCbx.Name = "mapModeCbx";
            this.mapModeCbx.Size = new System.Drawing.Size(105, 24);
            this.mapModeCbx.TabIndex = 25;
            // 
            // searchBtn
            // 
            this.searchBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.searchBtn.Location = new System.Drawing.Point(705, 721);
            this.searchBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(83, 26);
            this.searchBtn.TabIndex = 27;
            this.searchBtn.Text = "Pesquisar";
            this.searchBtn.UseVisualStyleBackColor = true;
            this.searchBtn.Visible = false;
            this.searchBtn.Click += new System.EventHandler(this.OnClick_searchBtn);
            // 
            // googleMapsVw
            // 
            this.googleMapsVw.Bearing = 0F;
            this.googleMapsVw.CanDragMap = true;
            this.googleMapsVw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.googleMapsVw.EmptyTileColor = System.Drawing.Color.Navy;
            this.googleMapsVw.GrayScaleMode = false;
            this.googleMapsVw.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.googleMapsVw.LevelsKeepInMemory = 5;
            this.googleMapsVw.Location = new System.Drawing.Point(0, 0);
            this.googleMapsVw.LockMove = false;
            this.googleMapsVw.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.googleMapsVw.MarkersEnabled = false;
            this.googleMapsVw.MaxZoom = 24;
            this.googleMapsVw.MinZoom = 0;
            this.googleMapsVw.MouseWheelZoomEnabled = true;
            this.googleMapsVw.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionWithoutCenter;
            this.googleMapsVw.Name = "googleMapsVw";
            this.googleMapsVw.NegativeMode = false;
            this.googleMapsVw.PolygonsEnabled = true;
            this.googleMapsVw.RetryLoadTile = 0;
            this.googleMapsVw.RoutesEnabled = true;
            this.googleMapsVw.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.googleMapsVw.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.googleMapsVw.ShowTileGridLines = false;
            this.googleMapsVw.Size = new System.Drawing.Size(907, 751);
            this.googleMapsVw.TabIndex = 26;
            this.googleMapsVw.Zoom = 4D;
            // 
            // MapView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.searchBtn);
            this.Controls.Add(this.mapModeCbx);
            this.Controls.Add(this.googleMapsVw);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MapView";
            this.Size = new System.Drawing.Size(907, 751);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox mapModeCbx;
        private GoogleMapsView googleMapsVw;
        private System.Windows.Forms.Button searchBtn;
    }
}

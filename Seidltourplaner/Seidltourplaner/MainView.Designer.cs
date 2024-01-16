namespace Seidltourplaner
{
    partial class MainView
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
            this.BtnCalculateRoute = new System.Windows.Forms.Button();
            this.ClbStations = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CbStart = new System.Windows.Forms.ComboBox();
            this.map = new GMap.NET.WindowsForms.GMapControl();
            this.label3 = new System.Windows.Forms.Label();
            this.LblDistance = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnCalculateRoute
            // 
            this.BtnCalculateRoute.Location = new System.Drawing.Point(1696, 923);
            this.BtnCalculateRoute.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnCalculateRoute.Name = "BtnCalculateRoute";
            this.BtnCalculateRoute.Size = new System.Drawing.Size(541, 112);
            this.BtnCalculateRoute.TabIndex = 0;
            this.BtnCalculateRoute.Text = "Route berechnen";
            this.BtnCalculateRoute.UseVisualStyleBackColor = true;
            this.BtnCalculateRoute.Click += new System.EventHandler(this.BtnCalculateRoute_Click);
            // 
            // ClbStations
            // 
            this.ClbStations.FormattingEnabled = true;
            this.ClbStations.Location = new System.Drawing.Point(1696, 100);
            this.ClbStations.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ClbStations.Name = "ClbStations";
            this.ClbStations.Size = new System.Drawing.Size(535, 564);
            this.ClbStations.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1685, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(290, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "Zu besuchende Pubs:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1688, 756);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 32);
            this.label2.TabIndex = 3;
            this.label2.Text = "Startpunkt:";
            // 
            // CbStart
            // 
            this.CbStart.FormattingEnabled = true;
            this.CbStart.Location = new System.Drawing.Point(1693, 789);
            this.CbStart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CbStart.Name = "CbStart";
            this.CbStart.Size = new System.Drawing.Size(537, 39);
            this.CbStart.TabIndex = 5;
            this.CbStart.DropDown += new System.EventHandler(this.CbStart_DropDown);
            // 
            // map
            // 
            this.map.Bearing = 0F;
            this.map.CanDragMap = true;
            this.map.EmptyTileColor = System.Drawing.Color.Navy;
            this.map.GrayScaleMode = false;
            this.map.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.map.LevelsKeepInMemory = 5;
            this.map.Location = new System.Drawing.Point(11, 12);
            this.map.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.map.MarkersEnabled = true;
            this.map.MaxZoom = 2;
            this.map.MinZoom = 2;
            this.map.MouseWheelZoomEnabled = true;
            this.map.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.map.Name = "map";
            this.map.NegativeMode = false;
            this.map.PolygonsEnabled = true;
            this.map.RetryLoadTile = 0;
            this.map.RoutesEnabled = true;
            this.map.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.map.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.map.ShowTileGridLines = false;
            this.map.Size = new System.Drawing.Size(1645, 1111);
            this.map.TabIndex = 6;
            this.map.Zoom = 0D;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1691, 1068);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(302, 32);
            this.label3.TabIndex = 7;
            this.label3.Text = "Weg für alle Stationen:";
            // 
            // LblDistance
            // 
            this.LblDistance.AutoSize = true;
            this.LblDistance.Location = new System.Drawing.Point(2003, 1068);
            this.LblDistance.Name = "LblDistance";
            this.LblDistance.Size = new System.Drawing.Size(22, 32);
            this.LblDistance.TabIndex = 8;
            this.LblDistance.Text = "/";
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2264, 1149);
            this.Controls.Add(this.LblDistance);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.map);
            this.Controls.Add(this.CbStart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ClbStations);
            this.Controls.Add(this.BtnCalculateRoute);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainView";
            this.Text = "Seidltourplaner";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnCalculateRoute;
        private System.Windows.Forms.CheckedListBox ClbStations;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CbStart;
        private GMap.NET.WindowsForms.GMapControl map;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label LblDistance;
    }
}


﻿namespace Seidltourplaner
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
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.LB_StationsSequence = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // BtnCalculateRoute
            // 
            this.BtnCalculateRoute.Location = new System.Drawing.Point(658, 328);
            this.BtnCalculateRoute.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.BtnCalculateRoute.Name = "BtnCalculateRoute";
            this.BtnCalculateRoute.Size = new System.Drawing.Size(202, 47);
            this.BtnCalculateRoute.TabIndex = 0;
            this.BtnCalculateRoute.Text = "Route berechnen";
            this.BtnCalculateRoute.UseVisualStyleBackColor = true;
            this.BtnCalculateRoute.Click += new System.EventHandler(this.BtnCalculateRoute_Click);
            // 
            // ClbStations
            // 
            this.ClbStations.FormattingEnabled = true;
            this.ClbStations.Location = new System.Drawing.Point(658, 26);
            this.ClbStations.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.ClbStations.Name = "ClbStations";
            this.ClbStations.Size = new System.Drawing.Size(204, 244);
            this.ClbStations.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(653, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Zu besuchende Pubs:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(656, 281);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Startpunkt:";
            // 
            // CbStart
            // 
            this.CbStart.FormattingEnabled = true;
            this.CbStart.Location = new System.Drawing.Point(658, 295);
            this.CbStart.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.CbStart.Name = "CbStart";
            this.CbStart.Size = new System.Drawing.Size(204, 21);
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
            this.map.Location = new System.Drawing.Point(4, 5);
            this.map.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
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
            this.map.Size = new System.Drawing.Size(639, 559);
            this.map.TabIndex = 6;
            this.map.Zoom = 0D;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(656, 546);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Weg für alle Stationen:";
            // 
            // LblDistance
            // 
            this.LblDistance.AutoSize = true;
            this.LblDistance.Location = new System.Drawing.Point(773, 546);
            this.LblDistance.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblDistance.Name = "LblDistance";
            this.LblDistance.Size = new System.Drawing.Size(12, 13);
            this.LblDistance.TabIndex = 8;
            this.LblDistance.Text = "/";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 19);
            this.label4.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(656, 403);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Weg zum Ziel:";
            // 
            // LB_StationsSequence
            // 
            this.LB_StationsSequence.FormattingEnabled = true;
            this.LB_StationsSequence.Location = new System.Drawing.Point(658, 418);
            this.LB_StationsSequence.Margin = new System.Windows.Forms.Padding(1);
            this.LB_StationsSequence.Name = "LB_StationsSequence";
            this.LB_StationsSequence.Size = new System.Drawing.Size(204, 121);
            this.LB_StationsSequence.TabIndex = 11;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 574);
            this.Controls.Add(this.LB_StationsSequence);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.LblDistance);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.map);
            this.Controls.Add(this.CbStart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ClbStations);
            this.Controls.Add(this.BtnCalculateRoute);
            this.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.Name = "MainView";
            this.Text = "Seidltourplaner";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainView_FormClosed);
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox LB_StationsSequence;
    }
}


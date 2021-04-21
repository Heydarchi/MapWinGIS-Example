namespace MapWindow_HeatMap
{
    partial class FormMain
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
            this.panel_GIS = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelMouse = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBarlatlon = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnHand = new System.Windows.Forms.Button();
            this.tnZoomIn = new System.Windows.Forms.Button();
            this.btnZoomOut = new System.Windows.Forms.Button();
            this.btnPan = new System.Windows.Forms.Button();
            this.btnZoomExist = new System.Windows.Forms.Button();
            this.btnPolygon = new System.Windows.Forms.Button();
            this.btnAddIcon = new System.Windows.Forms.Button();
            this.btnTrack = new System.Windows.Forms.Button();
            this.btnScreenShot = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.networkSettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startStopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRuler = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_GIS
            // 
            this.panel_GIS.Location = new System.Drawing.Point(0, 27);
            this.panel_GIS.Name = "panel_GIS";
            this.panel_GIS.Size = new System.Drawing.Size(981, 544);
            this.panel_GIS.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelMouse,
            this.toolStripStatusLabel1,
            this.statusBarlatlon});
            this.statusStrip1.Location = new System.Drawing.Point(0, 576);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1044, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelMouse
            // 
            this.toolStripStatusLabelMouse.Name = "toolStripStatusLabelMouse";
            this.toolStripStatusLabelMouse.Size = new System.Drawing.Size(148, 17);
            this.toolStripStatusLabelMouse.Text = "toolStripStatusLabelMouse";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // statusBarlatlon
            // 
            this.statusBarlatlon.Name = "statusBarlatlon";
            this.statusBarlatlon.Size = new System.Drawing.Size(85, 17);
            this.statusBarlatlon.Text = "statusBarlatlon";
            // 
            // btnHand
            // 
            this.btnHand.Image = global::MapWindow_HeatMap.Properties.Resources.cursor;
            this.btnHand.Location = new System.Drawing.Point(987, 27);
            this.btnHand.Name = "btnHand";
            this.btnHand.Size = new System.Drawing.Size(45, 44);
            this.btnHand.TabIndex = 2;
            this.btnHand.UseVisualStyleBackColor = true;
            this.btnHand.Click += new System.EventHandler(this.btnHand_Click);
            // 
            // tnZoomIn
            // 
            this.tnZoomIn.Image = global::MapWindow_HeatMap.Properties.Resources.zoom_in;
            this.tnZoomIn.Location = new System.Drawing.Point(987, 128);
            this.tnZoomIn.Name = "tnZoomIn";
            this.tnZoomIn.Size = new System.Drawing.Size(45, 44);
            this.tnZoomIn.TabIndex = 2;
            this.tnZoomIn.UseVisualStyleBackColor = true;
            this.tnZoomIn.Click += new System.EventHandler(this.tnZoomIn_Click);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.Image = global::MapWindow_HeatMap.Properties.Resources.zoom_out;
            this.btnZoomOut.Location = new System.Drawing.Point(987, 178);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(45, 44);
            this.btnZoomOut.TabIndex = 2;
            this.btnZoomOut.UseVisualStyleBackColor = true;
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            // 
            // btnPan
            // 
            this.btnPan.Image = global::MapWindow_HeatMap.Properties.Resources.pan;
            this.btnPan.Location = new System.Drawing.Point(987, 78);
            this.btnPan.Name = "btnPan";
            this.btnPan.Size = new System.Drawing.Size(45, 44);
            this.btnPan.TabIndex = 2;
            this.btnPan.UseVisualStyleBackColor = true;
            this.btnPan.Click += new System.EventHandler(this.btnPan_Click);
            // 
            // btnZoomExist
            // 
            this.btnZoomExist.Image = global::MapWindow_HeatMap.Properties.Resources.zoom_more;
            this.btnZoomExist.Location = new System.Drawing.Point(987, 227);
            this.btnZoomExist.Name = "btnZoomExist";
            this.btnZoomExist.Size = new System.Drawing.Size(45, 44);
            this.btnZoomExist.TabIndex = 2;
            this.btnZoomExist.UseVisualStyleBackColor = true;
            this.btnZoomExist.Click += new System.EventHandler(this.btnZoomExist_Click);
            // 
            // btnPolygon
            // 
            this.btnPolygon.Image = global::MapWindow_HeatMap.Properties.Resources.icons8_Polygon_26;
            this.btnPolygon.Location = new System.Drawing.Point(987, 327);
            this.btnPolygon.Name = "btnPolygon";
            this.btnPolygon.Size = new System.Drawing.Size(45, 44);
            this.btnPolygon.TabIndex = 2;
            this.btnPolygon.UseVisualStyleBackColor = true;
            this.btnPolygon.Click += new System.EventHandler(this.btnPolygon_Click);
            // 
            // btnAddIcon
            // 
            this.btnAddIcon.Image = global::MapWindow_HeatMap.Properties.Resources._32_pin_white;
            this.btnAddIcon.Location = new System.Drawing.Point(987, 377);
            this.btnAddIcon.Name = "btnAddIcon";
            this.btnAddIcon.Size = new System.Drawing.Size(45, 44);
            this.btnAddIcon.TabIndex = 2;
            this.btnAddIcon.UseVisualStyleBackColor = true;
            this.btnAddIcon.Click += new System.EventHandler(this.btnAddIcon_Click);
            // 
            // btnTrack
            // 
            this.btnTrack.BackgroundImage = global::MapWindow_HeatMap.Properties.Resources.track;
            this.btnTrack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnTrack.Location = new System.Drawing.Point(987, 427);
            this.btnTrack.Name = "btnTrack";
            this.btnTrack.Size = new System.Drawing.Size(45, 44);
            this.btnTrack.TabIndex = 2;
            this.btnTrack.UseVisualStyleBackColor = true;
            this.btnTrack.Click += new System.EventHandler(this.btnTrack_Click);
            // 
            // btnScreenShot
            // 
            this.btnScreenShot.BackgroundImage = global::MapWindow_HeatMap.Properties.Resources.photo_camera;
            this.btnScreenShot.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnScreenShot.Location = new System.Drawing.Point(987, 477);
            this.btnScreenShot.Name = "btnScreenShot";
            this.btnScreenShot.Size = new System.Drawing.Size(45, 44);
            this.btnScreenShot.TabIndex = 2;
            this.btnScreenShot.UseVisualStyleBackColor = true;
            this.btnScreenShot.Click += new System.EventHandler(this.btnScreenShot_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackgroundImage = global::MapWindow_HeatMap.Properties.Resources.refresh_32;
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefresh.Location = new System.Drawing.Point(987, 527);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(45, 44);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingToolStripMenuItem,
            this.mapToolStripMenuItem,
            this.connectionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1044, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.networkSettingToolStripMenuItem});
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.settingToolStripMenuItem.Text = "Setting";
            // 
            // networkSettingToolStripMenuItem
            // 
            this.networkSettingToolStripMenuItem.Name = "networkSettingToolStripMenuItem";
            this.networkSettingToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.networkSettingToolStripMenuItem.Text = "Network Setting";
            // 
            // mapToolStripMenuItem
            // 
            this.mapToolStripMenuItem.Name = "mapToolStripMenuItem";
            this.mapToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.mapToolStripMenuItem.Text = "Map";
            // 
            // connectionToolStripMenuItem
            // 
            this.connectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startStopToolStripMenuItem});
            this.connectionToolStripMenuItem.Name = "connectionToolStripMenuItem";
            this.connectionToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.connectionToolStripMenuItem.Text = "Connection";
            // 
            // startStopToolStripMenuItem
            // 
            this.startStopToolStripMenuItem.Name = "startStopToolStripMenuItem";
            this.startStopToolStripMenuItem.Size = new System.Drawing.Size(67, 22);
            // 
            // btnRuler
            // 
            this.btnRuler.BackgroundImage = global::MapWindow_HeatMap.Properties.Resources.ruler_24;
            this.btnRuler.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRuler.Location = new System.Drawing.Point(987, 277);
            this.btnRuler.Name = "btnRuler";
            this.btnRuler.Size = new System.Drawing.Size(45, 44);
            this.btnRuler.TabIndex = 2;
            this.btnRuler.UseVisualStyleBackColor = true;
            this.btnRuler.Click += new System.EventHandler(this.btnRuler_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1044, 598);
            this.Controls.Add(this.btnRuler);
            this.Controls.Add(this.tnZoomIn);
            this.Controls.Add(this.btnZoomOut);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnScreenShot);
            this.Controls.Add(this.btnTrack);
            this.Controls.Add(this.btnPan);
            this.Controls.Add(this.btnAddIcon);
            this.Controls.Add(this.btnPolygon);
            this.Controls.Add(this.btnZoomExist);
            this.Controls.Add(this.btnHand);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel_GIS);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MapWinGIS Example";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_GIS;
        private AxMapWinGIS.AxMap axMap1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelMouse;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel statusBarlatlon;
        private System.Windows.Forms.Button btnHand;
        private System.Windows.Forms.Button tnZoomIn;
        private System.Windows.Forms.Button btnZoomOut;
        private System.Windows.Forms.Button btnPan;
        private System.Windows.Forms.Button btnZoomExist;
        private System.Windows.Forms.Button btnPolygon;
        private System.Windows.Forms.Button btnAddIcon;
        private System.Windows.Forms.Button btnTrack;
        private System.Windows.Forms.Button btnScreenShot;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem networkSettingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mapToolStripMenuItem;
        private System.Windows.Forms.Button btnRuler;
        private System.Windows.Forms.ToolStripMenuItem connectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startStopToolStripMenuItem;
    }
}


namespace MapEdit
{
    partial class MapEditForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.mapWritePanel = new System.Windows.Forms.Panel();
            this.layerComboBox = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ファイルToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.drawModeComboBox = new System.Windows.Forms.ComboBox();
            this.turnVerticalButton = new System.Windows.Forms.Button();
            this.turnHorizontalButton = new System.Windows.Forms.Button();
            this.rotateRightButton = new System.Windows.Forms.Button();
            this.rotateLeftButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.開くToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.上書きToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.画像出力ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mapWritePanel
            // 
            this.mapWritePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapWritePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.mapWritePanel.Location = new System.Drawing.Point(5, 25);
            this.mapWritePanel.Name = "mapWritePanel";
            this.mapWritePanel.Size = new System.Drawing.Size(479, 400);
            this.mapWritePanel.TabIndex = 0;
            // 
            // layerComboBox
            // 
            this.layerComboBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.layerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.layerComboBox.FormattingEnabled = true;
            this.layerComboBox.Items.AddRange(new object[] {
            "layer1",
            "layer2",
            "layer3"});
            this.layerComboBox.Location = new System.Drawing.Point(436, -1);
            this.layerComboBox.Name = "layerComboBox";
            this.layerComboBox.Size = new System.Drawing.Size(61, 20);
            this.layerComboBox.TabIndex = 1;
            this.layerComboBox.SelectedIndexChanged += new System.EventHandler(this.layerComboBox_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Silver;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.設定ToolStripMenuItem,
            this.ファイルToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(508, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 設定ToolStripMenuItem
            // 
            this.設定ToolStripMenuItem.BackColor = System.Drawing.SystemColors.Menu;
            this.設定ToolStripMenuItem.Name = "設定ToolStripMenuItem";
            this.設定ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.設定ToolStripMenuItem.Text = "設定";
            this.設定ToolStripMenuItem.Click += new System.EventHandler(this.設定ToolStripMenuItem_Click);
            // 
            // ファイルToolStripMenuItem
            // 
            this.ファイルToolStripMenuItem.BackColor = System.Drawing.SystemColors.Menu;
            this.ファイルToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.開くToolStripMenuItem,
            this.保存ToolStripMenuItem,
            this.上書きToolStripMenuItem,
            this.画像出力ToolStripMenuItem1});
            this.ファイルToolStripMenuItem.Name = "ファイルToolStripMenuItem";
            this.ファイルToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.ファイルToolStripMenuItem.Text = "ファイル";
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hScrollBar1.LargeChange = 40;
            this.hScrollBar1.Location = new System.Drawing.Point(5, 423);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(479, 17);
            this.hScrollBar1.SmallChange = 40;
            this.hScrollBar1.TabIndex = 3;
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vScrollBar1.Location = new System.Drawing.Point(483, 25);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 398);
            this.vScrollBar1.TabIndex = 4;
            // 
            // drawModeComboBox
            // 
            this.drawModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drawModeComboBox.FormattingEnabled = true;
            this.drawModeComboBox.Items.AddRange(new object[] {
            "Nearest",
            "Bilinear"});
            this.drawModeComboBox.Location = new System.Drawing.Point(360, 0);
            this.drawModeComboBox.Name = "drawModeComboBox";
            this.drawModeComboBox.Size = new System.Drawing.Size(70, 20);
            this.drawModeComboBox.TabIndex = 5;
            this.drawModeComboBox.SelectedIndexChanged += new System.EventHandler(this.drawModeComboBox_SelectedIndexChanged);
            // 
            // turnVerticalButton
            // 
            this.turnVerticalButton.Image = global::MapEdit.Properties.Resources.UnsyncedCommits_16x;
            this.turnVerticalButton.Location = new System.Drawing.Point(272, 1);
            this.turnVerticalButton.Name = "turnVerticalButton";
            this.turnVerticalButton.Size = new System.Drawing.Size(24, 23);
            this.turnVerticalButton.TabIndex = 11;
            this.turnVerticalButton.UseVisualStyleBackColor = true;
            this.turnVerticalButton.Click += new System.EventHandler(this.turnVerticalButton_Click);
            // 
            // turnHorizontalButton
            // 
            this.turnHorizontalButton.Image = global::MapEdit.Properties.Resources.SyncArrow_16xMD;
            this.turnHorizontalButton.Location = new System.Drawing.Point(302, 1);
            this.turnHorizontalButton.Name = "turnHorizontalButton";
            this.turnHorizontalButton.Size = new System.Drawing.Size(24, 23);
            this.turnHorizontalButton.TabIndex = 10;
            this.turnHorizontalButton.UseVisualStyleBackColor = true;
            this.turnHorizontalButton.Click += new System.EventHandler(this.turnHorizontalButton_Click);
            // 
            // rotateRightButton
            // 
            this.rotateRightButton.Image = global::MapEdit.Properties.Resources.RotateRight_16x;
            this.rotateRightButton.Location = new System.Drawing.Point(242, 1);
            this.rotateRightButton.Name = "rotateRightButton";
            this.rotateRightButton.Size = new System.Drawing.Size(24, 23);
            this.rotateRightButton.TabIndex = 9;
            this.rotateRightButton.UseVisualStyleBackColor = true;
            this.rotateRightButton.Click += new System.EventHandler(this.rotateRightButton_Click);
            // 
            // rotateLeftButton
            // 
            this.rotateLeftButton.Image = global::MapEdit.Properties.Resources.RotateLeft_16x;
            this.rotateLeftButton.Location = new System.Drawing.Point(212, 1);
            this.rotateLeftButton.Name = "rotateLeftButton";
            this.rotateLeftButton.Size = new System.Drawing.Size(24, 23);
            this.rotateLeftButton.TabIndex = 8;
            this.rotateLeftButton.UseVisualStyleBackColor = true;
            this.rotateLeftButton.Click += new System.EventHandler(this.rotateLeftButton_Click);
            // 
            // button2
            // 
            this.button2.Image = global::MapEdit.Properties.Resources.ZoomOut_16x;
            this.button2.Location = new System.Drawing.Point(182, 1);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(24, 23);
            this.button2.TabIndex = 7;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Image = global::MapEdit.Properties.Resources.ZoomIn_16x;
            this.button1.Location = new System.Drawing.Point(152, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 23);
            this.button1.TabIndex = 6;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // 開くToolStripMenuItem
            // 
            this.開くToolStripMenuItem.Name = "開くToolStripMenuItem";
            this.開くToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.開くToolStripMenuItem.Text = "開く";
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.保存ToolStripMenuItem.Text = "保存";
            // 
            // 上書きToolStripMenuItem
            // 
            this.上書きToolStripMenuItem.Name = "上書きToolStripMenuItem";
            this.上書きToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.上書きToolStripMenuItem.Text = "上書き";
            // 
            // 画像出力ToolStripMenuItem1
            // 
            this.画像出力ToolStripMenuItem1.Name = "画像出力ToolStripMenuItem1";
            this.画像出力ToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.画像出力ToolStripMenuItem1.Text = "画像出力";
            this.画像出力ToolStripMenuItem1.Click += new System.EventHandler(this.画像出力ToolStripMenuItem_Click);
            // 
            // MapEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 449);
            this.Controls.Add(this.turnVerticalButton);
            this.Controls.Add(this.turnHorizontalButton);
            this.Controls.Add(this.rotateRightButton);
            this.Controls.Add(this.rotateLeftButton);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.drawModeComboBox);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.layerComboBox);
            this.Controls.Add(this.mapWritePanel);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MapEditForm";
            this.Text = "MapEdit";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel mapWritePanel;
        private System.Windows.Forms.ComboBox layerComboBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 設定ToolStripMenuItem;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.ComboBox drawModeComboBox;
        private System.Windows.Forms.ToolStripMenuItem ファイルToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button rotateLeftButton;
        private System.Windows.Forms.Button rotateRightButton;
        private System.Windows.Forms.Button turnHorizontalButton;
        private System.Windows.Forms.Button turnVerticalButton;
        private System.Windows.Forms.ToolStripMenuItem 開くToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 上書きToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 画像出力ToolStripMenuItem1;
    }
}


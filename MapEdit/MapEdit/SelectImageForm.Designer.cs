namespace MapEdit
{
    partial class SelectImageForm
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
            this.components = new System.ComponentModel.Container();
            this.palletPanel = new System.Windows.Forms.Panel();
            this.selectPicture = new System.Windows.Forms.PictureBox();
            this.rotateLeftButton = new System.Windows.Forms.Button();
            this.rotateRightButton = new System.Windows.Forms.Button();
            this.turnVerticalButton = new System.Windows.Forms.Button();
            this.turnHorizontalButton = new System.Windows.Forms.Button();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.selectPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // palletPanel
            // 
            this.palletPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.palletPanel.AutoScroll = true;
            this.palletPanel.BackColor = System.Drawing.SystemColors.WindowText;
            this.palletPanel.Location = new System.Drawing.Point(1, 3);
            this.palletPanel.Name = "palletPanel";
            this.palletPanel.Size = new System.Drawing.Size(240, 400);
            this.palletPanel.TabIndex = 2;
            // 
            // selectPicture
            // 
            this.selectPicture.BackColor = System.Drawing.SystemColors.WindowText;
            this.selectPicture.Location = new System.Drawing.Point(264, 12);
            this.selectPicture.Name = "selectPicture";
            this.selectPicture.Size = new System.Drawing.Size(40, 40);
            this.selectPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.selectPicture.TabIndex = 3;
            this.selectPicture.TabStop = false;
            // 
            // rotateLeftButton
            // 
            this.rotateLeftButton.Image = global::MapEdit.Properties.Resources.RotateLeft_16x;
            this.rotateLeftButton.Location = new System.Drawing.Point(264, 89);
            this.rotateLeftButton.Name = "rotateLeftButton";
            this.rotateLeftButton.Size = new System.Drawing.Size(24, 23);
            this.rotateLeftButton.TabIndex = 9;
            this.rotateLeftButton.UseVisualStyleBackColor = true;
            this.rotateLeftButton.Click += new System.EventHandler(this.rotateLeftButton_Click);
            // 
            // rotateRightButton
            // 
            this.rotateRightButton.Image = global::MapEdit.Properties.Resources.RotateRight_16x;
            this.rotateRightButton.Location = new System.Drawing.Point(294, 89);
            this.rotateRightButton.Name = "rotateRightButton";
            this.rotateRightButton.Size = new System.Drawing.Size(24, 23);
            this.rotateRightButton.TabIndex = 10;
            this.rotateRightButton.UseVisualStyleBackColor = true;
            this.rotateRightButton.Click += new System.EventHandler(this.rotateRightButton_Click);
            // 
            // turnVerticalButton
            // 
            this.turnVerticalButton.Image = global::MapEdit.Properties.Resources.UnsyncedCommits_16x;
            this.turnVerticalButton.Location = new System.Drawing.Point(264, 118);
            this.turnVerticalButton.Name = "turnVerticalButton";
            this.turnVerticalButton.Size = new System.Drawing.Size(24, 23);
            this.turnVerticalButton.TabIndex = 12;
            this.turnVerticalButton.UseVisualStyleBackColor = true;
            this.turnVerticalButton.Click += new System.EventHandler(this.turnVerticalButton_Click);
            // 
            // turnHorizontalButton
            // 
            this.turnHorizontalButton.Image = global::MapEdit.Properties.Resources.SyncArrow_16xMD;
            this.turnHorizontalButton.Location = new System.Drawing.Point(294, 118);
            this.turnHorizontalButton.Name = "turnHorizontalButton";
            this.turnHorizontalButton.Size = new System.Drawing.Size(24, 23);
            this.turnHorizontalButton.TabIndex = 13;
            this.turnHorizontalButton.UseVisualStyleBackColor = true;
            this.turnHorizontalButton.Click += new System.EventHandler(this.turnHorizontalButton_Click);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.vScrollBar1.Location = new System.Drawing.Point(244, 3);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 400);
            this.vScrollBar1.TabIndex = 14;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // SelectImageForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GreenYellow;
            this.ClientSize = new System.Drawing.Size(322, 409);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.turnHorizontalButton);
            this.Controls.Add(this.turnVerticalButton);
            this.Controls.Add(this.rotateRightButton);
            this.Controls.Add(this.rotateLeftButton);
            this.Controls.Add(this.selectPicture);
            this.Controls.Add(this.palletPanel);
            this.Name = "SelectImageForm";
            this.Text = "マップチップパレット";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.SelectImageForm_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.selectPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel palletPanel;
        private System.Windows.Forms.PictureBox selectPicture;
        private System.Windows.Forms.Button rotateLeftButton;
        private System.Windows.Forms.Button rotateRightButton;
        private System.Windows.Forms.Button turnVerticalButton;
        private System.Windows.Forms.Button turnHorizontalButton;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}
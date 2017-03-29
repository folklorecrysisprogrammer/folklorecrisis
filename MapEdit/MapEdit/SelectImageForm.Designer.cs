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
            this.panel1 = new System.Windows.Forms.Panel();
            this.selectPicture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.selectPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.WindowText;
            this.panel1.Location = new System.Drawing.Point(1, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(257, 400);
            this.panel1.TabIndex = 2;
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
            // SelectImageForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GreenYellow;
            this.ClientSize = new System.Drawing.Size(316, 409);
            this.Controls.Add(this.selectPicture);
            this.Controls.Add(this.panel1);
            this.Name = "SelectImageForm";
            this.Text = "マップチップパレット";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.selectPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox selectPicture;
    }
}
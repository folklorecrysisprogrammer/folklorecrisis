namespace MapEdit
{
    partial class ConfigForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pixelSizeTextBox = new System.Windows.Forms.TextBox();
            this.mapSizeYtextBox = new System.Windows.Forms.TextBox();
            this.mapSizeXtextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.updateButton = new System.Windows.Forms.Button();
            this.chara = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.chara)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "マップチップサイズ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "マップサイズ";
            // 
            // pixelSizeTextBox
            // 
            this.pixelSizeTextBox.Location = new System.Drawing.Point(28, 24);
            this.pixelSizeTextBox.Name = "pixelSizeTextBox";
            this.pixelSizeTextBox.Size = new System.Drawing.Size(77, 19);
            this.pixelSizeTextBox.TabIndex = 2;
            // 
            // mapSizeYtextBox
            // 
            this.mapSizeYtextBox.Location = new System.Drawing.Point(156, 61);
            this.mapSizeYtextBox.Name = "mapSizeYtextBox";
            this.mapSizeYtextBox.Size = new System.Drawing.Size(77, 19);
            this.mapSizeYtextBox.TabIndex = 3;
            // 
            // mapSizeXtextBox
            // 
            this.mapSizeXtextBox.Location = new System.Drawing.Point(50, 61);
            this.mapSizeXtextBox.Name = "mapSizeXtextBox";
            this.mapSizeXtextBox.Size = new System.Drawing.Size(77, 19);
            this.mapSizeXtextBox.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(133, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "縦";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "横";
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(90, 212);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(89, 21);
            this.updateButton.TabIndex = 7;
            this.updateButton.Text = "更新";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // chara
            // 
            this.chara.Location = new System.Drawing.Point(71, 86);
            this.chara.Name = "chara";
            this.chara.Size = new System.Drawing.Size(120, 120);
            this.chara.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.chara.TabIndex = 8;
            this.chara.TabStop = false;
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.chara);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.mapSizeXtextBox);
            this.Controls.Add(this.mapSizeYtextBox);
            this.Controls.Add(this.pixelSizeTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ConfigForm";
            this.Text = "設定";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.chara)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox pixelSizeTextBox;
        private System.Windows.Forms.TextBox mapSizeYtextBox;
        private System.Windows.Forms.TextBox mapSizeXtextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.PictureBox chara;
    }
}
namespace MapEdit
{
    partial class StartForm
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
            this.mapChipSizeTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mapChipSizeTextBox
            // 
            this.mapChipSizeTextBox.Location = new System.Drawing.Point(36, 60);
            this.mapChipSizeTextBox.Name = "mapChipSizeTextBox";
            this.mapChipSizeTextBox.Size = new System.Drawing.Size(173, 19);
            this.mapChipSizeTextBox.TabIndex = 0;
            this.mapChipSizeTextBox.Text = "40";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(109, 120);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(45, 41);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "マップチップサイズ";
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(247, 176);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.mapChipSizeTextBox);
            this.Name = "StartForm";
            this.Text = "スタート";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox mapChipSizeTextBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label label1;
    }
}
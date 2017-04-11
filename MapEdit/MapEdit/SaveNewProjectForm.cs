using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapEdit
{
    //プロジェクト保存ウインドウ
    public partial class SaveNewProjectForm : Form
    {
        public SaveNewProjectForm()
        {
            InitializeComponent();
        }

        private void folderSelectButton_Click(object sender, EventArgs e)
        {
            var fbd =new FolderBrowserDialog();

            //ダイアログを表示する
            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                //選択されたフォルダを表示する
                folderPathTextBox.Text=fbd.SelectedPath;
            }
        }
    }
}

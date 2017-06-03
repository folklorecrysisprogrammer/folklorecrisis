using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MapEdit
{
    //プロジェクト保存ウインドウ
    public partial class SaveNewProjectForm : Form
    {
        private readonly MapEditForm meForm;

        public SaveNewProjectForm(MapEditForm meForm)
        {
            InitializeComponent();
            this.meForm = meForm ;
        }

        private void folderSelectButton_Click(object sender, EventArgs e)
        {
            var fbd =new FolderBrowserDialog();
            fbd.SelectedPath = Directory.GetCurrentDirectory();

            //ダイアログを表示する
            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                //選択されたフォルダを表示する
                folderPathTextBox.Text=fbd.SelectedPath;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
           meForm.SaveNewProject(folderPathTextBox.Text, newProjectNameTextBox.Text);
           Dispose();
        }
    }
}

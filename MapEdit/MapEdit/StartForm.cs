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
    public partial class StartForm : Form
    {
        public int MapChipSize { get; private set; }

        public StartForm()
        {
            MapChipSize = -1;
            InitializeComponent();
        }

        //Okボタンが押された時の処理
        private void okButton_Click(object sender, EventArgs e)
        {
            int result;
            //文字列を変換したとき、0より大きい整数になるかどうかを判定
            if (int.TryParse(mapChipSizeTextBox.Text, out result) && result > 0)
            {
                Dispose();
                MapChipSize = result;
            }
            MessageBox.Show("値が不正です","",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }
    }
}

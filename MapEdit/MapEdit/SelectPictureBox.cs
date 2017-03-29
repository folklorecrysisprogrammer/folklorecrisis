using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MapEdit
{
    class SelectPictureBox:PictureBox
    {
        //画像のパスを保存用
        public string FilePath { get; private set; }

        //画像をセット
        public void SetImage(string filePath) {
            FilePath = filePath;
            Image = Image.FromFile(filePath);
        }
    }
}

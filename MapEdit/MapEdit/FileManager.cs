using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace MapEdit
{
    //ファイル操作に関係するクラス
    public class FileManager
    {
        //マップを画像ファイルとして出力する
        public void MapImageOutPut(Bitmap bitmap)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "w.png";
            sfd.Filter = "png|*.png|jpeg|*.jpeg|bmp|*.bmp";
            sfd.Title = "保存先を選択してください";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                ImageFormat imageFormat;
                switch (sfd.FilterIndex)
                {
                    case 1:
                        imageFormat = ImageFormat.Png;
                        break;
                    case 2:
                        imageFormat = ImageFormat.Jpeg;
                        break;
                    case 3:
                        imageFormat = ImageFormat.Bmp;
                        break;
                    default:
                        imageFormat = ImageFormat.Png;
                        break;
                }

                bitmap.Save(sfd.FileName, imageFormat);
            }
        }

        //作業データを新規保存する
        public void SaveNewProject()
        {
            
        }
    }
}

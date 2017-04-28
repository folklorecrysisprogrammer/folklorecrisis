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
    static public class FileManager
    {
        //Bitmapを任意のフォーマットの画像ファイルとして出力する
        static public void BitmapOutPut(Bitmap bitmap)
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
    }
}

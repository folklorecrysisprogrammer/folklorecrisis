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
    public partial class SelectImageForm : Form
    {
        private int imageCountX = 0, imageCountY = 0;
        //現在選択中の画像のパス
        public string SelectImagePath { get; private set; }

        //右回転ボタンを押したときの処理
        private void rotateRightButton_Click(object sender, EventArgs e)
        {
            var image = selectPicture.Image;
            image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            selectPicture.Image=image;
        }

        //左回転ボタンを押したときの処理
        private void rotateLeftButton_Click(object sender, EventArgs e)
        {
            var image = selectPicture.Image;
            image.RotateFlip(RotateFlipType.Rotate270FlipNone);
            selectPicture.Image = image;
        }

        public SelectImageForm()
        {
            
            InitializeComponent();
            SelectImagePath = "";
            //ドラッグされた時のイベント
            DragEnter += (object sender, DragEventArgs e) =>
            {
                //ファイルがドラッグされている場合、カーソルを変更する
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    e.Effect = DragDropEffects.Copy;
                }
            };
            //ドラッグドロップイベント
            DragDrop += (object o, DragEventArgs e) =>
            {
                //ドロップされたファイルの一覧を取得
                string[] fileName = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                if (fileName.Length <= 0)
                {
                    return;
                }
                for (int i = 0; i < fileName.Length; i++)
                {
                    var pictureBox = new SelectPictureBox();
                    //PictureBox1に画像ファイルはりつけ
                    try
                    {
                        pictureBox.SetImage(fileName[i]);
                    }
                    catch (System.OutOfMemoryException)
                    {
                        continue;
                    }
                    
                    //マウスクリックのイベント
                    pictureBox.MouseClick += (object _o, MouseEventArgs _e) =>
                    {
                        selectPicture.Image = pictureBox.Image;
                        SelectImagePath =pictureBox.FilePath;
                        pictureBox.Focus();
                    };
                    pictureBox.Size = new Size(40, 40);
                    pictureBox.Location = new Point(40 * imageCountX, 40 * imageCountY-panel1.VerticalScroll.Value);
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    panel1.Controls.Add(pictureBox);
                    imageCountX++;
                    if (imageCountX == 6) { imageCountX = 0; imageCountY++; }
                }
            };
            //フォームを閉じるのを無効化する
            FormClosing += (o, e) =>
            {
                e.Cancel = true;
            };
        }
    }
}

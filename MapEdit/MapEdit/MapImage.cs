using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using DxLibDLL;
namespace MapEdit
{
    class MapImage:DXEX.Node
    {
        //画像を表示するスプライト
        private DXEX.Sprite[] picture=new DXEX.Sprite[MapEditForm.maxLayer];
        //画像のパス。Bitmapにするときに必要
        private string[] picturePath=new string[MapEditForm.maxLayer];
        private int pixelSize;
        public int PixelSize {
            get { return pixelSize; }
            set { pixelSize = value; ChangePixelSize(); }
        }
        public MapImage(int pixelSize)
        {
            this.pixelSize = pixelSize;
            anchor.SetVect(0, 0);
            for (int i = 0; i < MapEditForm.maxLayer; i++)
            {
                picturePath[i] = "";
                picture[i] = new DXEX.Sprite();
                picture[i].anchor.SetVect(0, 0);
                AddChild(picture[i]);
            }
        }

        //Bitmapとして得る
        public Bitmap GetBitmap()
        {
            Bitmap resultBitmap = new Bitmap(pixelSize, pixelSize);
            Bitmap[] bitmap = new Bitmap[MapEditForm.maxLayer];
            for(int i=0; i < MapEditForm.maxLayer; i++)
            {
                if (picturePath[i] == "")
                {
                    bitmap[i] = new Bitmap(pixelSize, pixelSize);
                }
                else
                {
                    bitmap[i] = (Bitmap)Bitmap.FromFile(picturePath[i]);
                }
            }
            Graphics g = Graphics.FromImage(resultBitmap);
            g.PixelOffsetMode = PixelOffsetMode.Half;

            //リサイズ&回転して画像を重ねる
            for (int i = 0; i < MapEditForm.maxLayer; i++)
            {
                bitmap[i].RotateFlip((RotateFlipType)((int)picture[i].Angle / 90));
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.DrawImage(bitmap[i], 0, 0, pixelSize, pixelSize);
            }
            return resultBitmap;
        }

        //画像をセット
        public void PutImage(string filePath, int layer)
        {
            picturePath[layer] = filePath;
            picture[layer].SetTexture(filePath);
            picture[layer].scale.SetVect(
                pixelSize / picture[layer].Rect.Width,
                pixelSize / picture[layer].Rect.Height
            );
            picture[layer].Angle = 0;
            picture[layer].offsetPos.SetVect(0, 0);
        }

        //画像をクリア
        public void ClearImage(int layer)
        {
            picturePath[layer] = "";
            picture[layer].ClearTexture();
            picture[layer].scale.SetVect(
                pixelSize / picture[layer].Rect.Width,
                pixelSize / picture[layer].Rect.Height
            );
        }

        //PixelSizeが変更された時の処理
        public void ChangePixelSize()
        {
            for (int i = 0; i < MapEditForm.maxLayer; i++)
            {
                picture[i].scale.SetVect(
                    pixelSize / picture[i].Rect.Width,
                    pixelSize / picture[i].Rect.Height
                );
            }
        }


        //マップチッップの角度を変更する
        private void Rotate(int addAngle)
        {
            for (int i = 0; i < MapEditForm.maxLayer; i++)
            {
                picture[i].Angle += addAngle;
            }
        }

        //回転してずれた座標を修正
        private void RotateFix()
        {
            for (int i = 0; i < MapEditForm.maxLayer; i++)
            {
                picture[i].Angle = (int)picture[i].Angle % 360;
                switch ((int)picture[i].Angle / 90)
                {
                    case 0:
                        picture[i].offsetPos.SetVect(0, 0);
                        break;
                    case 1:
                        picture[i].offsetPos.SetVect(pixelSize, 0);
                        break;
                    case 2:
                        picture[i].offsetPos.SetVect(pixelSize, pixelSize);
                        break;
                    case 3:
                        picture[i].offsetPos.SetVect(0, pixelSize);
                        break;
                }
            }
        }

        //マップチップを右回転
        public void RotateRight()
        {
            Rotate(90);
            RotateFix();
        }

        //マップチップを左回転
        public void RotateLeft()
        {
            Rotate(270);
            RotateFix();
        }
    }
}

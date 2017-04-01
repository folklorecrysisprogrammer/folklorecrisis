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

            //リサイズして画像を重ねる
            for (int i = 0; i < MapEditForm.maxLayer; i++)
            {

                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.DrawImage(bitmap[i], 0, 0, pixelSize, pixelSize);
            }
            resultBitmap.RotateFlip((RotateFlipType)((int)Angle / 90));
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

        //マップチップを右回転
        public void RotateRight()
        {
            Angle += 90;
            RotateFix();
        }

        //マップチップを左回転
        public void RotateLeft()
        {
            Angle += 270;
            RotateFix();
        }

        //回転してずれた座標を修正
        public void RotateFix()
        {
            Angle = (int)Angle % 360;
            switch ((int)Angle / 90)
            {
                case 0:
                    offsetPos.SetVect(0, 0);
                    break;
                case 1:
                    offsetPos.SetVect(pixelSize, 0);
                    break;
                case 2:
                    offsetPos.SetVect(pixelSize, pixelSize);
                    break;
                case 3:
                    offsetPos.SetVect(0, pixelSize);
                    break;
            }
        }
    }
}

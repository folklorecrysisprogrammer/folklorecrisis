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
    //マップの1マスを管理するクラス
    class MapImage:DXEX.Node
    {
        //画像を表示するスプライト
        //レイヤーの数だけ保持
        private DXEX.Sprite[] picture=new DXEX.Sprite[MapEditForm.maxLayer];

        //画像のパス。Bitmapにするときに必要
        private string[] picturePath=new string[MapEditForm.maxLayer];

        //マップチップサイズ保存用
        private int mapChipSize;
        public int MapChipSize {
            get { return mapChipSize; }
            set { mapChipSize = value; ChangePixelSize(); }
        }

        //初期化
        public MapImage(int mapChipSize)
        {
            this.mapChipSize = mapChipSize;
            anchor.SetVect(0, 0);

            //レイヤーの数だけ画像表示用スプライトを生成する
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
            //返す用のBitmap
            Bitmap resultBitmap = new Bitmap(mapChipSize, mapChipSize);

            //重ねるBitmapを生成
            Bitmap[] bitmap = new Bitmap[MapEditForm.maxLayer];
            for(int i=0; i < MapEditForm.maxLayer; i++)
            {
                if (picturePath[i] == "")
                {   
                    bitmap[i] = new Bitmap(mapChipSize, mapChipSize);
                }
                else
                {
                    bitmap[i] = (Bitmap)Bitmap.FromFile(picturePath[i]);
                }
            }

            Graphics g = Graphics.FromImage(resultBitmap);

            //画像を扱うアルゴリズムを設定する
            g.PixelOffsetMode = PixelOffsetMode.Half;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;

            //リサイズ&回転&反転して画像をresultBitmapに重ねる
            for (int i = 0; i < MapEditForm.maxLayer; i++)
            {
                bitmap[i].RotateFlip((RotateFlipType)((int)picture[i].Angle / 90));
                if (picture[i].turnFlag == DX.TRUE)
                {
                    bitmap[i].RotateFlip(RotateFlipType.RotateNoneFlipX);
                }
                g.DrawImage(bitmap[i], 0, 0, mapChipSize, mapChipSize);
            }
            return resultBitmap;
        }

        //画像をセット
        public void PutImage(string filePath, int layer)
        {
            picturePath[layer] = filePath;
            picture[layer].SetTexture(filePath);
            picture[layer].scale.SetVect(
                mapChipSize / picture[layer].Rect.Width,
                mapChipSize / picture[layer].Rect.Height
            );
            picture[layer].Angle = 0;
            picture[layer].turnFlag = DX.FALSE;
            picture[layer].offsetPos.SetVect(0, 0);
        }

        //画像をクリア
        public void ClearImage(int layer)
        {
            picturePath[layer] = "";
            picture[layer].ClearTexture();
            picture[layer].scale.SetVect(
                mapChipSize / picture[layer].Rect.Width,
                mapChipSize / picture[layer].Rect.Height
            );
        }

        //PixelSizeが変更された時の処理
        public void ChangePixelSize()
        {
            for (int i = 0; i < MapEditForm.maxLayer; i++)
            {
                picture[i].scale.SetVect(
                    mapChipSize / picture[i].Rect.Width,
                    mapChipSize / picture[i].Rect.Height
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
                        picture[i].offsetPos.SetVect(mapChipSize, 0);
                        break;
                    case 2:
                        picture[i].offsetPos.SetVect(mapChipSize, mapChipSize);
                        break;
                    case 3:
                        picture[i].offsetPos.SetVect(0, mapChipSize);
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

        //マップチップを左右反転
        public void TurnHorizontal()
        {
            for (int i = 0; i < MapEditForm.maxLayer; i++)
            {
                if (picture[i].turnFlag == DX.TRUE)
                {
                    picture[i].turnFlag = DX.FALSE;
                }
                else
                {
                    picture[i].turnFlag = DX.TRUE;
                }
            }
        }

        //マップチップを上下反転
        public void TurnVertical()
        {
            Rotate(180);
            RotateFix();
            TurnHorizontal();
        }

        //マップチップを左回転
        public void RotateLeft()
        {
            Rotate(270);
            RotateFix();
        }
    }
}

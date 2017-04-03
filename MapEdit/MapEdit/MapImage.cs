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
    public class MapImage:DXEX.Node
    {
        //画像を表示するスプライト
        //レイヤーの数だけ保持
        private DXEX.Sprite[] mapChip=new DXEX.Sprite[MapEditForm.maxLayer];

        //画像のパス。Bitmapにするときに必要
        private string[] mapChipPath=new string[MapEditForm.maxLayer];

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
                mapChipPath[i] = "";
                mapChip[i] = new DXEX.Sprite();
                mapChip[i].anchor.SetVect(0, 0);
                AddChild(mapChip[i]);
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
                if (mapChipPath[i] == "")
                {   
                    bitmap[i] = new Bitmap(mapChipSize, mapChipSize);
                }
                else
                {
                    bitmap[i] = (Bitmap)Bitmap.FromFile(mapChipPath[i]);
                }
            }

            Graphics g = Graphics.FromImage(resultBitmap);

            //画像を扱うアルゴリズムを設定する
            g.PixelOffsetMode = PixelOffsetMode.Half;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;

            //リサイズ&回転&反転して画像をresultBitmapに重ねる
            for (int i = 0; i < MapEditForm.maxLayer; i++)
            {
                bitmap[i].RotateFlip((RotateFlipType)((int)mapChip[i].Angle / 90));
                if (mapChip[i].turnFlag == DX.TRUE)
                {
                    bitmap[i].RotateFlip(RotateFlipType.RotateNoneFlipX);
                }
                g.DrawImage(bitmap[i], 0, 0, mapChipSize, mapChipSize);
            }
            return resultBitmap;
        }

        //画像をセット
        public void PutImage(SelectImage selectImage, int layer)
        {
            if (selectImage.FilePath == "") return;
            mapChipPath[layer] = selectImage.FilePath;
            mapChip[layer].SetTexture(selectImage.FilePath);
            mapChip[layer].scale.SetVect(
                mapChipSize / mapChip[layer].Rect.Width,
                mapChipSize / mapChip[layer].Rect.Height
            );
            mapChip[layer].Angle = selectImage.Angle;
            mapChip[layer].turnFlag = selectImage.turnFlag;
            RotateFix();
        }

        //画像をクリア
        public void ClearImage(int layer)
        {
            mapChipPath[layer] = "";
            mapChip[layer].ClearTexture();
            mapChip[layer].scale.SetVect(
                mapChipSize / mapChip[layer].Rect.Width,
                mapChipSize / mapChip[layer].Rect.Height
            );
        }

        //PixelSizeが変更された時の処理
        public void ChangePixelSize()
        {
            for (int i = 0; i < MapEditForm.maxLayer; i++)
            {
                mapChip[i].scale.SetVect(
                    mapChipSize / mapChip[i].Rect.Width,
                    mapChipSize / mapChip[i].Rect.Height
                );
            }
        }


        //マップチッップの角度を変更する
        private void Rotate(int addAngle)
        {
            for (int i = 0; i < MapEditForm.maxLayer; i++)
            {
                mapChip[i].Angle += addAngle;
            }
        }

        //回転してずれた座標を修正
        private void RotateFix()
        {
            for (int i = 0; i < MapEditForm.maxLayer; i++)
            {
                mapChip[i].Angle = (int)mapChip[i].Angle % 360;
                switch ((int)mapChip[i].Angle / 90)
                {
                    case 0:
                        mapChip[i].offsetPos.SetVect(0, 0);
                        break;
                    case 1:
                        mapChip[i].offsetPos.SetVect(mapChipSize, 0);
                        break;
                    case 2:
                        mapChip[i].offsetPos.SetVect(mapChipSize, mapChipSize);
                        break;
                    case 3:
                        mapChip[i].offsetPos.SetVect(0, mapChipSize);
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

        private void SwitchTurnFlag(int i)
        {
            if (mapChip[i].turnFlag == DX.TRUE)
            {
                mapChip[i].turnFlag = DX.FALSE;
            }
            else
            {
                mapChip[i].turnFlag = DX.TRUE;
            }
        }

        //マップチップを左右反転
        public void TurnHorizontal()
        {
            for (int i = 0; i < MapEditForm.maxLayer; i++)
            {
                if (mapChip[i].Angle == 90 || mapChip[i].Angle == 270)
                {
                    mapChip[i].Angle += 180;
                }
                SwitchTurnFlag(i);
                
            }
            RotateFix();
        }


        //マップチップを上下反転
        public void TurnVertical()
        {
            for (int i = 0; i < MapEditForm.maxLayer; i++)
            {
                if (mapChip[i].Angle == 0 || mapChip[i].Angle == 180)
                {
                    mapChip[i].Angle += 180;
                }
                SwitchTurnFlag(i);

            }
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

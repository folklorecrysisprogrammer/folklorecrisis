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
    //mapChipsの参照を渡しているのが駄目だな・・・
    //カプセル化できてない。やり直し
    public class MapOneMass:DXEX.Node
    {
        //画像を表示するスプライトを
        //レイヤーの数だけ保持
        public MapChip[] mapChips { get; private set; }
        private readonly int mapChipSize;

        //MapOneMassが持つmapChip配列を交換する
        public void SwapMapChips(MapOneMass mapOneMass)
        {
            for(int i=0; i < MapEditForm.maxLayer; i++)
            {
                mapChips[i].RemoveFromParent();
                mapOneMass.mapChips[i].RemoveFromParent();
                AddChild(mapOneMass.mapChips[i]);
                mapOneMass.AddChild(mapChips[i]);
            }
            var tempMapChips=mapChips;
            mapChips = mapOneMass.mapChips;
            mapOneMass.mapChips = tempMapChips;
        }

        //初期化
        public MapOneMass(int mapChipSize)
        {
            mapChips = new MapChip[MapEditForm.maxLayer];
            this.mapChipSize = mapChipSize;
            anchor.SetVect(0, 0);           

            //レイヤーの数だけ画像表示用スプライトを生成する
            for (int i = 0; i < MapEditForm.maxLayer; i++)
            {
                mapChips[i] = new MapChip(mapChipSize);
                mapChips[i].anchor.SetVect(0, 0);
                AddChild(mapChips[i]);
            }
        }

        //Bitmapとして得る
        public Bitmap GetBitmap(MapChipResourceManager mcrm)
        {
            //返す用のBitmap
            Bitmap resultBitmap = new Bitmap(mapChipSize, mapChipSize);

            //重ねるBitmapを生成
            Bitmap[] bitmap = new Bitmap[MapEditForm.maxLayer];
            for(int i=0; i < MapEditForm.maxLayer; i++)
            {
                if (mapChips[i].Id == null)
                {   
                    bitmap[i] = new Bitmap(mapChipSize, mapChipSize);
                }
                else
                {
                    bitmap[i] = mcrm.GetBitmap(mapChips[i].Id.value);
                }
            }

            Graphics g = Graphics.FromImage(resultBitmap);

            //画像を扱うアルゴリズムを設定する
            g.PixelOffsetMode = PixelOffsetMode.Half;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;

            //リサイズ&回転&反転して画像をresultBitmapに重ねる
            for (int i = 0; i < MapEditForm.maxLayer; i++)
            {
                bitmap[i].RotateFlip((RotateFlipType)((int)mapChips[i].Angle / 90));
                if (mapChips[i].turnFlag == DX.TRUE)
                {
                    bitmap[i].RotateFlip(RotateFlipType.RotateNoneFlipX);
                }
                g.DrawImage(bitmap[i],0,0);
            }
            return resultBitmap;
        }

        //画像をセット
        public void PutImage(MapChip mapChip, int layer)
        {
            if(mapChip.GetTexture() == null)return;
            mapChips[layer].SetId(mapChip.Id);
            mapChips[layer].SetTexture(mapChip.GetTexture());
            mapChips[layer].Angle = mapChip.Angle;
            mapChips[layer].turnFlag = mapChip.turnFlag;
        }

        //画像をクリア
        public void ClearImage(int layer)
        {
            mapChips[layer].ClearImage();
        }

        //PixelSizeが変更された時の処理
        public void ChangePixelSize()
        {
            for (int i = 0; i < MapEditForm.maxLayer; i++)
            {
                mapChips[i].MapChipSize = mapChipSize;
            }
        }


        //マップチッップの角度を変更する
        private void Rotate(int addAngle)
        {
            for (int i = 0; i < MapEditForm.maxLayer; i++)
            {
                mapChips[i].Angle+=addAngle;
            }
        }

        //マップチップを右回転
        public void RotateRight()
        {
            Rotate(90);
        }


        //マップチップを左右反転
        public void TurnHorizontal()
        {
            for (int i = 0; i < MapEditForm.maxLayer; i++)
            {
                mapChips[i].TurnHorizontal();
            }
        }


        //マップチップを上下反転
        public void TurnVertical()
        {
            for (int i = 0; i < MapEditForm.maxLayer; i++)
            {
                mapChips[i].TurnVertical();
            }
        }

        //マップチップを左回転
        public void RotateLeft()
        {
            Rotate(270);
        }
    }
}

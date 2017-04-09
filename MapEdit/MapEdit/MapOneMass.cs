﻿using System;
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
    public class MapOneMass:DXEX.Node
    {
        //画像を表示するスプライト
        //レイヤーの数だけ保持
        private MapChip[] mapChips=new MapChip[MapEditForm.maxLayer];

        //画像のパス。Bitmapにするときに必要
        private string[] mapChipPath=new string[MapEditForm.maxLayer];

        //マップチップサイズ保存用
        private int mapChipSize;
        public int MapChipSize {
            get { return mapChipSize; }
            set { mapChipSize = value; ChangePixelSize(); }
        }

        //初期化
        public MapOneMass(int mapChipSize)
        {
            this.mapChipSize = mapChipSize;
            anchor.SetVect(0, 0);
           

            //レイヤーの数だけ画像表示用スプライトを生成する
            for (int i = 0; i < MapEditForm.maxLayer; i++)
            {
                mapChipPath[i] = "";
                mapChips[i] = new MapChip(mapChipSize);
                mapChips[i].anchor.SetVect(0, 0);
                AddChild(mapChips[i]);
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
                bitmap[i].RotateFlip((RotateFlipType)((int)mapChips[i].Angle / 90));
                if (mapChips[i].turnFlag == DX.TRUE)
                {
                    bitmap[i].RotateFlip(RotateFlipType.RotateNoneFlipX);
                }
                g.DrawImage(bitmap[i], 0, 0, mapChipSize, mapChipSize);
            }
            return resultBitmap;
        }

        //画像をセット
        public void PutImage(MapChip mapChip, int layer)
        {
            // if (selectImage.FilePath == "") return;
            // mapChipPath[layer] = selectImage.FilePath;
            if(mapChip.GetTexture() == null)return;
            mapChips[layer].SetTexture(mapChip.GetTexture());
            mapChips[layer].Angle = mapChip.Angle;
            mapChips[layer].turnFlag = mapChip.turnFlag;
        }

        //画像をクリア
        public void ClearImage(int layer)
        {
            mapChipPath[layer] = "";
            mapChips[layer].ClearTexture();
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MapEdit
{

    //マップの画像情報を保持するクラス
    public class MapImageDataMap
    {
        //マップチップの配列
        private MapImage[,] mapImage;

        //マップチップサイズ
        private int mapChipSize = 40;
        public int MapChipSize {
            get { return mapChipSize; }
            set { ChangeMapChipSize(value); }
        }
        //マップチップの横の数と縦の数
        private int numberX=20;
        private int numberY=20;
        public Size MapSize
        {
            get { return new Size(numberX, numberY); }
            set { ChangeMapSize(value.Width,value.Height); }
        }


        private readonly MapWriteScene mapWriteScene;

        //配列ぽく振るまう
        public MapImage this[int x,int y]
        {
            get { return mapImage[x, y]; }
        }

        //初期化
        public MapImageDataMap(MapWriteScene mapWriteScene)
        {
            this.mapWriteScene = mapWriteScene;
            mapImage = new MapImage[numberX, numberY];
            for (int x = 0; x < numberX; x++)
            {
                for (int y = 0; y <numberY; y++)
                {
                    mapImage[x, y] = new MapImage(mapChipSize);
                    mapImage[x, y].localPos.SetVect(x * mapChipSize, y * mapChipSize);
                }
            }
        }

        //マップ全体をBitmapに変換する
        public Bitmap GetBitmap()
        {
            Bitmap unitedImg = new Bitmap(mapChipSize * numberX, mapChipSize *numberY);
            Graphics g = Graphics.FromImage(unitedImg);
            for (int countY = 0; countY < numberY; ++countY)
            {
                for (int countX = 0; countX < numberX; ++countX)
                {
                    Bitmap bitmap = mapImage[countX, countY].GetBitmap();
                    g.DrawImage(bitmap, mapChipSize * countX, mapChipSize * countY);
                }
            }
            return unitedImg;
        }

        //MapChipSize変更処理
        private void ChangeMapChipSize(int newMapChipSize)
        {
            mapChipSize = newMapChipSize;
            //マップチップの位置とサイズ調整
            for (int x = 0; x < numberX; x++)
            {
                for (int y = 0; y < numberY; y++)
                {
                    mapImage[x, y].MapChipSize = mapChipSize;
                    mapImage[x, y].localPos.SetVect(x * mapChipSize, y * mapChipSize);
                }
            }
            //スクロールバーの調整
            mapWriteScene.GetScroll().SetScrollDelta();
            mapWriteScene.GetScroll().SetScrollMaximum();
            mapWriteScene.UpdateShowMapImage();
        }

        //MapSize変更処理
        private void ChangeMapSize(int newNumberX,int newNumberY)
        {

            var tMapImage = mapImage;
            mapImage = new MapImage[newNumberX, newNumberY];

            //前のMapImageから、はみ出した部分があれば削除する
            for (int x = newNumberX; x < numberX; x++)
            {
                for (int y = 0; y < numberY; y++)
                {
                    tMapImage[x, y].Dispose();
                }
            }
            for (int x = 0; x < newNumberX; x++)
            {
                for (int y = newNumberY; y < numberY; y++)
                {
                    tMapImage[x, y].Dispose();
                }
            }

            //新しいMapImageに前のMapImageをサイズの範囲内でコピーする
            for (int x = 0;
                x < numberX && x <newNumberX;
                x++)
            {
                for (int y = 0;
                    y < numberY && y < newNumberY;
                    y++)
                {
                    mapImage[x, y] = tMapImage[x, y];
                }
            }

            //新しいMapImageの新しく生成された領域を初期化する
            for (int x = numberX; x < newNumberX; x++)
            {
                for (int y = 0; y < newNumberY; y++)
                {
                    mapImage[x, y] = new MapImage(MapChipSize);
                    mapImage[x, y].localPos.SetVect(x * MapChipSize, y * MapChipSize);
                }
            }
            for (int x = 0; x < numberX; x++)
            {
                for (int y =numberY; y < newNumberY; y++)
                {
                    mapImage[x, y] = new MapImage(MapChipSize);
                    mapImage[x, y].localPos.SetVect(x * MapChipSize, y * MapChipSize);
                }
            }
            numberX = newNumberX;
            numberY = newNumberY;
            //スクロールバーの調整
            mapWriteScene.GetScroll().SetScrollMaximum();
            mapWriteScene.UpdateShowMapImage();
        }

        //マップを右回転
        public void RotateRight()
        {
            var tMapImage = mapImage;
            mapImage = new MapImage[numberY, numberX];
            for (int x = 0; x < mapImage.GetLength(0); x++)
            {
                for (int y = 0; y < mapImage.GetLength(1); y++)
                {
                    mapImage[x, y] = tMapImage[y, tMapImage.GetLength(1) - x - 1];
                    mapImage[x, y].localPos.SetVect(x * MapChipSize, y * MapChipSize);
                    mapImage[x, y].RotateRight();
                }
            }
            numberX = mapImage.GetLength(0);
            numberY = mapImage.GetLength(1);
            mapWriteScene.GetScroll().SetScrollMaximum();
            mapWriteScene.UpdateShowMapImage();
        }

        //マップを左回転
        public void RotateLeft()
        {
            var tMapImage = mapImage;
            mapImage = new MapImage[numberY, numberX];
            for (int x = 0; x < mapImage.GetLength(0); x++)
            {
                for (int y = 0; y < mapImage.GetLength(1); y++)
                {
                    mapImage[x, y] = tMapImage[tMapImage.GetLength(0) - y - 1, x];
                    mapImage[x, y].localPos.SetVect(x * MapChipSize, y * MapChipSize);
                    mapImage[x, y].RotateLeft();
                }
            }
            numberX = mapImage.GetLength(0);
            numberY = mapImage.GetLength(1);
            mapWriteScene.GetScroll().SetScrollMaximum();
            mapWriteScene.UpdateShowMapImage();
        }

        //マップを左右反転
        public void turnHorizontal()
        {
            var tMapImage = mapImage;
            mapImage = new MapImage[numberX, numberY];
            for (int x = 0; x < mapImage.GetLength(0); x++)
            {
                for (int y = 0; y < mapImage.GetLength(1); y++)
                {
                    mapImage[x, y] = tMapImage[tMapImage.GetLength(0) - x - 1, y];
                    mapImage[x, y].localPos.SetVect(x * MapChipSize, y * MapChipSize);
                    mapImage[x, y].TurnHorizontal();
                }
            }
        }

        //マップを上下反転
        public void turnVertical()
        {
            var tMapImage = mapImage;
            mapImage = new MapImage[numberX, numberY];
            for (int x = 0; x < mapImage.GetLength(0); x++)
            {
                for (int y = 0; y < mapImage.GetLength(1); y++)
                {
                    mapImage[x, y] = tMapImage[x, tMapImage.GetLength(1) - y - 1];
                    mapImage[x, y].localPos.SetVect(x * MapChipSize, y * MapChipSize);
                    mapImage[x, y].TurnVertical();
                }
            }
        }
    }
}

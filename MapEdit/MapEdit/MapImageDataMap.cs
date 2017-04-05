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
        public int MapChipSize { get; private set; }
        //マップチップの横の数と縦の数
        public int NumberX { get; private set; }
        public int NumberY { get; private set; }

        //配列ぽく振るまう
        public MapImage this[int x,int y]
        {
            get { return mapImage[x, y]; }
        }

        //初期化
        public MapImageDataMap(int numberX,int numberY,int mapChipSize)
        {
            mapImage = new MapImage[numberX, numberY];
            this.NumberX = numberX;
            this.NumberY = numberY;
            this.MapChipSize = mapChipSize;
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
            Bitmap unitedImg = new Bitmap(MapChipSize * NumberX, MapChipSize *NumberY);
            Graphics g = Graphics.FromImage(unitedImg);
            for (int countY = 0; countY < NumberY; ++countY)
            {
                for (int countX = 0; countX < NumberX; ++countX)
                {
                    Bitmap bitmap = mapImage[countX, countY].GetBitmap();
                    g.DrawImage(bitmap, MapChipSize * countX, MapChipSize * countY);
                }
            }
            return unitedImg;
        }

        //MapChipSize変更処理
        public void ChangeMapChipSize(int newMapChipSize)
        {
            MapChipSize = newMapChipSize;
            //マップチップの位置とサイズ調整
            for (int x = 0; x < NumberX; x++)
            {
                for (int y = 0; y < NumberY; y++)
                {
                    mapImage[x, y].MapChipSize = MapChipSize;
                    mapImage[x, y].localPos.SetVect(x * MapChipSize, y * MapChipSize);
                }
            }
        }

        //MapSize変更処理
        public void ChangeMapSize(int newNumberX,int newNumberY)
        {

            var tMapImage = mapImage;
            mapImage = new MapImage[newNumberX, newNumberY];

            //前のMapImageから、はみ出した部分があれば削除する
            for (int x = newNumberX; x < NumberX; x++)
            {
                for (int y = 0; y < NumberY; y++)
                {
                    tMapImage[x, y].Dispose();
                }
            }
            for (int x = 0; x < newNumberX; x++)
            {
                for (int y = newNumberY; y < NumberY; y++)
                {
                    tMapImage[x, y].Dispose();
                }
            }

            //新しいMapImageに前のMapImageをサイズの範囲内でコピーする
            for (int x = 0;
                x < NumberX && x <newNumberX;
                x++)
            {
                for (int y = 0;
                    y < NumberY && y < newNumberY;
                    y++)
                {
                    mapImage[x, y] = tMapImage[x, y];
                }
            }

            //新しいMapImageの新しく生成された領域を初期化する
            for (int x = NumberX; x < newNumberX; x++)
            {
                for (int y = 0; y < newNumberY; y++)
                {
                    mapImage[x, y] = new MapImage(MapChipSize);
                    mapImage[x, y].localPos.SetVect(x * MapChipSize, y * MapChipSize);
                }
            }
            for (int x = 0; x < NumberX; x++)
            {
                for (int y =NumberY; y < newNumberY; y++)
                {
                    mapImage[x, y] = new MapImage(MapChipSize);
                    mapImage[x, y].localPos.SetVect(x * MapChipSize, y * MapChipSize);
                }
            }
            NumberX = newNumberX;
            NumberY = newNumberY;
        }

        //マップを右回転
        public void RotateRight()
        {
            var tMapImage = mapImage;
            mapImage = new MapImage[NumberY, NumberX];
            for (int x = 0; x < mapImage.GetLength(0); x++)
            {
                for (int y = 0; y < mapImage.GetLength(1); y++)
                {
                    mapImage[x, y] = tMapImage[y, tMapImage.GetLength(1) - x - 1];
                    mapImage[x, y].localPos.SetVect(x * MapChipSize, y * MapChipSize);
                    mapImage[x, y].RotateRight();
                }
            }
            NumberX = mapImage.GetLength(0);
            NumberY = mapImage.GetLength(1);
        }

        //マップを左回転
        public void RotateLeft()
        {
            var tMapImage = mapImage;
            mapImage = new MapImage[NumberY, NumberX];
            for (int x = 0; x < mapImage.GetLength(0); x++)
            {
                for (int y = 0; y < mapImage.GetLength(1); y++)
                {
                    mapImage[x, y] = tMapImage[tMapImage.GetLength(0) - y - 1, x];
                    mapImage[x, y].localPos.SetVect(x * MapChipSize, y * MapChipSize);
                    mapImage[x, y].RotateLeft();
                }
            }
            NumberX = mapImage.GetLength(0);
            NumberY = mapImage.GetLength(1);
        }

        //マップを左右反転
        public void turnHorizontal()
        {
            var tMapImage = mapImage;
            mapImage = new MapImage[NumberX, NumberY];
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
            mapImage = new MapImage[NumberX, NumberY];
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

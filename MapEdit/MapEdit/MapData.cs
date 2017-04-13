using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MapEdit
{

    //マップ全体の画像情報を保持するクラス
    public class MapData
    {
        //マップのマスごとの情報を保持
        private MapOneMass[,] mapImage;

        //マップチップサイズ
        public int MapChipSize { get; }
        //マップチップの横の数と縦の数
        private int numberX=20;
        private int numberY=20;
        public Size MapSize
        {
            get { return new Size(numberX, numberY); }
            set { ChangeMapSize(value.Width,value.Height); }
        }

        private readonly MapEditForm meForm;

        //配列ぽく振るまう
        public MapOneMass this[int x,int y]
        {
            get { return mapImage[x, y]; }
        }

        //初期化
        public MapData(MapEditForm meForm)
        {
            this.meForm = meForm;
            MapChipSize = meForm.MapChipSize;
            mapImage = new MapOneMass[numberX, numberY];
            for (int x = 0; x < numberX; x++)
            {
                for (int y = 0; y <numberY; y++)
                {
                    mapImage[x, y] = new MapOneMass(meForm);
                    mapImage[x, y].LocalPos=new DXEX.Vect(x * MapChipSize, y * MapChipSize);
                }
            }
        }

        //マップ全体をBitmapに変換する
        public Bitmap GetBitmap()
        {
            Bitmap unitedImg = new Bitmap(MapChipSize * numberX, MapChipSize *numberY);
            Graphics g = Graphics.FromImage(unitedImg);
            for (int countY = 0; countY < numberY; ++countY)
            {
                for (int countX = 0; countX < numberX; ++countX)
                {
                    Bitmap bitmap = mapImage[countX, countY].GetBitmap();
                    g.DrawImage(bitmap, MapChipSize * countX, MapChipSize * countY);
                }
            }
            return unitedImg;
        }

        //MapSize変更処理
        private void ChangeMapSize(int newNumberX,int newNumberY)
        {

            var tMapImage = mapImage;
            mapImage = new MapOneMass[newNumberX, newNumberY];

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
                    mapImage[x, y] = new MapOneMass(meForm);
                    mapImage[x, y].LocalPos=new DXEX.Vect(x * MapChipSize, y * MapChipSize);
                }
            }
            for (int x = 0; x < numberX; x++)
            {
                for (int y =numberY; y < newNumberY; y++)
                {
                    mapImage[x, y] = new MapOneMass(meForm);
                    mapImage[x, y].LocalPos=new DXEX.Vect(x * MapChipSize, y * MapChipSize);
                }
            }
            numberX = newNumberX;
            numberY = newNumberY;

        }

        //マップを右回転
        public void RotateRight()
        {
            var tMapImage = mapImage;
            mapImage = new MapOneMass[numberY, numberX];
            for (int x = 0; x < mapImage.GetLength(0); x++)
            {
                for (int y = 0; y < mapImage.GetLength(1); y++)
                {
                    mapImage[x, y] = tMapImage[y, tMapImage.GetLength(1) - x - 1];
                    mapImage[x, y].LocalPos=new DXEX.Vect(x * MapChipSize, y * MapChipSize);
                    mapImage[x, y].RotateRight();
                }
            }
            numberX = mapImage.GetLength(0);
            numberY = mapImage.GetLength(1);
        }

        //マップを左回転
        public void RotateLeft()
        {
            var tMapImage = mapImage;
            mapImage = new MapOneMass[numberY, numberX];
            for (int x = 0; x < mapImage.GetLength(0); x++)
            {
                for (int y = 0; y < mapImage.GetLength(1); y++)
                {
                    mapImage[x, y] = tMapImage[tMapImage.GetLength(0) - y - 1, x];
                    mapImage[x, y].LocalPos=new DXEX.Vect(x * MapChipSize, y * MapChipSize);
                    mapImage[x, y].RotateLeft();
                }
            }
            numberX = mapImage.GetLength(0);
            numberY = mapImage.GetLength(1);
        }

        //マップを左右反転
        public void turnHorizontal()
        {
            var tMapImage = mapImage;
            mapImage = new MapOneMass[numberX, numberY];
            for (int x = 0; x < mapImage.GetLength(0); x++)
            {
                for (int y = 0; y < mapImage.GetLength(1); y++)
                {
                    mapImage[x, y] = tMapImage[tMapImage.GetLength(0) - x - 1, y];
                    mapImage[x, y].LocalPos=new DXEX.Vect(x * MapChipSize, y * MapChipSize);
                    mapImage[x, y].TurnHorizontal();
                }
            }
        }

        //マップを上下反転
        public void turnVertical()
        {
            var tMapImage = mapImage;
            mapImage = new MapOneMass[numberX, numberY];
            for (int x = 0; x < mapImage.GetLength(0); x++)
            {
                for (int y = 0; y < mapImage.GetLength(1); y++)
                {
                    mapImage[x, y] = tMapImage[x, tMapImage.GetLength(1) - y - 1];
                    mapImage[x, y].LocalPos=new DXEX.Vect(x * MapChipSize, y * MapChipSize);
                    mapImage[x, y].TurnVertical();
                }
            }
        }
    }
}

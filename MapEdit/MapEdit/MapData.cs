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
        private MapOneMass[,] mapOneMass;

        //マップチップサイズ
        public int MapChipSize { get; }
        //マップチップの横の数と縦の数
        private int numberX;
        private int numberY;
        public Size MapSize
        {
            get { return new Size(numberX, numberY); }
            set { ChangeMapSize(value.Width, value.Height); }
        }
        public int MapSizeX { get { return numberX; } }
        public int MapSizeY { get { return numberY; }}

        private readonly MapEditForm meForm;

        //配列ぽく振るまう
        public MapOneMass this[int x,int y]
        {
            get { return mapOneMass[x, y]; }
        }

        //初期化
        public MapData(MapEditForm meForm, Size mapSize,int mapChipSize)
        {
            numberX = mapSize.Width;
            numberY = mapSize.Height;
            this.meForm = meForm;
            MapChipSize = mapChipSize;
            mapOneMass = new MapOneMass[numberX, numberY];
            for (int x = 0; x < numberX; x++)
            {
                for (int y = 0; y < numberY; y++)
                {
                    mapOneMass[x, y] = new MapOneMass(meForm,mapChipSize);
                    mapOneMass[x, y].LocalPos = new DXEX.Vect(x * MapChipSize, y * MapChipSize);
                }
            }
        }

        public void LoadProject(MapDataFromText mdft)
        {
            int count=0;
            for(int y = 0; y < numberY; y++)
            {
                for(int x = 0; x < numberX; x++)
                {
                    for (int layer = 0; layer < MapEditForm.maxLayer; layer++)
                    {
                        if (mdft.Id[count] != -1)
                        {
                            mapOneMass[x, y].mapChips[layer].SetTexture(meForm.mcrm.GetTexture(mdft.Id[count]));
                            mapOneMass[x, y].mapChips[layer].Id = mdft.Id[count];
                            mapOneMass[x, y].mapChips[layer].Angle = mdft.Angle[count];
                            mapOneMass[x, y].mapChips[layer].turnFlag = mdft.Turn[count];
                        }
                        count++;
                    }
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
                    Bitmap bitmap = mapOneMass[countX, countY].GetBitmap();
                    g.DrawImage(bitmap, MapChipSize * countX, MapChipSize * countY);
                }
            }
            return unitedImg;
        }

        //MapSize変更処理
        private void ChangeMapSize(int newNumberX,int newNumberY)
        {

            var tMapImage = mapOneMass;
            mapOneMass = new MapOneMass[newNumberX, newNumberY];

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
                    mapOneMass[x, y] = tMapImage[x, y];
                }
            }

            //新しいMapImageの新しく生成された領域を初期化する
            for (int x = numberX; x < newNumberX; x++)
            {
                for (int y = 0; y < newNumberY; y++)
                {
                    mapOneMass[x, y] = new MapOneMass(meForm,MapChipSize);
                    mapOneMass[x, y].LocalPos=new DXEX.Vect(x * MapChipSize, y * MapChipSize);
                }
            }
            for (int x = 0; x < numberX; x++)
            {
                for (int y =numberY; y < newNumberY; y++)
                {
                    mapOneMass[x, y] = new MapOneMass(meForm,MapChipSize);
                    mapOneMass[x, y].LocalPos=new DXEX.Vect(x * MapChipSize, y * MapChipSize);
                }
            }
            numberX = newNumberX;
            numberY = newNumberY;

        }

        //マップを右回転
        public void RotateRight()
        {
            var tMapImage = mapOneMass;
            mapOneMass = new MapOneMass[numberY, numberX];
            for (int x = 0; x < mapOneMass.GetLength(0); x++)
            {
                for (int y = 0; y < mapOneMass.GetLength(1); y++)
                {
                    mapOneMass[x, y] = tMapImage[y, tMapImage.GetLength(1) - x - 1];
                    mapOneMass[x, y].LocalPos=new DXEX.Vect(x * MapChipSize, y * MapChipSize);
                    mapOneMass[x, y].RotateRight();
                }
            }
            numberX = mapOneMass.GetLength(0);
            numberY = mapOneMass.GetLength(1);
        }

        //マップを左回転
        public void RotateLeft()
        {
            var tMapImage = mapOneMass;
            mapOneMass = new MapOneMass[numberY, numberX];
            for (int x = 0; x < mapOneMass.GetLength(0); x++)
            {
                for (int y = 0; y < mapOneMass.GetLength(1); y++)
                {
                    mapOneMass[x, y] = tMapImage[tMapImage.GetLength(0) - y - 1, x];
                    mapOneMass[x, y].LocalPos=new DXEX.Vect(x * MapChipSize, y * MapChipSize);
                    mapOneMass[x, y].RotateLeft();
                }
            }
            numberX = mapOneMass.GetLength(0);
            numberY = mapOneMass.GetLength(1);
        }

        //マップを左右反転
        public void turnHorizontal()
        {
            var tMapImage = mapOneMass;
            mapOneMass = new MapOneMass[numberX, numberY];
            for (int x = 0; x < mapOneMass.GetLength(0); x++)
            {
                for (int y = 0; y < mapOneMass.GetLength(1); y++)
                {
                    mapOneMass[x, y] = tMapImage[tMapImage.GetLength(0) - x - 1, y];
                    mapOneMass[x, y].LocalPos=new DXEX.Vect(x * MapChipSize, y * MapChipSize);
                    mapOneMass[x, y].TurnHorizontal();
                }
            }
        }

        //マップを上下反転
        public void turnVertical()
        {
            var tMapImage = mapOneMass;
            mapOneMass = new MapOneMass[numberX, numberY];
            for (int x = 0; x < mapOneMass.GetLength(0); x++)
            {
                for (int y = 0; y < mapOneMass.GetLength(1); y++)
                {
                    mapOneMass[x, y] = tMapImage[x, tMapImage.GetLength(1) - y - 1];
                    mapOneMass[x, y].LocalPos=new DXEX.Vect(x * MapChipSize, y * MapChipSize);
                    mapOneMass[x, y].TurnVertical();
                }
            }
        }
    }
}

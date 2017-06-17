using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MapEdit
{
    //MapWriteSceneに配置するマップチップを統合的に管理するクラス
    public class MapDataControl
    {
        //マップのマスごとの情報を保持
        private MapOneMass[,] mapOneMassList;
        //マップチップサイズ
        public int MapChipSize { get; }
        //マップチップの横の数と縦の数
        public Size MapSize
        {
            get { return new Size(MapSizeX,MapSizeY); }
            set { ChangeMapSize(value.Width, value.Height); }
        }
        public int MapSizeX { get { return mapOneMassList.GetLength(0); } }
        public int MapSizeY { get { return mapOneMassList.GetLength(1); }}

        private readonly MapChipResourceManager mcrm;

        //配列ぽく振るまう
        //（こいつは闇、消すべき）
        public MapOneMass this[int x,int y]
        {
            get { return mapOneMassList[x, y]; }
        }
        
        //初期化
        public MapDataControl(MapChipResourceManager mcrm, Size mapSize,int mapChipSize)
        {
            this.mcrm = mcrm;
            MapChipSize = mapChipSize;
            mapOneMassList = new MapOneMass[mapSize.Width, mapSize.Height];
            for (int x = 0; x < MapSizeX; x++)
            {
                for (int y = 0; y < MapSizeY; y++)
                {
                    mapOneMassList[x, y] = new MapOneMass(mcrm,mapChipSize);
                    mapOneMassList[x, y].LocalPos = new DXEX.Vect(x * MapChipSize, y * MapChipSize);
                }
            }
        }

        //既存のプロジェクトからマップをロードする
        public void LoadProject(MapInfoFromText mift)
        {
            int count=0;
            for(int y = 0; y < MapSizeY; y++)
            {
                for(int x = 0; x < MapSizeX; x++)
                {
                    for (int layer = 0; layer < MapEditForm.maxLayer; layer++)
                    {
                        if (mift.Id[count] != -1)
                        {
                            mapOneMassList[x, y].mapChips[layer].SetTexture(mcrm.GetTexture(mift.Id[count]));
                            mapOneMassList[x, y].mapChips[layer].Id = mift.Id[count];
                            mapOneMassList[x, y].mapChips[layer].Angle = mift.Angle[count];
                            mapOneMassList[x, y].mapChips[layer].turnFlag = mift.Turn[count];
                        }
                        count++;
                    }
                }
            }
        }

        public void RemoveId(int id,int lastid)
        {
            if (id == lastid) lastid = -999;
           foreach ( var item in mapOneMassList)
            {
                foreach(var mapChip in item.mapChips)
                {
                    if (mapChip.Id == id)
                    {
                        mapChip.ClearTexture();
                        mapChip.Id = -1;
                    }
                    else if (mapChip.Id == lastid)
                    {
                        mapChip.Id = id;
                    }
                }
            }
        }

        public void SwapId(int id1, int id2)
        {
            foreach (var item in mapOneMassList)
            {
                foreach (var mapChip in item.mapChips)
                {
                    if (mapChip.Id == id1)
                    {
                        mapChip.Id = id2;
                    }
                    else if (mapChip.Id == id2)
                    {
                        mapChip.Id = id1;
                    }
                }
            }
        }



        //マップ全体をBitmapに変換する
        public Bitmap GetBitmap()
        {
            Bitmap unitedImg = new Bitmap(MapChipSize * MapSizeX, MapChipSize * MapSizeY);
            Graphics g = Graphics.FromImage(unitedImg);
            for (int countY = 0; countY < MapSizeY; ++countY)
            {
                for (int countX = 0; countX < MapSizeX; ++countX)
                {
                    Bitmap bitmap = mapOneMassList[countX, countY].GetBitmap();
                    g.DrawImage(bitmap, MapChipSize * countX, MapChipSize * countY);
                }
            }
            return unitedImg;
        }


        public StringBuilder GetMapDataText()
        {
            // マップの中身を書き出す
            StringBuilder mapDataText = new StringBuilder();
            mapDataText.Append(MapChipSize + "," + MapSizeX + "," + MapSizeY);
            for (int y = 0; y < MapSizeY; y++)
            {
                mapDataText.Append(Environment.NewLine);
                for (int x = 0; x < MapSizeX; x++)
                {
                    MapOneMass mom = mapOneMassList[x, y];
                    MapChip[] ChipId = mom.mapChips;
                    for (int i = 0; i < ChipId.Length; i++)
                    {
                        int Id = ChipId[i].Id;
                        int Angle = (int)(ChipId[i].Angle / 90.0);
                        int turnFlag = ChipId[i].turnFlag;
                        mapDataText.Append(Id + "," + Angle + "," + turnFlag + ",");
                    }
                }
            }
            return mapDataText;
        }

        //MapSize変更処理
        private void ChangeMapSize(int newNumberX,int newNumberY)
        {
            var newMapOneMassList = new MapOneMass[newNumberX, newNumberY];

            //前のMapImageから、はみ出した部分があれば削除する
            for (int x = newNumberX; x < MapSizeX; x++)
            {
                for (int y = 0; y < MapSizeY; y++)
                {
                    mapOneMassList[x, y].Dispose();
                }
            }
            for (int x = 0; x < newNumberX; x++)
            {
                for (int y = newNumberY; y < MapSizeY; y++)
                {
                    mapOneMassList[x, y].Dispose();
                }
            }

            //新しいMapImageに前のMapImageをサイズの範囲内でコピーする
            for (int x = 0;
                x < MapSizeX && x <newNumberX;
                x++)
            {
                for (int y = 0;
                    y < MapSizeY && y < newNumberY;
                    y++)
                {
                    newMapOneMassList[x, y] = mapOneMassList[x, y];
                }
            }

            //新しいMapImageの新しく生成された領域を初期化する
            for (int x = MapSizeX; x < newNumberX; x++)
            {
                for (int y = 0; y < newNumberY; y++)
                {
                    newMapOneMassList[x, y] = new MapOneMass(mcrm,MapChipSize);
                    newMapOneMassList[x, y].LocalPos=new DXEX.Vect(x * MapChipSize, y * MapChipSize);
                }
            }
            for (int x = 0; x < MapSizeX; x++)
            {
                for (int y = MapSizeY; y < newNumberY; y++)
                {
                    newMapOneMassList[x, y] = new MapOneMass(mcrm,MapChipSize);
                    newMapOneMassList[x, y].LocalPos=new DXEX.Vect(x * MapChipSize, y * MapChipSize);
                }
            }
            //新しいMadDataリストの方のポインタを保存
            mapOneMassList = newMapOneMassList;

        }

        //マップを右回転
        public void RotateRight()
        {
            var newMapOneMassList = new MapOneMass[MapSizeY, MapSizeX];
            for (int x = 0; x < newMapOneMassList.GetLength(0); x++)
            {
                for (int y = 0; y < newMapOneMassList.GetLength(1); y++)
                {
                    newMapOneMassList[x, y] = mapOneMassList[y, mapOneMassList.GetLength(1) - x - 1];
                    newMapOneMassList[x, y].LocalPos=new DXEX.Vect(x * MapChipSize, y * MapChipSize);
                    newMapOneMassList[x, y].RotateRight();
                }
            }
            //新しいMadDataリストの方のポインタを保存
            mapOneMassList = newMapOneMassList;
        }

        //マップを左回転
        public void RotateLeft()
        {
            var newMapOneMassList = new MapOneMass[MapSizeY, MapSizeX];
            for (int x = 0; x < newMapOneMassList.GetLength(0); x++)
            {
                for (int y = 0; y < newMapOneMassList.GetLength(1); y++)
                {
                    newMapOneMassList[x, y] = mapOneMassList[mapOneMassList.GetLength(0) - y - 1, x];
                    newMapOneMassList[x, y].LocalPos=new DXEX.Vect(x * MapChipSize, y * MapChipSize);
                    newMapOneMassList[x, y].RotateLeft();
                }
            }
            //新しいMadDataリストの方のポインタを保存
            mapOneMassList = newMapOneMassList;
        }

        //マップを左右反転
        public void turnHorizontal()
        {
            var newMapOneMassList = new MapOneMass[MapSizeX, MapSizeY];
            for (int x = 0; x < newMapOneMassList.GetLength(0); x++)
            {
                for (int y = 0; y < newMapOneMassList.GetLength(1); y++)
                {
                    newMapOneMassList[x, y] = mapOneMassList[mapOneMassList.GetLength(0) - x - 1, y];
                    newMapOneMassList[x, y].LocalPos=new DXEX.Vect(x * MapChipSize, y * MapChipSize);
                    newMapOneMassList[x, y].TurnHorizontal();
                }
            }
            //新しいMadDataリストの方のポインタを保存
            mapOneMassList = newMapOneMassList;
        }

        //マップを上下反転
        public void turnVertical()
        {
            var newMapOneMassList = new MapOneMass[MapSizeX, MapSizeY];
            for (int x = 0; x < newMapOneMassList.GetLength(0); x++)
            {
                for (int y = 0; y < newMapOneMassList.GetLength(1); y++)
                {
                    newMapOneMassList[x, y] =mapOneMassList[x, mapOneMassList.GetLength(1) - y - 1];
                    newMapOneMassList[x, y].LocalPos=new DXEX.Vect(x * MapChipSize, y * MapChipSize);
                    newMapOneMassList[x, y].TurnVertical();
                }
            }
            //新しいMadDataリストの方のポインタを保存
            mapOneMassList = newMapOneMassList;
        }

        public void UpdateShowMapImage(MapWriteScene mws)
        {
            MapShowArea.UpdateShowMapImage(mws, mapOneMassList);
        }
    }
}

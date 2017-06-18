using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEdit
{
    //マップデータの回転をつかさどる神
    public class TurnController:MapDataController
    {
        public TurnController(MapData mapData)
            : base(mapData) {} 

        //マップを右回転
        public void RotateRight()
        {
            var newMapOneMassList = new MapOneMass[mapData.MapSizeY, mapData.MapSizeX];
            for (int x = 0; x < newMapOneMassList.GetLength(0); x++)
            {
                for (int y = 0; y < newMapOneMassList.GetLength(1); y++)
                {
                    newMapOneMassList[x, y] = mapData.list[y, mapData.list.GetLength(1) - x - 1];
                    newMapOneMassList[x, y].LocalPos = new DXEX.Vect(x * mapData.MapChipSize, y * mapData.MapChipSize);
                    newMapOneMassList[x, y].RotateRight();
                }
            }
            //新しいMadDataリストの方のポインタを保存
            mapData.list = newMapOneMassList;
        }

        //マップを左回転
        public void RotateLeft()
        {
            var newMapOneMassList = new MapOneMass[mapData.MapSizeY, mapData.MapSizeX];
            for (int x = 0; x < newMapOneMassList.GetLength(0); x++)
            {
                for (int y = 0; y < newMapOneMassList.GetLength(1); y++)
                {
                    newMapOneMassList[x, y] = mapData.list[mapData.list.GetLength(0) - y - 1, x];
                    newMapOneMassList[x, y].LocalPos = new DXEX.Vect(x * mapData.MapChipSize, y * mapData.MapChipSize);
                    newMapOneMassList[x, y].RotateLeft();
                }
            }
            //新しいMadDataリストの方のポインタを保存
            mapData.list = newMapOneMassList;
        }

        //マップを左右反転
        public void turnHorizontal()
        {
            for (int x = 0; x < mapData.MapSizeX/2; x++)
            {
                for (int y = 0; y < mapData.MapSizeY; y++)
                {
                    mapData.list[x, y].SwapMapChips(mapData.list[mapData.MapSizeX - x - 1, y]);
                    mapData.list[mapData.MapSizeX - x - 1, y].TurnHorizontal();
                    mapData.list[x, y].TurnHorizontal();
                }
            }
            //Xの要素数が奇数ならtrue
            //これは奇数個の時、左右反転させるときにどことも入れ替わらないマスがあるので
            //それの処理です
            bool oddFlag = mapData.MapSizeX % 2 == 1;

            if (oddFlag)
            {
                //中心の行を反転
                for (int y = 0; y < mapData.MapSizeY; y++)
                {
                    mapData.list[ mapData.MapSizeX / 2,y].TurnHorizontal();
                }
            }
        }

        //マップを上下反転
        public void turnVertical()
        {
            for (int y = 0; y < mapData.MapSizeY / 2; y++)
            {
                for (int x = 0; x < mapData.MapSizeX; x++)
                {

                    mapData.list[x, y].SwapMapChips(mapData.list[x, mapData.MapSizeY - y - 1]);
                    mapData.list[x, mapData.MapSizeY - y - 1].TurnVertical();
                    mapData.list[x, y].TurnVertical();
                }
            }

            //Yの要素数が奇数ならtrue
            //これは奇数個の時、上下反転させるときにどことも入れ替わらないマスがあるので
            //それの処理です
            bool oddFlag = mapData.MapSizeY % 2 == 1;

            if (oddFlag)
            {
                //中心の行を反転
                for (int x = 0; x < mapData.MapSizeX; x++)
                {
                    mapData.list[x, mapData.MapSizeY / 2].TurnVertical();
                }
            }
        }
    }
}

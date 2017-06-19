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
                    newMapOneMassList[x, y] = mapData.List[y, mapData.MapSizeY - x - 1];
                    newMapOneMassList[x, y].LocalPos = new DXEX.Vect(x * mapData.MapChipSize, y * mapData.MapChipSize);
                    newMapOneMassList[x, y].RotateRight();
                }
            }
            //新しいMadDataリストの方のポインタを保存
            mapData.List = newMapOneMassList;
        }

        //マップを左回転
        public void RotateLeft()
        {
            var newMapOneMassList = new MapOneMass[mapData.MapSizeY, mapData.MapSizeX];
            for (int x = 0; x < newMapOneMassList.GetLength(0); x++)
            {
                for (int y = 0; y < newMapOneMassList.GetLength(1); y++)
                {
                    newMapOneMassList[x, y] = mapData.List[mapData.MapSizeX - y - 1, x];
                    newMapOneMassList[x, y].LocalPos = new DXEX.Vect(x * mapData.MapChipSize, y * mapData.MapChipSize);
                    newMapOneMassList[x, y].RotateLeft();
                }
            }
            //新しいMadDataリストの方のポインタを保存
            mapData.List = newMapOneMassList;
        }

        //マップを左右反転
        public void TurnHorizontal()
        {
            for (int x = 0; x < mapData.MapSizeX/2; x++)
            {
                for (int y = 0; y < mapData.MapSizeY; y++)
                {
                    mapData.List[x, y].SwapMapChips(mapData.List[mapData.MapSizeX - x - 1, y]);
                    mapData.List[mapData.MapSizeX - x - 1, y].TurnHorizontal();
                    mapData.List[x, y].TurnHorizontal();
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
                    mapData.List[ mapData.MapSizeX / 2,y].TurnHorizontal();
                }
            }
        }

        //マップを上下反転
        public void TurnVertical()
        {
            for (int y = 0; y < mapData.MapSizeY / 2; y++)
            {
                for (int x = 0; x < mapData.MapSizeX; x++)
                {

                    mapData.List[x, y].SwapMapChips(mapData.List[x, mapData.MapSizeY - y - 1]);
                    mapData.List[x, mapData.MapSizeY - y - 1].TurnVertical();
                    mapData.List[x, y].TurnVertical();
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
                    mapData.List[x, mapData.MapSizeY / 2].TurnVertical();
                }
            }
        }
    }
}

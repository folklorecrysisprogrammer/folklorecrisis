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
            var newMapOneMassList = new MapOneMass[mapData.MapSizeX, mapData.MapSizeY];
            for (int x = 0; x < newMapOneMassList.GetLength(0); x++)
            {
                for (int y = 0; y < newMapOneMassList.GetLength(1); y++)
                {
                    newMapOneMassList[x, y] = mapData.list[mapData.list.GetLength(0) - x - 1, y];
                    newMapOneMassList[x, y].LocalPos = new DXEX.Vect(x * mapData.MapChipSize, y * mapData.MapChipSize);
                    newMapOneMassList[x, y].TurnHorizontal();
                }
            }
            //新しいMadDataリストの方のポインタを保存
            mapData.list = newMapOneMassList;
        }

        //マップを上下反転
        public void turnVertical()
        {
            var newMapOneMassList = new MapOneMass[mapData.MapSizeX, mapData.MapSizeY];
            for (int x = 0; x < newMapOneMassList.GetLength(0); x++)
            {
                for (int y = 0; y < newMapOneMassList.GetLength(1); y++)
                {
                    newMapOneMassList[x, y] = mapData.list[x, mapData.list.GetLength(1) - y - 1];
                    newMapOneMassList[x, y].LocalPos = new DXEX.Vect(x * mapData.MapChipSize, y * mapData.MapChipSize);
                    newMapOneMassList[x, y].TurnVertical();
                }
            }
            //新しいMadDataリストの方のポインタを保存
            mapData.list = newMapOneMassList;
        }
    }
}

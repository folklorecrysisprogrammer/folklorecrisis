using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEdit
{
    //マップのサイズを変更するコントローラ
    public class ChangeMapSizeController :MapDataController
    {
        public ChangeMapSizeController(MapData mapData)
        :base(mapData){ }

        //配列サイズ変更処理
        public void Change(int newNumberX, int newNumberY)
        {
            var newMapOneMassList = new MapOneMass[newNumberX, newNumberY];

            //前のMapImageから、はみ出した部分があれば削除する
            for (int x = newNumberX; x < mapData.MapSizeX; x++)
            {
                for (int y = 0; y < mapData.MapSizeY; y++)
                {
                    mapData.List[x, y].Dispose();
                }
            }
            for (int x = 0; x < newNumberX; x++)
            {
                for (int y = newNumberY; y < mapData.MapSizeY; y++)
                {
                    mapData.List[x, y].Dispose();
                }
            }

            //新しいMapImageに前のMapImageをサイズの範囲内でコピーする
            for (int x = 0;
                x < mapData.MapSizeX && x < newNumberX;
                x++)
            {
                for (int y = 0;
                    y < mapData.MapSizeY && y < newNumberY;
                    y++)
                {
                    newMapOneMassList[x, y] = mapData.List[x, y];
                }
            }

            //新しいMapImageの新しく生成された領域を初期化する
            for (int x = mapData.MapSizeX; x < newNumberX; x++)
            {
                for (int y = 0; y < newNumberY; y++)
                {
                    newMapOneMassList[x, y] = new MapOneMass(mapData.MapChipSize);
                    newMapOneMassList[x, y].LocalPos = new DXEX.Vect(x * mapData.MapChipSize, y * mapData.MapChipSize);
                }
            }
            for (int x = 0; x < mapData.MapSizeX; x++)
            {
                for (int y = mapData.MapSizeY; y < newNumberY; y++)
                {
                    newMapOneMassList[x, y] = new MapOneMass(mapData.MapChipSize);
                    newMapOneMassList[x, y].LocalPos = new DXEX.Vect(x * mapData.MapChipSize, y * mapData.MapChipSize);
                }
            }
            //新しいMadDataリストの方のポインタを保存
            mapData.List = newMapOneMassList;

        }
    }
}

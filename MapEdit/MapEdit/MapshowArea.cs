using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MapEdit
{
    //画面に見えてるマップチップだけをAddChildするクラス
    public static class MapShowArea
    {
        //画面に表示されているマップチップをRemoveChildする
        private static void ClearShowMapImage(MapDataControl mapData)
        {
            for (int x = 0; x < mapData.MapSizeX; x++)
            {
                for (int y = 0; y < mapData.MapSizeY; y++)
                {
                    if (mapData[x, y].Parent != null)
                    {
                        mapData[x, y].RemoveFromParent();
                    }
                }
            }
        }

        //画面に表示するマップチップをAddChildする
        private static void AddShowMapImage(MapWriteScene mws,MapDataControl mapData)
        {
            var panel = mws.control;
            //新たにlUpIndexを計算する
            Point newLUpIndex = mws.LocationToMap(new Point(0, 0), mapData.MapChipSize);
            //新たにrDownIndexを計算する
            Point newRDownIndex =
                new Point(
                    panel.Size.Width / mapData.MapChipSize + newLUpIndex.X + 1,
                    panel.Size.Height / mapData.MapChipSize + newLUpIndex.Y + 1
                );
            //画面に表示されるMapImageだけAddChild
            for (int x = newLUpIndex.X; x < newRDownIndex.X && x < mapData.MapSizeX; x++)
            {
                for (int y = newLUpIndex.Y; y < newRDownIndex.Y && y < mapData.MapSizeY; y++)
                {
                    mws.AddChild(mapData[x, y]);
                }
            }
        }

        //表示するMapImageをAddChildして、表示されなくなったMapImageをRemoveChildする
        public static void UpdateShowMapImage(MapWriteScene mws,MapDataControl mapData)
        {
            ClearShowMapImage(mapData);
            AddShowMapImage(mws,mapData);
        }


    }
}

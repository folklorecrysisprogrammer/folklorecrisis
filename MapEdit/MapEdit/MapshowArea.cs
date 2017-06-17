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
        private static void ClearShowMapImage(MapOneMass[,] mapDatas)
        {
            for (int x = 0; x < mapDatas.GetLength(0); x++)
            {
                for (int y = 0; y < mapDatas.GetLength(1); y++)
                {
                    if (mapDatas[x, y].Parent != null)
                    {
                        mapDatas[x, y].RemoveFromParent();
                    }
                }
            }
        }

        //画面に表示するマップチップをAddChildする
        private static void AddShowMapImage(MapWriteScene mws,MapOneMass[,] mapDatas,int mapChipSize)
        {
            var panel = mws.control;
            //新たにlUpIndexを計算する
            Point newLUpIndex = mws.LocationToMap(new Point(0, 0), mapChipSize);
            //新たにrDownIndexを計算する
            Point newRDownIndex =
                new Point(
                    panel.Size.Width / mapChipSize + newLUpIndex.X + 1,
                    panel.Size.Height / mapChipSize + newLUpIndex.Y + 1
                );
            //画面に表示されるMapImageだけAddChild
            for (int x = newLUpIndex.X; x < newRDownIndex.X && x < mapDatas.GetLength(0); x++)
            {
                for (int y = newLUpIndex.Y; y < newRDownIndex.Y && y < mapDatas.GetLength(1); y++)
                {
                    mws.AddChild(mapDatas[x, y]);
                }
            }
        }

        //表示するMapImageをAddChildして、表示されなくなったMapImageをRemoveChildする
        public static void UpdateShowMapImage(MapWriteScene mws,MapOneMass[,] mapDatas)
        {
            ClearShowMapImage(mapDatas);
            AddShowMapImage(mws,mapDatas,mapDatas[0,0].mapChips[0].MapChipSize);
        }


    }
}

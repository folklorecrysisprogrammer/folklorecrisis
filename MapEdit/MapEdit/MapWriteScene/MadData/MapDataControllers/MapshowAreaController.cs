using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MapEdit
{
    //画面に見えてるマップチップだけをAddChildするクラス
    public class MapShowAreaController:MapDataController
    {

        public MapShowAreaController(MapData mapData) 
            :base(mapData){ }

        //画面に表示されているマップチップをRemoveChildする
        private void ClearShowMapImage()
        {
            for (int x = 0; x < mapData.MapSizeX; x++)
            {
                for (int y = 0; y < mapData.MapSizeY; y++)
                {
                    if (mapData.List[x, y].Parent != null)
                    {
                        mapData.List[x, y].RemoveFromParent();
                    }
                }
            }
        }

        //画面に表示するマップチップをAddChildする
        private void AddShowMapImage(MapWriteScene mws,int mapChipSize)
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
            for (int x = newLUpIndex.X; x < newRDownIndex.X && x < mapData.MapSizeX; x++)
            {
                for (int y = newLUpIndex.Y; y < newRDownIndex.Y && y < mapData.MapSizeY; y++)
                {
                    mws.AddChild(mapData.List[x, y]);
                }
            }
        }

        //表示するMapImageをAddChildして、表示されなくなったMapImageをRemoveChildする
        public void UpdateShowMapImage(MapWriteScene mws)
        {
            ClearShowMapImage();
            AddShowMapImage(mws, mapData.List[0,0].mapChips[0].MapChipSize);
        }


    }
}

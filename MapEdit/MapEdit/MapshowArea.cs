using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MapEdit
{
    //マップを描画するところだけ表示するようにするクラス
    public class MapShowArea
    {
        private readonly MapWriteScene mapWriteScene;
        private readonly MapData mapData;
        public MapShowArea(MapWriteScene mapWriteScene,MapData mapData)
        {
            this.mapWriteScene = mapWriteScene;
            this.mapData = mapData;
        }
        //画面に表示されているマップチップをRemoveChildする
        private void ClearShowMapImage()
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
        private void AddShowMapImage()
        {
            var panel = mapWriteScene.control;
            //新たにlUpIndexを計算する
            Point newLUpIndex = mapWriteScene.LocationToMap(new Point(0, 0), mapData.MapChipSize);
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
                    mapWriteScene.AddChild(mapData[x, y]);
                }
            }
        }

        //表示するMapImageをAddChildして、表示されなくなったMapImageをRemoveChildする
        public void UpdateShowMapImage()
        {
            ClearShowMapImage();
            AddShowMapImage();
        }


    }
}

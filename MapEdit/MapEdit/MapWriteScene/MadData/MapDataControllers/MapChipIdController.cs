using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEdit
{
    //mapData.listのマップチップ達が持つIDを操作する神
   public class MapChipIdController:MapDataController
    {
        public MapChipIdController(MapData mapData)
            : base(mapData) { }

        //マップデータリストで特定のIDを持つマップチップのTextureを削除する
        public void RemoveId(int id, int lastid)
        {
            if (id == lastid) lastid = -999;
            foreach (var item in mapData.List)
            {
                foreach (var mapChip in item.mapChips)
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

        //マップデータリストで特定のIDの交換を行う
        public void SwapId(int id1, int id2)
        {
            foreach (var item in mapData.List)
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
    }
}

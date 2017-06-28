using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEdit
{
    //あるデータを元にmapData.listを生成するクラス
   public class LoadMapDataListController:MapDataController
    {
        public LoadMapDataListController(MapData mapData)
            : base(mapData) { }

        //テキストデータからmapData.listをロードする
        public void LoadFromText(MapInfoFromText mift,MapChipResourceManager mcrm)
        {
            int count = 0;
            for (int y = 0; y < mapData.MapSizeY; y++)
            {
                for (int x = 0; x < mapData.MapSizeX; x++)
                {
                    for (int layer = 0; layer < MapEditForm.maxLayer; layer++)
                    {
                        if (mift.Id[count] != -1)
                        {
                            mapData.List[x, y].mapChips[layer].SetTexture(mcrm.GetTexture(mift.Id[count]));
                            mapData.List[x, y].mapChips[layer].SetId(mcrm.GetId(mift.Id[count]));
                            mapData.List[x, y].mapChips[layer].Angle = mift.Angle[count];
                            mapData.List[x, y].mapChips[layer].turnFlag = mift.Turn[count];
                        }
                        count++;
                    }
                }
            }
        }
    }
}

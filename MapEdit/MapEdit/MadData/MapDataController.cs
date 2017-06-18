using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEdit
{
    //マップデータをコントロールするインターフェイス
    public abstract class MapDataController
    {
        protected readonly MapData mapData;

        protected MapDataController(MapData mapData) {
            this.mapData=mapData;
        }
    }
}

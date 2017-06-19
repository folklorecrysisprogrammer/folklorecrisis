using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MapEdit
{
    //こいつの名前は速く変えるか殺すべきだ。
    public class MapEdit
    {
        
        public MapWriteScene mws { get; }
       
        //mapWriteSceneを生成するだけ
        public MapEdit(Panel mwp,HScrollBar hScroll,VScrollBar vScroll,Size mapSize,int mapChipSize)
        {
            
            mws =
                new MapWriteScene(mwp, new MapDataControl(mapSize, mapChipSize),hScroll,vScroll);
           
        }

        //miftの情報からmwsのmapData.listを構築する
        private MapEdit(MapInfoFromText mift,Panel mwp, MapChipResourceManager mcrm, HScrollBar hScroll, VScrollBar vScroll):
            this(mwp,hScroll,vScroll, mift.MapSize, mift.MapChipSize)
        {
            mws.MapDataControl.LoadMapDataList.LoadFromText(mift,mcrm);
        }

        //自殺してからprivateの方のコンストラクタ呼んで再び復活するだけのクソコード
        public MapEdit LoadProject(MapInfoFromText mift, Panel mwp, MapChipResourceManager mcrm, SelectImageForm sif, HScrollBar hScroll, VScrollBar vScroll)
        {
            this.Dispose();
            return new MapEdit(mift, mwp, mcrm, hScroll, vScroll);
        }

        //mwsと共に心中
        public void Dispose()
        {
            mws.Dispose();
        }
    }
}

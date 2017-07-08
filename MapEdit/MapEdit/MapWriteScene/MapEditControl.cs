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
    public class MapEditControl
    {

        private readonly MapWriteScene mws;
        private readonly MapChipConfigSprite mccs;

        //シーンをスクロールするクラス
        public MapWriteScroll MapWriteScroll { get; }

        //マップに配置するマップチップを管理するクラス
        public MapDataControl MapDataControl { get; }

        //mapWriteSceneを生成するだけ
        public MapEditControl(Panel mwp,HScrollBar hScroll,VScrollBar vScroll,Size mapSize,int mapChipSize)
        {
            MapDataControl = new MapDataControl(mapSize, mapChipSize);

            mws =
                new MapWriteScene(mwp,mapChipSize );
            mws.UpdateLocalPosEvent+=
                ()=>MapDataControl.MapShowArea.UpdateShowMapImage(mws);

            MapWriteScroll = new MapWriteScroll(hScroll, vScroll, mws, mapSize, mapChipSize);
            MapDataControl.
                setChangeListEvent(
                    () => 
                        MapWriteScroll.SetScrollMaximum(mapSize, mapChipSize));

            // マップチップの上に表示するスプライト
            mccs = new MapChipConfigSprite();
        }

        //miftの情報からmwsのmapData.listを構築する
        private MapEditControl(MapInfoFromText mift,Panel mwp, MapChipResourceManager mcrm, HScrollBar hScroll, VScrollBar vScroll):
            this(mwp,hScroll,vScroll, mift.MapSize, mift.MapChipSize)
        {
            MapDataControl.LoadMapDataList.LoadFromText(mift,mcrm);
        }

        //自殺してからprivateの方のコンストラクタ呼んで再び復活するだけのクソコード
        public MapEditControl LoadProject(MapInfoFromText mift, Panel mwp, MapChipResourceManager mcrm, SelectImageForm sif, HScrollBar hScroll, VScrollBar vScroll)
        {
            this.Dispose();
            return new MapEditControl(mift, mwp, mcrm, hScroll, vScroll);
        }

        //mwsと共に心中
        public void Dispose()
        {
            MapWriteScroll.Dispose();
            mws.Dispose();
        }

        public ConfigForm CreateConfigForm()
        {
            //ConfigFormを作成（第二引数は、MapSizeがConfigFormによって変更されるときの処理）
            return new 
                ConfigForm(
                    MapDataControl.MapSize,
                    (mapSize) =>
                    {
                        MapDataControl.MapSize = mapSize;
                        MapWriteScroll.SetScrollMaximum(MapDataControl.MapSize, MapDataControl.MapChipSize);
                    }
                );
        }

        //パネル上でマウスクリックされた時、マップチップを編集する
        public void MapMouseAction(Point point, int currentLayer, MapChip mapChip)
        {
            //左クリックされている時の処理
            if ((Control.MouseButtons & MouseButtons.Left)
                == MouseButtons.Left)
                //マップを書く
                MapDataControl.EditMapChip.
                    EditWrite(mws.LocationToMap(point, MapDataControl.MapChipSize), mapChip, currentLayer);

            //右クリックされている時の処理
           else if ((Control.MouseButtons & MouseButtons.Right)
                == MouseButtons.Right)
                //マップをクリアします
                MapDataControl.EditMapChip.EditErase(mws.LocationToMap(point, MapDataControl.MapChipSize), currentLayer);
        }
    }
}

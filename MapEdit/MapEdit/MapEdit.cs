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
        
        private readonly MapWriteScene mapWriteScene;
       
        public MapEdit(Panel mwp, MapChipResourceManager mcrm,SelectImageForm sif,HScrollBar hScroll,VScrollBar vScroll,Size mapSize,int mapChipSize)
        {
            
            mapWriteScene =
                new MapWriteScene(mwp, new MapDataControl(mcrm,mapSize, mapChipSize),hScroll,vScroll,sif);
           
        }

        public MapEdit(MapInfoFromText mift,Panel mwp, MapChipResourceManager mcrm, SelectImageForm sif, HScrollBar hScroll, VScrollBar vScroll):
            this(mwp,mcrm,sif,hScroll,vScroll, mift.MapSize, mift.MapChipSize)
        {
            mapWriteScene.LoadProject(mift,mcrm);
        }

        public MapEdit LoadProject(MapInfoFromText mift, Panel mwp, MapChipResourceManager mcrm, SelectImageForm sif, HScrollBar hScroll, VScrollBar vScroll)
        {
            this.Dispose();
            return new MapEdit(mift, mwp, mcrm, sif, hScroll, vScroll);
        }

        public void Dispose()
        {
            mapWriteScene.Dispose();
        }
        //マップの右回転
        public void MapRotateRight()
        {
            mapWriteScene.MapRotateRight();
        }

        //マップの左回転
        public void MapRotateLeft()
        {
            mapWriteScene.MapRotateLeft();
        }

        //マップの上下反転
        public void MapTurnVertical() {
            mapWriteScene.MapTurnVertical();
        }

        //マップの左右反転
        public void MapTurnHorizontal()
        {
            mapWriteScene.MapTurnHorizontal();
        }

        //キーによるスクロール
        public void KeyScroll(KeyEventArgs e)
        {
            mapWriteScene.KeyScroll(e);
        }

        //マップ描画のパネルのサイズが変更されたら、
        //マップに表示するスプライトの調整や
        //スクロールバーの調整を行う
        public void mapWritePanel_SizeChanged()
        {
            mapWriteScene.mapWritePanel_SizeChanged();
        }

        //パネル上でマウスが操作された時の処理をする
        public void MapMouseAction(MouseEventArgs e,int currentLayer)
        {
            mapWriteScene.MapMouseAction(e, currentLayer);
        }

        //マップをbitmapで得る
        public Bitmap GetBitmap()
        {
            return mapWriteScene.GetBitmap();
        }

        public StringBuilder GetMapDataText()
        {
            return mapWriteScene.GetMapDataText();
        }

        public ConfigForm CreateConfigForm()
        {
            return mapWriteScene.CreateConfigForm();
        }

        public void RemoveId(int id,int lastid)
        {
            mapWriteScene.RemoveId(id, lastid);
        }

        public void SwapId(int id1, int id2)
        {
            mapWriteScene.SwapId(id1, id2);
        }
    }
}

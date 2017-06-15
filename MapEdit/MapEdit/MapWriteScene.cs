using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace MapEdit
{
    //マップを表示するシーン
    public class MapWriteScene : MapSceneBase
    {
        //マップのグリッド
        private readonly MapGrid mapGrid;
        //マップに配置するマップチップを管理するクラス
        private readonly MapDataControl mapData;
        //シーンをスクロールするクラス
        private readonly MapWriteScroll mapWriteScroll;
        //マップをマウスで書き込むクラス
        private readonly EditMapChip editMapChip;
        //初期化                                 //描画先をmwpにする
        public MapWriteScene(Control control,int mapChipSize, MapChipResourceManager mcrm, HScrollBar hScroll, VScrollBar vScroll, Size MapSize, SelectImageForm sif, ComboBox layerComboBox) : base(control)
        {
            mapGrid = new MapGrid(this,mapChipSize);
            this.mapData = new MapDataControl(mcrm, MapSize, mapChipSize);
            mapWriteScroll = new MapWriteScroll(hScroll, vScroll, this, mapData.MapSize,mapData.MapChipSize);
            editMapChip = new EditMapChip(mapData, sif, this, layerComboBox);
            AddChild(mapGrid,1);
            LocalPos=new DXEX.Vect(0, 0);
            DXEX.DirectorForForm.AddSubScene(this);
        }

        protected override void UpdateLocalPos()
        {
            MapShowArea.UpdateShowMapImage(this, mapData);
        }
        protected override void Dispose(bool isFinalize)
        {
            DXEX.DirectorForForm.RemoveSubScene(this);
            mapWriteScroll.Dispose();
            base.Dispose(isFinalize);
        }
        //マップの右回転
        public void MapRotateRight()
        {
            mapData.RotateRight();
            //スクロールバーの調整
            mapWriteScroll.SetScrollMaximum(mapData.MapSize,mapData.MapChipSize);
        }

        //マップの左回転
        public void MapRotateLeft()
        {
            mapData.RotateLeft();
            //スクロールバーの調整
            mapWriteScroll.SetScrollMaximum(mapData.MapSize, mapData.MapChipSize);
        }

        //マップの上下反転
        public void MapTurnVertical()
        {
            mapData.turnVertical();
            MapShowArea.UpdateShowMapImage(this,mapData);
        }

        //マップの左右反転
        public void MapTurnHorizontal()
        {
            mapData.turnHorizontal();
            MapShowArea.UpdateShowMapImage(this,mapData);
        }

        //キーによるスクロール
        public void KeyScroll(KeyEventArgs e)
        {
            mapWriteScroll.KeyScroll(e);
        }

        //マップ描画のパネルのサイズが変更されたら、
        //マップに表示するスプライトの調整や
        //スクロールバーの調整を行う
        public void mapWritePanel_SizeChanged()
        {
            mapWriteScroll.SetScrollMaximum(mapData.MapSize, mapData.MapChipSize);
        }

        //パネル上でマウスが操作された時の処理をする
        public void MapMouseAction(MouseEventArgs e)
        {
            editMapChip.MouseAction(e);
        }
        //マップをbitmapで得る
        public Bitmap GetBitmap()
        {
            return mapData.GetBitmap();
        }

        public StringBuilder GetMapDataText()
        {
            return mapData.GetMapDataText();
        }

        public ConfigForm CreateConfigForm()
        {
            return new ConfigForm( mapData, mapWriteScroll);
        }

        public void RemoveId(int id, int lastid)
        {
            mapData.RemoveId(id, lastid);
        }

        public void SwapId(int id1, int id2)
        {
            mapData.SwapId(id1, id2);
        }

        //既存のプロジェクトからマップをロードする
        public void LoadProject(MapInfoFromText mift)
        {
            mapData.LoadProject(mift);
        }

    }
}

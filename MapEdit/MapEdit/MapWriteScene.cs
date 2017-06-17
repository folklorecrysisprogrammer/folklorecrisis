using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MapEdit
{
    //マップを表示するシーン
    public class MapWriteScene : MapSceneBase
    {
        //マップに配置するマップチップを管理するクラス
        private readonly MapDataControl mapDataControl;
        //シーンをスクロールするクラス
        private readonly MapWriteScroll mapWriteScroll;
        //マップをマウスで書き込むクラス
        private readonly EditMapChip editMapChip;
        //初期化                                 //描画先をmwpにする
        public MapWriteScene(Control control,int mapChipSize, MapChipResourceManager mcrm, HScrollBar hScroll, VScrollBar vScroll, Size MapSize, SelectImageForm sif, ComboBox layerComboBox) : base(control)
        {
            this.mapDataControl = new MapDataControl(mcrm, MapSize, mapChipSize);
            mapWriteScroll = new MapWriteScroll(hScroll, vScroll, this, mapDataControl.MapSize,mapDataControl.MapChipSize);
            editMapChip = new EditMapChip(mapDataControl, sif, layerComboBox);
            AddChild(new MapGrid(this,mapChipSize),1);
            LocalPos=new DXEX.Vect(0, 0);
            DXEX.DirectorForForm.AddSubScene(this);
        }

        protected override void UpdateLocalPos()
        {
            mapDataControl.UpdateShowMapImage(this);
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
            mapDataControl.RotateRight();
            //スクロールバーの調整
            mapWriteScroll.SetScrollMaximum(mapDataControl.MapSize,mapDataControl.MapChipSize);
        }

        //マップの左回転
        public void MapRotateLeft()
        {
            mapDataControl.RotateLeft();
            //スクロールバーの調整
            mapWriteScroll.SetScrollMaximum(mapDataControl.MapSize, mapDataControl.MapChipSize);
        }

        //マップの上下反転
        public void MapTurnVertical()
        {
            mapDataControl.turnVertical();
            mapDataControl.UpdateShowMapImage(this);
        }

        //マップの左右反転
        public void MapTurnHorizontal()
        {
            mapDataControl.turnHorizontal();
            mapDataControl.UpdateShowMapImage(this);
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
            mapWriteScroll.SetScrollMaximum(mapDataControl.MapSize, mapDataControl.MapChipSize);
        }

        //パネル上でマウスが操作された時の処理をする
        public void MapMouseAction(MouseEventArgs e)
        {
            editMapChip.MouseAction(LocationToMap(e.Location, mapDataControl.MapChipSize));
        }
        //マップをbitmapで得る
        public Bitmap GetBitmap()
        {
            return mapDataControl.GetBitmap();
        }

        public StringBuilder GetMapDataText()
        {
            return mapDataControl.GetMapDataText();
        }

        public ConfigForm CreateConfigForm()
        {
            return new ConfigForm( mapDataControl, mapWriteScroll);
        }

        public void RemoveId(int id, int lastid)
        {
            mapDataControl.RemoveId(id, lastid);
        }

        public void SwapId(int id1, int id2)
        {
            mapDataControl.SwapId(id1, id2);
        }

        //既存のプロジェクトからマップをロードする
        public void LoadProject(MapInfoFromText mift)
        {
            mapDataControl.LoadProject(mift);
        }

    }
}

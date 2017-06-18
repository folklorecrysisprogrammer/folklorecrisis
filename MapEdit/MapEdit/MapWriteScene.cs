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

        private readonly SelectImageForm sif;

        //初期化                                 //描画先をmwpにする
        public MapWriteScene(Control control,MapDataControl mapDataControl, HScrollBar hScroll, VScrollBar vScroll, SelectImageForm sif) : base(control)
        {
            this.sif = sif;
            this.mapDataControl = mapDataControl;
            mapWriteScroll = new MapWriteScroll(hScroll, vScroll, this, mapDataControl.MapSize,mapDataControl.MapChipSize);
            AddChild(new MapGrid(this,mapDataControl.MapChipSize),1);
            LocalPos=new DXEX.Vect(0, 0);
            DXEX.DirectorForForm.AddSubScene(this);
        }

        protected override void UpdateLocalPos()
        {
            mapDataControl.MapShowArea.UpdateShowMapImage(this);
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
            mapDataControl.Turn.RotateRight();
            //スクロールバーの調整
            mapWriteScroll.SetScrollMaximum(mapDataControl.MapSize,mapDataControl.MapChipSize);
        }

        //マップの左回転
        public void MapRotateLeft()
        {
            mapDataControl.Turn.RotateLeft();
            //スクロールバーの調整
            mapWriteScroll.SetScrollMaximum(mapDataControl.MapSize, mapDataControl.MapChipSize);
        }

        //マップの上下反転
        public void MapTurnVertical()
        {
            mapDataControl.Turn.turnVertical();
            mapDataControl.MapShowArea.UpdateShowMapImage(this);
        }

        //マップの左右反転
        public void MapTurnHorizontal()
        {
            mapDataControl.Turn.turnHorizontal();
            mapDataControl.MapShowArea.UpdateShowMapImage(this);
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

        //パネル上でマウスクリックされた時、マップチップを編集する
        public void MapMouseAction(MouseEventArgs e,int currentLayer)
        {
            //左クリックされている時の処理
            if ((Control.MouseButtons & MouseButtons.Left)
                == MouseButtons.Left)
            {
                //マップを書く
                mapDataControl.EditMapChip.EditWrite(LocationToMap(e.Location, mapDataControl.MapChipSize),sif.GetSelectMapChip(),currentLayer);
            }

            //右クリックされている時の処理
            if ((Control.MouseButtons & MouseButtons.Right)
                == MouseButtons.Right)
            {
                //マップをクリアします
                mapDataControl.EditMapChip.EditErase(LocationToMap(e.Location, mapDataControl.MapChipSize), currentLayer);
            }
        }

        //マップをbitmapで得る
        public Bitmap GetBitmap()
        {
            return mapDataControl.ConvertData.GetBitmap();
        }

        public StringBuilder GetMapDataText()
        {
            return mapDataControl.ConvertData.GetMapDataText();
        }

        public ConfigForm CreateConfigForm()
        {
            return new ConfigForm( mapDataControl, mapWriteScroll);
        }

        public void RemoveId(int id, int lastid)
        {
            mapDataControl.MapChipId.RemoveId(id, lastid);
        }

        public void SwapId(int id1, int id2)
        {
            mapDataControl.MapChipId.SwapId(id1, id2);
        }

        //既存のプロジェクトからマップをロードする
        public void LoadProject(MapInfoFromText mift,MapChipResourceManager mcrm)
        {
            mapDataControl.LoadMapDataList.LoadFromText(mift,mcrm);
        }

    }
}

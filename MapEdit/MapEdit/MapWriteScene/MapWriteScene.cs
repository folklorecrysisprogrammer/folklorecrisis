using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MapEdit
{
    //マップを表示するシーン
    public class MapWriteScene : MapSceneBase
    {
        //マップに配置するマップチップを管理するクラス
        public MapDataControl MapDataControl { get; }

        //シーンをスクロールするクラス
        private readonly MapWriteScroll mapWriteScroll;


        //初期化                                 //描画先をmwpにする
        public MapWriteScene(Control control,MapDataControl mapDataControl, HScrollBar hScroll, VScrollBar vScroll) : base(control)
        {
            this.MapDataControl = mapDataControl;
            mapDataControl.setChangeListEvent(
                () => mapWriteScroll.SetScrollMaximum(mapDataControl.MapSize, mapDataControl.MapChipSize));
            mapWriteScroll = new MapWriteScroll(hScroll, vScroll, this, mapDataControl.MapSize,mapDataControl.MapChipSize);
            AddChild(new MapGrid(this,mapDataControl.MapChipSize),1);
            LocalPos=new DXEX.Vect(0, 0);
            DXEX.DirectorForForm.AddSubScene(this);
        }

        //シーンの座標が更新されたら呼ばれる処理
        protected override void UpdateLocalPos()
        {
            MapDataControl.MapShowArea.UpdateShowMapImage(this);
        }

        protected override void Dispose(bool isFinalize)
        {
            DXEX.DirectorForForm.RemoveSubScene(this);
            mapWriteScroll.Dispose();
            base.Dispose(isFinalize);
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
            mapWriteScroll.SetScrollMaximum(MapDataControl.MapSize, MapDataControl.MapChipSize);
        }

        //パネル上でマウスクリックされた時、マップチップを編集する
        public void MapMouseAction(MouseEventArgs e,int currentLayer,MapChip mapChip)
        {
            //左クリックされている時の処理
            if ((Control.MouseButtons & MouseButtons.Left)
                == MouseButtons.Left)
            {
                //マップを書く
                MapDataControl.EditMapChip.
                    EditWrite(LocationToMap(e.Location, MapDataControl.MapChipSize),mapChip,currentLayer);
            }

            //右クリックされている時の処理
            if ((Control.MouseButtons & MouseButtons.Right)
                == MouseButtons.Right)
            {
                //マップをクリアします
                MapDataControl.EditMapChip.EditErase(LocationToMap(e.Location, MapDataControl.MapChipSize), currentLayer);
            }
        }

        public ConfigForm CreateConfigForm()
        {
            //ConfigFormを作成（第二引数は、MapSizeがConfigFormによって変更されるときの処理）
            return new ConfigForm(MapDataControl, (mapSize)=> {
                MapDataControl.MapSize = mapSize;
                mapWriteScroll.SetScrollMaximum(MapDataControl.MapSize, MapDataControl.MapChipSize);
            });
        }


    }
}

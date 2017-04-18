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

    //マップを表示、編集を行うシーン
    public class MapWriteScene : MapSceneBase
    {

        //親フォーム
        private MapEditForm meForm;

        //マップのスクロール制御用
        private readonly MapWriteScroll mapWriteScroll;
        public MapWriteScroll GetScroll() { return mapWriteScroll; }

        //現在のレイヤー
        public int CurrentLayer { get; set; }

        //マップの各マスの画像情報
        public MapData MapData{ get; }

        //初期化                                               //描画先をmwpにする
        public MapWriteScene(MapEditForm meForm,Size mapSize,int mapChipSize) : base(meForm.mwp)
        {
            this.meForm =meForm;
            MapData = new MapData(meForm,mapSize,mapChipSize);
            mapWriteScroll = new MapWriteScroll(meForm.Hscroll, meForm.Vscroll, this);
            localPos.SetVect(0, 0);
            UpdateShowMapImage();
        }

        //表示するMapImageをAddChildして、表示されなくなったMapImageをRemoveChildする
        public void UpdateShowMapImage()
        {
            Panel panel = meForm.mwp;
            //マップに表示されている左上のMapImageにアクセスするために、MapImage配列の添え字を計算する
            Point leftUpIndex = LocationToMap(new Point(0, 0),MapData.MapChipSize);
            //表示される領域はMapImage配列のどこからどこまでか計算
            Size showNumber = 
                new Size(panel.Size.Width / MapData.MapChipSize + 1, panel.Size.Height / MapData.MapChipSize + 1);
            //全てのMapImageを親から外す
            GetAllChildren().ForEach((child)=>{ child.RemoveFromParent();});
            //画面に表示されるMapImageだけAddChild
            for (int x = leftUpIndex.X; x < leftUpIndex.X + showNumber.Width && x<MapData.MapSizeX; x++)
            {
                for (int y = leftUpIndex.Y; y < leftUpIndex.Y+showNumber.Height && y < MapData.MapSizeY; y++)
                {
                    AddChild(MapData[x, y]);
                }
            }
        }

        //マウスでマップを書く処理
        public void MouseAction(object o, MouseEventArgs e)
        {
            Point point = LocationToMap(e.Location,MapData.MapChipSize);
            //マップサイズ範囲外なら終了
            if (point.X >= MapData.MapSizeX || point.Y >=MapData.MapSizeY ||
                point.X < 0 || point.Y < 0) return;

            //マップ処理
            if ((Control.MouseButtons & MouseButtons.Left)
                == MouseButtons.Left)
            {
                //左クリックされている時の処理
                //マップを書く
                MapData[point.X, point.Y].PutImage(meForm.sif.GetSelectMapChip(), CurrentLayer);
            }
            if ((Control.MouseButtons & MouseButtons.Right)
                == MouseButtons.Right)
            {
                //右クリックされている時の処理
                //マップをクリアします
                MapData[point.X, point.Y].ClearImage(CurrentLayer);
            }
        }

    }
}

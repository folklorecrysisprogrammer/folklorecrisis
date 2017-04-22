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

        //マップのスクロール制御用
        private readonly MapWriteScroll mapWriteScroll;
        public MapWriteScroll GetScroll() { return mapWriteScroll; }

        //現在のレイヤー
        public int CurrentLayer { get; set; }

        //マップの各マスの画像情報
        public MapData MapData{ get; }

        //マップのグリッド
        public MapGrid MapGrid { get; }

        //マップに表示されている左上のMapImageを表すMapImage配列の添え字
        private Point lUpIndex=new Point(0,0);
        private Point rDownIndex=new Point(-1,-1);

        //初期化                                               //描画先をmwpにする
        public MapWriteScene(MapEditForm meForm,Size mapSize,int mapChipSize) : base(meForm,meForm.mwp)
        {
            MapData = new MapData(meForm,mapSize,mapChipSize);
            MapGrid = new MapGrid(this,mapChipSize);
            AddChild(MapGrid,1);
            mapWriteScroll = new MapWriteScroll(meForm.Hscroll, meForm.Vscroll, this);
            localPos.SetVect(0, 0);
            UpdateShowMapImage();
        }

        //画面に表示されているマップチップをRemoveChildする
        public void ClearShowMapImage()
        {
            for (int x = lUpIndex.X; x < rDownIndex.X && x < MapData.MapSizeX; x++)
            {
                for (int y = lUpIndex.Y; y < rDownIndex.Y && y < MapData.MapSizeY; y++)
                {
                    MapData[x, y].RemoveFromParent();
                }
            }
            lUpIndex = new Point(0, 0);
            rDownIndex = new Point(-1, -1);
        }

        //画面に表示するマップチップをAddChildする
        public void AddShowMapImage()
        {
            Panel panel = meForm.mwp;
            //新たにlUpIndexを計算する
            Point newLUpIndex = LocationToMap(new Point(0, 0), MapData.MapChipSize);
            //新たにrDownIndexを計算する
            Point newRDownIndex =
                new Point(
                    panel.Size.Width / MapData.MapChipSize + newLUpIndex.X + 1,
                    panel.Size.Height / MapData.MapChipSize + newLUpIndex.Y + 1
                );
            //画面に表示されるMapImageだけAddChild
            for (int x = newLUpIndex.X; x < newRDownIndex.X && x < MapData.MapSizeX; x++)
            {
                for (int y = newLUpIndex.Y; y < newRDownIndex.Y && y < MapData.MapSizeY; y++)
                {
                    AddChild(MapData[x, y]);
                }
            }
            lUpIndex = newLUpIndex;
            rDownIndex = newRDownIndex;
        }

        //表示するMapImageをAddChildして、表示されなくなったMapImageをRemoveChildする
        public void UpdateShowMapImage()
        {
            ClearShowMapImage();
            AddShowMapImage();
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

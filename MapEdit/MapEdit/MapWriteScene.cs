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
    public class MapWriteScene : DXEX.Scene
    {

        private SelectImageForm selectImageForm;
        //マップチップの一辺の長さ
        private int mapChipSize = 40;
        public int MapChipSize
        {
            get { return mapChipSize; }
            set { mapChipSize = value; ChangeMapChipSize(); }
        }

        //マップのサイズ（マス単位で）
        public Size MapSize
        {
            get { return new Size(mapData.NumberX,mapData.NumberY); }
            set { ChangeMapSize(value); }
        }

        //表示先のパネル
        private Panel panel;

        //マップのスクロール制御用
        private MapWriteScroll mapWriteScroll;

        //現在のレイヤー
        public int CurrentLayer { get; set; }

        //マップの各マスの画像情報
        private readonly MapImageDataMap mapData;

        //初期化
        public MapWriteScene(SelectImageForm selectImageForm, Panel panel, HScrollBar hScroll, VScrollBar vScroll) : base(panel)
        {
            this.selectImageForm = selectImageForm;
            this.panel = panel;
            mapData = new MapImageDataMap(20,20, mapChipSize);
            this.mapWriteScroll = new MapWriteScroll(hScroll, vScroll, this);
            mapWriteScroll.SetScrollMaximum();
            mapWriteScroll.SetScrollDelta();
            panel.SizeChanged += (o, e) =>
            {
                mapWriteScroll.SetScrollMaximum();
            };
            panel.MouseDown += MouseAction;
            panel.MouseMove += MouseAction;
            localPos.SetVect(0, 0);
            

            //mapスプライトを登録
            for (int x = 0; x < mapData.NumberX; x++)
            {
                for (int y = 0; y < mapData.NumberY; y++)
                {
                    this.AddChild(mapData[x, y]);
                }
            }
        }

        //マウスでマップを書く処理
        private void MouseAction(object o, MouseEventArgs e)
        {
            Point point = MouseLocationToMapVect(e);
            //マップサイズ範囲外なら終了
            if (point.X >= mapData.NumberX || point.Y >=mapData.NumberY ||
                point.X < 0 || point.Y < 0) return;

            //マップ処理
            if ((Control.MouseButtons & MouseButtons.Left)
                == MouseButtons.Left)
            {
                //左クリックされている時の処理
                //マップを書く
                mapData[point.X, point.Y].PutImage(selectImageForm.GetSelectImage(), CurrentLayer);
            }
            if ((Control.MouseButtons & MouseButtons.Right)
                == MouseButtons.Right)
            {
                //右クリックされている時の処理
                //マップをクリアします
                mapData[point.X, point.Y].ClearImage(CurrentLayer);
            }
        }

        //マウスの座標からマップチップのマス座標へ変換
        private Point MouseLocationToMapVect(MouseEventArgs e)
        {
            Point point = new Point();
            point.X = (int)((e.Location.X - localPos.x) / mapChipSize);
            point.Y = (int)((e.Location.Y - localPos.y) / mapChipSize);
            return point;
        }

        //マップ全体をBitmapに変換する
        public Bitmap GetBitmap()
        {
            return mapData.GetBitmap();
        }

        //MapChipSize変更処理
        private void ChangeMapChipSize()
        {
            //スクロールバーの調整
            mapWriteScroll.SetScrollDelta();
            mapWriteScroll.SetScrollMaximum();
            mapData.ChangeMapChipSize(mapChipSize);
        }

        //MapSize変更処理
        private void ChangeMapSize(Size mapSize)
        {
            //スクロールバーの調整
            mapWriteScroll.SetScrollMaximum();
            mapData.ChangeMapSize(mapSize.Width, mapSize.Height);
            for (int x = 0; x < mapData.NumberX; x++)
            {
                for (int y = 0; y < mapData.NumberY; y++)
                {
                    if (mapData[x, y].Parent == null)
                    {
                        AddChild(mapData[x, y]);
                    }
                }
            }
        }

        //マップを右回転
        public void RotateRight()
        {
            mapData.RotateRight();
            mapWriteScroll.SetScrollMaximum();
        }

        //マップを左回転
        public void RotateLeft()
        {
            mapData.RotateLeft();
            mapWriteScroll.SetScrollMaximum();
        }

        //マップを左右反転
        public void turnHorizontal()
        {
            mapData.turnHorizontal();
        }

        //マップを上下反転
        public void turnVertical()
        {
            mapData.turnVertical();
        }
    }
}

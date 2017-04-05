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

        //表示されてるマスの左上のインデックス
        public Point showOriginMapImageIndex;
        //表示されているマスの数
        public Size ShowMapImageNumber;

        //マップチップの一辺の長さ
        public int MapChipSize
        {
            get { return mapData.MapChipSize; }
            set { ChangeMapChipSize(value); }
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
        private readonly MapWriteScroll mapWriteScroll;

        //現在のレイヤー
        public int CurrentLayer { get; set; }

        //マップの各マスの画像情報
        private readonly MapImageDataMap mapData;

        //初期化
        public MapWriteScene(SelectImageForm selectImageForm, Panel panel, HScrollBar hScroll, VScrollBar vScroll) : base(panel)
        {
            this.selectImageForm = selectImageForm;
            this.panel = panel;
            mapData = new MapImageDataMap(20,20,40);
            this.mapWriteScroll = new MapWriteScroll(hScroll, vScroll, this);
            mapWriteScroll.SetScrollMaximum();
            mapWriteScroll.SetScrollDelta();
            panel.SizeChanged += (o, e) =>
            {
                mapWriteScroll.SetScrollMaximum();
                UpdateShowMapImage();
            };
            panel.MouseDown += MouseAction;
            panel.MouseMove += MouseAction;
            localPos.SetVect(0, 0);

            UpdateShowMapImage();
        }

        //表示するMapImageをAddChildして、表示されなくなったMapImageをRemoveChildする
        public void UpdateShowMapImage()
        {
            Point tshowOriginMapImageIndex=showOriginMapImageIndex;
            Size tShowMapImageNumber=ShowMapImageNumber;
            showOriginMapImageIndex = LocationToMap(new Point(0, 0));
            ShowMapImageNumber = new Size(panel.Size.Width / MapChipSize+1, panel.Size.Height / MapChipSize+1);
            GetAllChildren().ForEach((child)=>{ child.RemoveFromParent();});
            for (int x = showOriginMapImageIndex.X; x < showOriginMapImageIndex.X + ShowMapImageNumber.Width && x<mapData.NumberX; x++)
            {
                for (int y = showOriginMapImageIndex.Y; y < showOriginMapImageIndex.Y+ShowMapImageNumber.Height && y < mapData.NumberY; y++)
                {
                    AddChild(mapData[x, y]);
                }
            }
        }

        //マウスでマップを書く処理
        private void MouseAction(object o, MouseEventArgs e)
        {
            Point point = LocationToMap(e.Location);
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

        //座標からマップチップのマス座標へ変換
        private Point LocationToMap(Point Location)
        {
            Point point = new Point();
            point.X = (int)((Location.X - localPos.x) / MapChipSize);
            point.Y = (int)((Location.Y - localPos.y) / MapChipSize);
            return point;
        }

        //マップ全体をBitmapに変換する
        public Bitmap GetBitmap()
        {
            return mapData.GetBitmap();
        }

        //MapChipSize変更処理
        private void ChangeMapChipSize(int mapChipSize)
        {
            mapData.ChangeMapChipSize(mapChipSize);
            //スクロールバーの調整
            mapWriteScroll.SetScrollDelta();
            mapWriteScroll.SetScrollMaximum();
            UpdateShowMapImage();
            
        }

        //MapSize変更処理
        private void ChangeMapSize(Size mapSize)
        {
            mapData.ChangeMapSize(mapSize.Width, mapSize.Height);

            //スクロールバーの調整
            mapWriteScroll.SetScrollMaximum();
            UpdateShowMapImage();
        }

        //マップを右回転
        public void RotateRight()
        {
            mapData.RotateRight();
            mapWriteScroll.SetScrollMaximum();
            UpdateShowMapImage();
        }

        //マップを左回転
        public void RotateLeft()
        {
            mapData.RotateLeft();
            mapWriteScroll.SetScrollMaximum();
            UpdateShowMapImage();

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

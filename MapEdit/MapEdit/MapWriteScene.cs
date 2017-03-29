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
   public class MapWriteScene:DXEX.Scene
    {

        private SelectImageForm selectImageForm;
        //マップチップの一辺の長さ
        private int pixelSize = 40;
        public int PixelSize
        {
            get { return pixelSize; }
            set { pixelSize = value; ChangePixelSize(); }
        }

        //マップのサイズ（マス単位で）
        private Size mapSize = new Size(20, 20);
        public Size MapSize
        {
            get { return mapSize; }
            set { mapSize = value; ChangeMapSize(); }
        }

        //表示先のパネル
        private Panel panel;
        private MapWriteScroll mapWriteScroll;
        //現在のレイヤー
        public int CurrentLayer { get; set; }

        //マップの各マスの画像情報
        private MapImage[,] mapImage;

        public MapWriteScene(SelectImageForm selectImageForm,Panel panel,HScrollBar hScroll,VScrollBar vScroll) : base(panel)
        {
            this.selectImageForm = selectImageForm;
            this.panel = panel;
            this.mapWriteScroll = new MapWriteScroll(hScroll,vScroll,this);
            mapWriteScroll.SetScrollMaximum();
            mapWriteScroll.SetScrollDelta();
            panel.SizeChanged += (o, e) =>
            {
                mapWriteScroll.SetScrollMaximum();
            };
            panel.MouseDown += MouseAction;
            panel.MouseMove += MouseAction;
            
            
            localPos.SetVect(0, 0);

            //Mapをスプライトで埋める
            mapImage = new MapImage[MapSize.Width, MapSize.Height];
            for (int x = 0; x < mapImage.GetLength(0); x++)
            {
                for (int y = 0; y < mapImage.GetLength(1); y++)
                {
                    mapImage[x, y] = new MapImage(pixelSize);
                    mapImage[x, y].localPos.SetVect(x * PixelSize, y * PixelSize);
                    this.AddChild(mapImage[x, y]);
                }
            }
        }
        
        //マウスでマップを書く処理
        private void MouseAction(object o, MouseEventArgs e)
        {
            
            //マップチップを未選択なら終了
            if (selectImageForm.SelectImagePath == "") return;
            Point point = MouseLocationToMapVect(e);
            //マップサイズ範囲外なら終了
            if (point.X >= mapImage.GetLength(0) || point.Y >= mapImage.GetLength(1) ||
                point.X < 0 || point.Y < 0) return;

            //マップ処理
            if ((Control.MouseButtons & MouseButtons.Left)
                == MouseButtons.Left)
            {
                //左クリックされている時の処理
                //マップを書く
                mapImage[point.X, point.Y].PutImage(selectImageForm.SelectImagePath, CurrentLayer);
            }
            if ((Control.MouseButtons & MouseButtons.Right)
                == MouseButtons.Right)
            {
                //右クリックされている時の処理
                //マップをクリアします
                mapImage[point.X, point.Y].ClearImage(CurrentLayer);
            }
        }

        //マウスの座標からマップチップのマス座標へ変換
        private Point MouseLocationToMapVect(MouseEventArgs e)
        {
            Point point=new Point();
            point.X = (int)((e.Location.X - localPos.x) / pixelSize);
            point.Y = (int)((e.Location.Y - localPos.y) / pixelSize);
            return point;
        }

        //マップ全体をBitmapに変換する
        public Bitmap GetBitmap()
        {
            Bitmap unitedImg = new Bitmap(pixelSize * mapSize.Width, pixelSize * mapSize.Height);
            Graphics g = Graphics.FromImage(unitedImg);
            for (int countY = 0; countY < mapSize.Height; ++countY)
            {
                for (int countX = 0; countX < mapSize.Width; ++countX)
                {
                    Bitmap bitmap = mapImage[countX, countY].GetBitmap();
                    g.DrawImage(bitmap, pixelSize * countX, pixelSize * countY);
                }
            }
            return unitedImg;
        }

        //PixelSize変更処理
        private void ChangePixelSize()
        {
            //スクロールバーの調整
            mapWriteScroll.SetScrollDelta();
            mapWriteScroll.SetScrollMaximum();

            //マップチップの位置とサイズ調整
             for (int x = 0; x < mapImage.GetLength(0); x++)
             {
                 for (int y = 0; y < mapImage.GetLength(1); y++)
                 {
                     mapImage[x, y].PixelSize =pixelSize;
                     mapImage[x, y].localPos.SetVect(x * PixelSize, y * PixelSize);
                 }
             }
        }

        //MapSize変更処理
        private void ChangeMapSize()
        {
            //スクロールバーの調整
            mapWriteScroll.SetScrollMaximum();

            var tMapImage = mapImage;
            mapImage = new MapImage[mapSize.Width, mapSize.Height];

            //前のMapImageから、はみ出した部分があれば削除する
            for (int x = mapImage.GetLength(0); x < tMapImage.GetLength(0); x++)
            {
                for (int y = 0; y < tMapImage.GetLength(1); y++)
                {
                    tMapImage[x, y].Dispose();
                }
            }
            for (int x = 0; x < mapImage.GetLength(0); x++)
            {
                for (int y = mapImage.GetLength(1); y < tMapImage.GetLength(1); y++)
                {
                    tMapImage[x, y].Dispose();
                }
            }

            //新しいMapImageに前のMapImageをサイズの範囲内でコピーする
            for (int x = 0;
                x < tMapImage.GetLength(0) && x < mapImage.GetLength(0);
                x++)
            {
                for (int y = 0;
                    y < tMapImage.GetLength(1) && y < mapImage.GetLength(1);
                    y++)
                {
                    mapImage[x, y] = tMapImage[x, y];
                }
            }

            //新しいMapImageの新しく生成された領域を初期化する
            for (int x = tMapImage.GetLength(0); x < mapImage.GetLength(0); x++)
            {
                for (int y = 0; y < mapImage.GetLength(1); y++)
                {
                    mapImage[x, y] = new MapImage(pixelSize);
                    mapImage[x, y].localPos.SetVect(x * PixelSize, y * PixelSize);
                    AddChild(mapImage[x, y]);
                }
            }
            for (int x = 0; x < tMapImage.GetLength(0); x++)
            {
                for (int y = tMapImage.GetLength(1); y < mapImage.GetLength(1); y++)
                {
                    mapImage[x, y] = new MapImage(pixelSize);
                    mapImage[x, y].localPos.SetVect(x * PixelSize, y * PixelSize);
                    AddChild(mapImage[x, y]);
                }
            }
        }
    }
}

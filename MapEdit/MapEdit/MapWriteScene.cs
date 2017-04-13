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

        private SelectImageForm selectImageForm;


        //表示先のパネル
        private Panel panel;

        //マップのスクロール制御用
        private readonly MapWriteScroll mapWriteScroll;
        public MapWriteScroll GetScroll() { return mapWriteScroll; }

        //現在のレイヤー
        public int CurrentLayer { get; set; }

        //マップの各マスの画像情報
        private readonly MapData mapData;
        public MapData GetMapData() { return mapData; }

        //初期化
        public MapWriteScene(SelectImageForm selectImageForm, Panel panel, HScrollBar hScroll, VScrollBar vScroll,MapEditForm meForm) : base(panel)
        {
            this.selectImageForm = selectImageForm;
            this.panel = panel;
            mapData = new MapData(meForm);
            mapWriteScroll = new MapWriteScroll(hScroll, vScroll, this);
            
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
            Point showOriginMapImageIndex= LocationToMap(new Point(0, 0),mapData.MapChipSize);
            Size ShowMapImageNumber= new Size(panel.Size.Width / mapData.MapChipSize + 1, panel.Size.Height / mapData.MapChipSize + 1);
            GetAllChildren().ForEach((child)=>{ child.RemoveFromParent();});
            for (int x = showOriginMapImageIndex.X; x < showOriginMapImageIndex.X + ShowMapImageNumber.Width && x<mapData.MapSize.Width; x++)
            {
                for (int y = showOriginMapImageIndex.Y; y < showOriginMapImageIndex.Y+ShowMapImageNumber.Height && y < mapData.MapSize.Height; y++)
                {
                    AddChild(mapData[x, y]);
                }
            }
        }

        //マウスでマップを書く処理
        private void MouseAction(object o, MouseEventArgs e)
        {
            Point point = LocationToMap(e.Location,mapData.MapChipSize);
            //マップサイズ範囲外なら終了
            if (point.X >= mapData.MapSize.Width || point.Y >=mapData.MapSize.Height ||
                point.X < 0 || point.Y < 0) return;

            //マップ処理
            if ((Control.MouseButtons & MouseButtons.Left)
                == MouseButtons.Left)
            {
                //左クリックされている時の処理
                //マップを書く
                mapData[point.X, point.Y].PutImage(selectImageForm.GetSelectMapChip(), CurrentLayer);
            }
            if ((Control.MouseButtons & MouseButtons.Right)
                == MouseButtons.Right)
            {
                //右クリックされている時の処理
                //マップをクリアします
                mapData[point.X, point.Y].ClearImage(CurrentLayer);
            }
        }

    }
}

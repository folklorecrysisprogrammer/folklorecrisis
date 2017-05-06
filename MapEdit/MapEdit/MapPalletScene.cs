using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace MapEdit
{
    //マップパレットフォームのパネルのとこに表示される画像を制御するシーン
   public class MapPalletScene:MapSceneBase
    {
        private readonly MapPalletData mapPalletData;
        private readonly MapEditForm meForm;
        private SelectMapChipScene sms;
        private Point tempPoint;
        private Point tempPoint2;
        public MapPalletScene(Panel panel,MapEditForm meForm,SelectMapChipScene sms) : base(panel)
        {
            this.meForm = meForm;
            panel.MouseDown += MouseAction;
            panel.MouseMove += MouseDrag;
            this.sms = sms;
            mapPalletData = new MapPalletData();
            localPos.SetVect(0, 0);
            AddChild(new MapGrid(this, 40), 1);
        }

        //ファイルから新しいマップチップを生成し登録する
        public void AddMapChip(string fileName)
        {
            MapChip mapChip = new MapChip(40);
            try
            {
                mapChip.SetTexture(fileName);
                mapChip.Id=meForm.mcrm.PushImageFile(fileName);
            }
            catch (Exception)
            {
                return;
            }
            mapPalletData.AddMapChip(mapChip);
            AddChild(mapChip);
        }

        //クリックされた場所にあるマップチップを選択OR削除する
        private void MouseAction(object o, MouseEventArgs e)
        {
            Point point = LocationToMap(e.Location, 40);
            if (mapPalletData[point.X, point.Y] == null) return;

            //左クリックの処理（選択）
            if ((Control.MouseButtons & MouseButtons.Left)
                == MouseButtons.Left)
            {
                tempPoint = point;
                tempPoint2 = point;
                sms.setMapChip(
                    mapPalletData[point.X, point.Y].GetTexture(),
                    mapPalletData[point.X, point.Y].Id
                );return;
            }

            //右クリックの処理（削除）
            if ((Control.MouseButtons & MouseButtons.Right)
                == MouseButtons.Right)
            {
                RemoveMapChip(point.X, point.Y);
            }
        }
        //クリックされた場所にあるマップチップを選択する
        private void MouseDrag(object o,MouseEventArgs e)
        {
            if ((Control.MouseButtons & MouseButtons.Left)
                != MouseButtons.Left) return;
                Point point = LocationToMap(e.Location, 40);
            if (point.X < 0 || point.Y < 0 || point.X >= 6 || point.Y >= 50) return;
            if (mapPalletData[point.X, point.Y] == null) return;
            if (tempPoint2 == point) return;
            if (tempPoint == point)
            {
                SwapMapChip(tempPoint2.X, tempPoint2.Y, tempPoint.X, tempPoint.Y);
                tempPoint2 = point;
                return;
            }
            SwapMapChip(point.X, point.Y, tempPoint2.X, tempPoint2.Y);
            SwapMapChip(tempPoint2.X, tempPoint2.Y, tempPoint.X, tempPoint.Y);
            tempPoint2 = point;
        }

        //指定座標のマップチップを削除する
        private void RemoveMapChip(int x,int y)
        {
            int removeId = mapPalletData[x,y].Id;
            sms.RemoveId(removeId, meForm.mcrm.LastID());
            meForm.RemoveId(removeId);
            mapPalletData.RemoveMapChip(x, y);
        }

        //指定座標のマップチップを入れ替える
        private void SwapMapChip(int x1, int y1, int x2, int y2)
        {
            meForm.SwapId(mapPalletData[x1, y1].Id, mapPalletData[x2, y2].Id);
            mapPalletData.SwapMapChip(x1, y1, x2, y2);

        }

        //プロジェクトからマップチップパレットをロードする
        public void LoadProject()
        {
            mapPalletData.ClearMapChip();
            for(int id = 0; id <=meForm. mcrm.LastID(); id++)
            {
                MapChip mapChip = new MapChip(40);
                mapChip.SetTexture(meForm.mcrm.GetTexture(id));
                mapChip.Id = id;
                mapPalletData.AddMapChip(mapChip);
                AddChild(mapChip);
            }
        }

    }
}

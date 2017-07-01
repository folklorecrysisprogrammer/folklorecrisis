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
        private MapChipResourceManager mcrm;
        private SelectMapChipScene sms;
        private MouseSwap mouseSwap;
        public MapPalletScene(Panel panel,MapChipResourceManager mcrm,SelectMapChipScene sms) : base(panel)
        {
            mouseSwap = new MouseSwap();
            this.mcrm = mcrm;
            panel.MouseDown += MouseClickAction;
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
            // : MapChipがSpriteを所持ではなく継承しちゃってる
            try
            {
                mapChip.SetTexture(fileName);
                mapChip.SetId(mcrm.PushImageFile(fileName));
            }
            catch (Exception)
            {
                return;
            }
            mapPalletData.AddMapChip(mapChip);
            AddChild(mapChip);
        }

        //クリックされた場所にあるマップチップを選択OR削除する
        private void MouseClickAction(object o, MouseEventArgs e)
        {
            Point point = LocationToMap(e.Location, 40);
            if (mapPalletData.ExsitMapChip(point.X, point.Y) == false) return;

            //左クリックの処理（選択）
            if ((Control.MouseButtons & MouseButtons.Left)
                == MouseButtons.Left)
            {
                if (MapChipConfig.passEditMode ==MapChipConfig.PassEditModeKind.編集中)
                {
                    // マップチップの通行判定編集
                    // ドラッグ系の処理どなってるんだ…
                    mapPalletData.ReverseEnablePassFlag(point.X, point.Y);
                }
                else
                {
                    sms.setMapChip(
                        mapPalletData.GetTexture(point.X, point.Y),
                        mapPalletData.GetId(point.X, point.Y)
                    );
                    mouseSwap.Start(point);
                }
                return;
            }

            //右クリックの処理（削除）
            if ((Control.MouseButtons & MouseButtons.Right)
                == MouseButtons.Right)
            {
                mapPalletData.RemoveMapChip(point.X, point.Y,mcrm);
            }
        }
        //クリックされた場所にあるマップチップを選択する
        private void MouseDrag(object o,MouseEventArgs e)
        {
            if (MapChipConfig.passEditMode == MapChipConfig.PassEditModeKind.編集中) return;

                if ((Control.MouseButtons & MouseButtons.Left)
                != MouseButtons.Left) return;
                Point point = LocationToMap(e.Location, 40);
            if (point.X < 0 || point.Y < 0 || point.X >= 6 || point.Y >= 50) return;
            if (mapPalletData.ExsitMapChip(point.X, point.Y) == false) return;
            mouseSwap.Move(point, mapPalletData, mcrm);
        }

        //プロジェクトからマップチップパレットをロードする
        public void LoadProject(MapChipResourceManager mcrm)
        {
            this.mcrm = mcrm;
            mapPalletData.ClearMapChip();
            for(int id = 0; id <=mcrm.LastID(); id++)
            {
                MapChip mapChip = new MapChip(40);
                mapChip.SetTexture(mcrm.GetTexture(id));
                mapChip.SetId(mcrm.GetId(id));
                mapPalletData.AddMapChip(mapChip);
                AddChild(mapChip);
            }
        }

        // 通行設定情報の表示の有無を設定
        public void SetDrawMapChipInfo( bool value )
        {
        }
    }
}

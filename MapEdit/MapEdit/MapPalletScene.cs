﻿using System;
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
        public MapPalletScene(Panel panel,MapEditForm meForm,SelectMapChipScene sms) : base(panel)
        {
            this.meForm = meForm;
            panel.MouseDown += MouseAction;
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

        //クリックされた場所にあるマップチップを選択する
        private void MouseAction(object o, MouseEventArgs e)
        {
            Point point = LocationToMap(e.Location, 40);
            if (mapPalletData[point.X, point.Y] == null) return;

            //左クリックの処理
            if ((Control.MouseButtons & MouseButtons.Left)
                == MouseButtons.Left)
            {
                sms.setMapChip(
                    mapPalletData[point.X, point.Y].GetTexture(),
                    mapPalletData[point.X, point.Y].Id
                );return;
            }

            //右クリックの処理
            if ((Control.MouseButtons & MouseButtons.Right)
                == MouseButtons.Right)
            {

                if (sms.MapChip.Id == mapPalletData[point.X, point.Y].Id)
                {
                    sms.resetMapChip();
                }
                if (sms.MapChip.Id == meForm.mcrm.LastID())
                {
                    sms.MapChip.Id = mapPalletData[point.X, point.Y].Id;
                }
                meForm.RemoveId(mapPalletData[point.X, point.Y].Id);
                
                mapPalletData[point.X, point.Y].Dispose();
                mapPalletData.RemoveMapChip(point.X, point.Y);
                
            }
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

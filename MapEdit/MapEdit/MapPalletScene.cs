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
        public MapGrid MapGrid { get; }
        private SelectMapChipScene sms;
        public MapPalletScene(Panel panel,MapEditForm meForm,SelectImageForm sif) : base(panel)
        {
            this.meForm = meForm;
            panel.MouseDown += MouseAction;
            this.sms = sif.SelectMapChipScene;
            mapPalletData = new MapPalletData();
            localPos.SetVect(0, 0);
            MapGrid = new MapGrid(this, 40);
            AddChild(MapGrid, 1);
        }

        //ファイルから新しいマップチップを生成し登録する
        public void AddMapChip(string fileName)
        {
            MapChip mapChip = new MapChip(40);
            try
            {
                mapChip.SetTexture(fileName);
                mapChip.Id=meForm.mcrm.pushImageFile(fileName);
            }
            catch (Exception)
            {
                return;
            }
            mapPalletData.AddMapChip(mapChip);
            AddChild(mapChip);
        }

        //クリックされた場所にあるマップチップを選択する
        private void MouseAction(object o,MouseEventArgs e)
        {
            Point point=LocationToMap(e.Location,40);
            if (mapPalletData[point.X, point.Y] == null) return;
            sms.setMapChip(
                mapPalletData[point.X, point.Y].GetTexture(),
                mapPalletData[point.X, point.Y].Id    
            );
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

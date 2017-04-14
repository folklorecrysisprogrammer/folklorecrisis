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

        private readonly MapChipResourceManager mcrm;

        private SelectMapChipScene sms;
        public MapPalletScene(Panel panel,SelectImageForm sif) : base(panel)
        {
            panel.MouseDown += MouseAction;
            this.sms = sif.SelectMapChipScene;
            mcrm = sif.MeForm.mcrm;
            mapPalletData = new MapPalletData();
            localPos.SetVect(0, 0);
        }

        //ファイルから新しいマップチップを生成し登録する
        private void AddMapChip(string fileName)
        {
            MapChip mapChip = new MapChip(40);
            try
            {
                mapChip.SetTexture(fileName);
                mapChip.Id=mcrm.pushImageFile(fileName);
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

        //プロジェクトからパレットをロードする
        public void Load()
        {

        }

    }
}

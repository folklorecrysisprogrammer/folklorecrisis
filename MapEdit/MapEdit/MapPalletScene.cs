using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace MapEdit
{
    class MapPalletScene:MapSceneBase
    {
        public MapPalletData MapPalletData { get; set; }

        private SelectMapChipScene sms;
        public MapPalletScene(Panel panel,SelectMapChipScene sms) : base(panel)
        {
            panel.MouseDown += MouseAction;
            this.sms = sms;
            MapPalletData = new MapPalletData();
            localPos.SetVect(0, 0);
        }

        public void AddMapChip(string fileName)
        {
            MapChip mapChip = new MapChip(40);
            try
            {
                mapChip.SetTexture(fileName);
            }
            catch (Exception)
            {
                return;
            }
            MapPalletData.AddMapChip(mapChip);
            AddChild(mapChip);
        }

        private void MouseAction(object o,MouseEventArgs e)
        {
            Point point=LocationToMap(e.Location,40);
            sms.setTexture(MapPalletData[point.X, point.Y].GetTexture());
        }

    }
}

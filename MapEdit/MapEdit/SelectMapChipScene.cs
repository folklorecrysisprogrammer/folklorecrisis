using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapEdit
{
    class SelectMapChipScene:DXEX.Scene
    {
        public MapChip MapChip { get; private set; }
        public SelectMapChipScene(Control control) : base(control)
        {
            localPos.SetVect(0, 0);
            MapChip = new MapChip(40);
            AddChild(MapChip);
        }
        public void setMapChip(DXEX.Texture texture,int id)
        {
            MapChip.SetTexture(texture);
            MapChip.Id = id;
        }
    }
}

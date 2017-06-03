using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// 選択中のマップチップを表示するクラス
namespace MapEdit
{
    public class SelectMapChipScene : DXEX.Scene
    {
        public MapChip MapChip { get; private set; }
        public SelectMapChipScene(Control control) : base(control)
        {
            localPos.SetVect(0, 0);
            // コンストラクタ: マップチップのサイズ
            MapChip = new MapChip(40);
            AddChild(MapChip);
        }
        public void setMapChip(DXEX.Texture texture, int id)
        {
            MapChip.SetTexture(texture);
            MapChip.Id = id;
        }

        public void resetMapChip()
        {
            MapChip.ClearTexture();
        }

        public void RemoveId(int removeId,int lastid)
        {
            if (MapChip.Id == removeId)
            {
                resetMapChip();
            }
            if (MapChip.Id == lastid)
            {
                MapChip.Id = removeId;
            }
        }
    }
}

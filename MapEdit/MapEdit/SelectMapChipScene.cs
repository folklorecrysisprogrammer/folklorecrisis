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
        public void setMapChip(DXEX.Texture texture, Id id)
        {
            MapChip.SetTexture(texture);
            MapChip.SetId(id);
        }

        public void resetMapChip()
        {
            MapChip.ClearImage();
        }
        //各種アクションの種類定義
        public enum ActionKind
        {
            rotateRight,
            rotateLeft,
            TurnVertical,
            TurnHorizontal
        }

        //各種アクションを実行
        public void DoAction(ActionKind actionKind)
        {
            switch (actionKind)
            {
                case ActionKind.rotateRight:
                    MapChip.Angle += 90;
                    break;
                case ActionKind.rotateLeft:
                    MapChip.Angle += 270;
                    break;
                case ActionKind.TurnVertical:
                    MapChip.TurnVertical();
                    break;
                case ActionKind.TurnHorizontal:
                    MapChip.TurnHorizontal();
                    break;
            }
        }
    }
}

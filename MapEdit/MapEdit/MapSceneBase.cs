using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MapEdit
{
    public class MapSceneBase:DXEX.Scene
    { 
        public MapSceneBase(Control control) : base(control){}

        //C#のコントロールの座標をSceneのMapChip配列やMapOneMass配列などの
        //インデックスに変換
        //(変換したい座標,Sceneのローカル座標,マスの大きさ)
        public Point LocationToMap(Point Location, int MassSize)
        {
            Point point = new Point();
            point.X = (int)((Location.X - localPos.x) / MassSize);
            point.Y = (int)((Location.Y - localPos.y) / MassSize);
            return point;
        }
    }
}

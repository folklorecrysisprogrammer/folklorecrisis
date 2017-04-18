using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXEX
{
   public class Colision : Node
    {
        Shape cShape;
        public Shape CShape
        {
            get
            {
                return cShape;
            }
            set
            {
                cShape = value;
            }
        }
        //描画処理
        public override void Draw()
        {
            if (cShape == null) return;
            cShape.DebugDraw();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXEX.Base
{
    public class Colision : Coroutine
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
        //当たり判定の描画処理
        public void DebugDraw()
        {
            if (cShape == null) return;
            cShape.DebugDraw();
        }
    }
}

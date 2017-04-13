using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace DXEX
{
    //ボタンコンポーネント
    class Button : Component<Colision>
    {
        public delegate void Handler();
        private Point point = new Point(new Node());
        //イベント
        public event Handler MouseDown;
        public override IEnumerator Update()
        {
            if ((DX.GetMouseInput() & DX.MOUSE_INPUT_LEFT) == 1)
            {
                int x, y;
                DX.GetMousePoint(out x, out y);
                point.node.LocalPos.SetVect(x, y);
                if (owner.CShape.CheckHit(point))
                {
                    MouseDown();
                }
            }
                yield break;
        }
    }
}

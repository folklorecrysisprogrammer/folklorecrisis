using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace DXEX.Base
{
    class Mouse:KeyBase
    {
        public Mouse(int keycode) : base(keycode)
        {

        }
        //特定のマウスボタンが押されてたらtrueを返す関数
        protected override bool ButtonCheck()
        {
            return (DX.GetMouseInput() & keycode) != 0 ? true : false;
        }
    }
}

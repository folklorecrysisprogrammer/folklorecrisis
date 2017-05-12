using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace DXEX.Base
{
    //キーのイベントを制御するクラス
    class Key : KeyBase
    {
        public Key(int keycode) : base(keycode)
        {

        }

        //特定のキーが押されてたらtrueを返す関数
        protected override bool ButtonCheck()
        {
            return DX.CheckHitKey(keycode) == 1 ? true : false;
        }
    }
}

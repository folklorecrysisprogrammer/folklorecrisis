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

        //ボタンの状態を監視し、更新し,イベントを呼ぶ関数
        public override void Update()
        {
            if (DX.CheckHitKey(keycode) == 1)
            {
                Count++;
                if (Count == 1) InvokeButtonDown();
                InvokeButtonKeepDown();
            }
            else
            {
                if (Count == 0) InvokeButtonKeepUp();
                else
                {
                    Count = 0;
                    InvokeButtonKeepUp();
                    InvokeButtonUp();
                }
            }
        }
    }
}

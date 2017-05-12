using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXEX.Base
{
    //ボタンのイベントのインターフェイスを提供するクラス
    //派生クラスには、キーやマウスのボタンイベントを取れるクラス
    //KeyクラスとMouseクラスがある
    abstract class KeyBase
    {
        //押された瞬間に呼ばれるイベント
        private ButtonEvent buttonDown;
        //離した瞬間に呼ばれるイベント
        private ButtonEvent buttonUp;
        //離し続けてる時に呼ばれるイベント
        private ButtonEvent buttonKeepUp;
        //押し続けてる時に呼ばれるイベント
        private ButtonEvent buttonKeepDown;
        //何フレーム押したかカウントする
        public int Count { get; private set; }
        //ボタンの識別コード
        public readonly int keycode;

        protected KeyBase(int keycode)
        {
            this.keycode = keycode;
            Count = 0;
        }

        //関数を追加
        public void AddButtonEvent(ButtonType bt, Action func)
        {
            switch (bt)
            {
                case ButtonType.UP:
                    buttonUp.Event += func;
                    break;
                case ButtonType.DOWN:
                    buttonDown.Event += func;
                    break;
                case ButtonType.KEEPUP:
                    buttonKeepUp.Event += func;
                    break;
                case ButtonType.KEEPDOWN:
                    buttonKeepDown.Event += func;
                    break;
            }
        }

        //関数を削除
        public void DeleteButtonEvent(ButtonType bt, Action func)
        {
            switch (bt)
            {
                case ButtonType.UP:
                    buttonUp.Event -= func;
                    break;
                case ButtonType.DOWN:
                    buttonDown.Event -= func;
                    break;
                case ButtonType.KEEPUP:
                    buttonKeepUp.Event -= func;
                    break;
                case ButtonType.KEEPDOWN:
                    buttonKeepDown.Event -= func;
                    break;
            }
        }
        //各種イベントの実行
        public void InvokeButtonDown() { buttonDown.EventDo(); }
        public void InvokeButtonUp() { buttonUp.EventDo(); }
        public void InvokeButtonKeepDown() { buttonKeepDown.EventDo(); }
        public void InvokeButtonKeepUp() { buttonKeepUp.EventDo(); }

        //ボタンの状態を監視し、更新し,イベントを呼ぶ関数
        public void Update()
        {
            if (ButtonCheck())
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
        //ボタンが押されてたらtrueを返す関数
        protected abstract bool ButtonCheck();
    }
}

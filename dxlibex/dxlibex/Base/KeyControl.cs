using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace DXEX.Base {
    //キーコード定義
   public enum KeyCode
    {
        //方向キー：キャラクターやカーソルの移動
        UP = 0,
        DOWN ,
        LEFT ,
        RIGHT,
        //エンターキー：決定、話しかける、宝箱を開ける等
        ENTER,
        //バックスペースキー：キャンセル、マップ上でメニューを開く
        BACKSPACE ,
        //スペースキー：マップ上、固有アクション
        SPACE ,
        //テンキー：ショートカット
        TENKEY0 ,
        TENKEY1 ,
        TENKEY2 ,
        TENKEY3 ,
        TENKEY4 ,
        TENKEY5 ,
        TENKEY6 ,
        TENKEY7 ,
        TENKEY8 ,
        TENKEY9 ,
        //F1キー別でWindow説明書を開く
        F1 ,
        //F2キーエクスプローラーを起動し、本ゲームのホームページを開く
        F2 ,
        //Xキー：メニュー開く
        X ,
    };
    

    //ボタンイベント構造体
   public struct ButtonEvent
    {
        public delegate void Handler();
        //イベント
        public event Handler Event;
        //イベント実行
        public void EventDo() {
            if (Event != null) Event();
        }
    }

    //ボタンの状態
   public enum ButtonType
    {
        UP,//離した瞬間
        DOWN,//押した瞬間
        KEEPUP,//離し続けてる時
        KEEPDOWN,//押し続けてる時
    }

    //キー判定をし、イベント、キーの状態を管理するクラス
  public  static class KeyControl
    {   
        //DXライブラリ用のキーコードと enum KeyCode との互換性を保つ用
        static int[] dxKeyCode = {
            DX.KEY_INPUT_UP,DX.KEY_INPUT_DOWN,DX.KEY_INPUT_LEFT,DX.KEY_INPUT_RIGHT,
            DX.KEY_INPUT_RETURN,DX.KEY_INPUT_BACK,DX.KEY_INPUT_SPACE,DX.KEY_INPUT_NUMPAD0,
            DX.KEY_INPUT_NUMPAD1,DX.KEY_INPUT_NUMPAD2,DX.KEY_INPUT_NUMPAD3,DX.KEY_INPUT_NUMPAD4,
            DX.KEY_INPUT_NUMPAD5,DX.KEY_INPUT_NUMPAD6,DX.KEY_INPUT_NUMPAD7,DX.KEY_INPUT_NUMPAD8,
            DX.KEY_INPUT_NUMPAD9,DX.KEY_INPUT_F1,DX.KEY_INPUT_F2,DX.KEY_INPUT_X };
        //押された瞬間に呼ばれるイベント
        static ButtonEvent[] buttonDown = new ButtonEvent[dxKeyCode.Length];
        //離した瞬間に呼ばれるイベント
        static ButtonEvent[] buttonUp = new ButtonEvent[dxKeyCode.Length];
        //離し続けてる時に呼ばれるイベント
        static ButtonEvent[] buttonKeepUp = new ButtonEvent[dxKeyCode.Length];
        //押し続けてる時に呼ばれるイベント
        static ButtonEvent[] buttonKeepDown = new ButtonEvent[dxKeyCode.Length];
        //キー入力状態を保持する変数
        static int[] keyData = new int[dxKeyCode.Length];
        //buttonイベントに関数追加  (ボタンの状態、キーコード、関数)
        static public void AddButtonEvent(ButtonType bt, KeyCode k, ButtonEvent.Handler h) {
            switch (bt)
            {
                case ButtonType.UP:
                    buttonUp[(int)k].Event += h;
                    break;
                case ButtonType.DOWN:
                    buttonDown[(int)k].Event += h;
                    break;
                case ButtonType.KEEPUP:
                    buttonKeepUp[(int)k].Event += h;
                    break;
                case ButtonType.KEEPDOWN:
                    buttonKeepDown[(int)k].Event += h;
                    break;
            }
        }
        //buttonイベントから関数削除
        static public void DeleteButtonEvent(ButtonType bt, KeyCode k, ButtonEvent.Handler h)
        {
            switch (bt)
            {
                case ButtonType.UP:
                    buttonUp[(int)k].Event -= h;
                    break;
                case ButtonType.DOWN:
                    buttonDown[(int)k].Event -= h;
                    break;
                case ButtonType.KEEPUP:
                    buttonKeepUp[(int)k].Event -= h;
                    break;
                case ButtonType.KEEPDOWN:
                    buttonKeepDown[(int)k].Event -= h;
                    break;
            }
        }
        //特定キーの状態を得る（0：押されてない,1:押した瞬間,それ以外:押されつづけたフレーム数）
        static public int GiveKey(KeyCode k)
        {
           return keyData[(int)k];
        }
        //メインループで呼び出すべき処理
        static public void Getkey()
        {
            for(int i= 0; i<dxKeyCode.Length;i++)
            {
                if (DX.CheckHitKey(dxKeyCode[i]) == 1)
                {
                    keyData[i]++;
                    if (keyData[i] == 1) buttonDown[i].EventDo();
                    buttonKeepDown[i].EventDo();
                }
                else {
                    if (keyData[i] == 0) buttonKeepUp[i].EventDo();
                    else
                    {
                        keyData[i] = 0;
                        buttonUp[i].EventDo();
                    }
                }
            }
        }
    };
}

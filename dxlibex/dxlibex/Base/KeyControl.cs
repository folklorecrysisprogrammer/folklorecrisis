using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace DXEX.Base {
    //キーコード定義
  /* public enum KeyCode
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
    };*/
    

    //ボタンイベント構造体
   public struct ButtonEvent
    {
        //イベント
        public event Action Event;
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

    class Key
    {
        //押された瞬間に呼ばれるイベント
        public ButtonEvent buttonDown;
        //離した瞬間に呼ばれるイベント
        public ButtonEvent buttonUp;
        //離し続けてる時に呼ばれるイベント
        public ButtonEvent buttonKeepUp ;
        //押し続けてる時に呼ばれるイベント
        public ButtonEvent buttonKeepDown;
        public int count=0;
    }

    //キー判定をし、イベント、キーの状態を管理するクラス
  public  static class KeyControl
    {
        static Dictionary<int, Key> keyList=new Dictionary<int, Key>();
        //DXライブラリ用のキーコードと enum KeyCode との互換性を保つ用
        /*static int[] dxKeyCode = {
            DX.KEY_INPUT_UP,DX.KEY_INPUT_DOWN,DX.KEY_INPUT_LEFT,DX.KEY_INPUT_RIGHT,
            DX.KEY_INPUT_RETURN,DX.KEY_INPUT_BACK,DX.KEY_INPUT_SPACE,DX.KEY_INPUT_NUMPAD0,
            DX.KEY_INPUT_NUMPAD1,DX.KEY_INPUT_NUMPAD2,DX.KEY_INPUT_NUMPAD3,DX.KEY_INPUT_NUMPAD4,
            DX.KEY_INPUT_NUMPAD5,DX.KEY_INPUT_NUMPAD6,DX.KEY_INPUT_NUMPAD7,DX.KEY_INPUT_NUMPAD8,
            DX.KEY_INPUT_NUMPAD9,DX.KEY_INPUT_F1,DX.KEY_INPUT_F2,DX.KEY_INPUT_X };*/
        /*//押された瞬間に呼ばれるイベント
        static ButtonEvent[] buttonDown = new ButtonEvent[dxKeyCode.Length];
        //離した瞬間に呼ばれるイベント
        static ButtonEvent[] buttonUp = new ButtonEvent[dxKeyCode.Length];
        //離し続けてる時に呼ばれるイベント
        static ButtonEvent[] buttonKeepUp = new ButtonEvent[dxKeyCode.Length];
        //押し続けてる時に呼ばれるイベント
        static ButtonEvent[] buttonKeepDown = new ButtonEvent[dxKeyCode.Length];
        //キー入力状態を保持する変数
        static int[] keyData = new int[dxKeyCode.Length];*/
        //buttonイベントに関数追加  (ボタンの状態、キーコード、関数)
        static public void AddButtonEvent(ButtonType bt, int keycode, Action func) {
            switch (bt)
            {
                case ButtonType.UP:
                    keyList[keycode].buttonUp.Event += func;
                    break;
                case ButtonType.DOWN:
                    keyList[keycode].buttonDown.Event += func;
                    break;
                case ButtonType.KEEPUP:
                    keyList[keycode].buttonKeepUp.Event += func;
                    break;
                case ButtonType.KEEPDOWN:
                    keyList[keycode].buttonKeepDown.Event += func;
                    break;
            }
        }
        //buttonイベントから関数削除
        static public void DeleteButtonEvent(ButtonType bt, int keycode , Action func)
        {
            switch (bt)
            {
                case ButtonType.UP:
                    keyList[keycode].buttonUp.Event -= func;
                    break;
                case ButtonType.DOWN:
                    keyList[keycode].buttonDown.Event -= func;
                    break;
                case ButtonType.KEEPUP:
                    keyList[keycode].buttonKeepUp.Event -= func;
                    break;
                case ButtonType.KEEPDOWN:
                    keyList[keycode].buttonKeepDown.Event -= func;
                    break;
            }
        }
        //使用するキーを登録
        static public void ResistKey(int keycode)
        {
            if (keyList.ContainsKey(keycode) == true) throw new Exception("既に登録されたキーです");
            keyList.Add(keycode, new Key());
        }
        //特定キーの状態を得る（0：押されてない,1:押した瞬間,それ以外:押されつづけたフレーム数）
        static public int GiveKey(int keycode)
        {
            return keyList[keycode].count;
        }
        //メインループで呼び出すべき処理
        static public void Getkey()
        {
            foreach(var keyPair in keyList)
            {
                if (DX.CheckHitKey(keyPair.Key) == 1)
                {
                    keyPair.Value.count++;
                    if (keyPair.Value.count == 1) keyPair.Value.buttonDown.EventDo();
                    keyPair.Value.buttonKeepDown.EventDo();
                }
                else {
                    if (keyPair.Value.count == 0) keyPair.Value.buttonKeepUp.EventDo();
                    else
                    {
                        keyPair.Value.count = 0;
                        keyPair.Value.buttonKeepUp.EventDo();
                        keyPair.Value.buttonUp.EventDo();
                    }
                }
            }
        }
    };
}

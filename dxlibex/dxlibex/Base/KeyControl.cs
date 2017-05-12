using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace DXEX.Base {

    //ボタンイベント構造体
   public struct ButtonEvent
    {
        //イベント
        public event Action Event;
        //イベント実行
        public void EventDo() {
            Event?.Invoke();
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
        //キーリスト
        static Dictionary<int, Key> keyList=new Dictionary<int, Key>();

        //buttonイベントに関数追加  (ボタンの状態、キーコード、関数)
        static public void AddButtonEvent(ButtonType bt, int keycode, Action func) {
            keyList[keycode].AddButtonEvent(bt, func);
        }
        //buttonイベントから関数削除
        static public void DeleteButtonEvent(ButtonType bt, int keycode , Action func)
        {
            keyList[keycode].DeleteButtonEvent(bt, func);
        }
        //使用するキーを登録
        static public void ResistKey(int keycode)
        {
            if (keyList.ContainsKey(keycode) == true) throw new Exception("既に登録されたキーです");
            keyList.Add(keycode, new Key(keycode));
        }
        //特定キーの状態を得る（0：押されてない,1:押した瞬間,それ以外:押されつづけたフレーム数）
        static public int GiveKey(int keycode)
        {
            return keyList[keycode].Count;
        }
        //各種ボタンの状態を監視し、更新し、イベントを呼ぶ
        static public void Getkey()
        {
            foreach(var value in keyList.Values)
            {
                value.Update();
            }
        }
    };
}

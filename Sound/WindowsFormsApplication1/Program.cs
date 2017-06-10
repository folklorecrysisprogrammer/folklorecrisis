using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DXEX;
using DXEX.Base;
using DXEX.User;
using DxLibDLL;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 画面解像度をセット
            DX.ChangeWindowMode(DX.TRUE);
            DX.SetGraphMode(800, 500, 32);
            Director.init();

            Scene sc = new Scene();
            sc.LocalPos = new Vect(0, 0);

            // 使用するキーは手動で登録…？
            // (なにか登録しておかないとぬるぽｶﾞｯ)
            KeyControl.ResistKey(DX.KEY_INPUT_0);
            KeyControl.ResistKey(DX.KEY_INPUT_1);
            KeyControl.ResistKey(DX.KEY_INPUT_2);
            KeyControl.ResistKey(DX.KEY_INPUT_3);
            KeyControl.ResistKey(DX.KEY_INPUT_4);
            KeyControl.ResistKey(DX.KEY_INPUT_5);

            SoundTest st = new SoundTest("bgm1.mp3","bgm2.mp3","se.mp3");
            sc.AddChild(st);

            // ループを抜けたらENDも呼ばれる
            Director.StartLoop(sc);
        }
    }
}

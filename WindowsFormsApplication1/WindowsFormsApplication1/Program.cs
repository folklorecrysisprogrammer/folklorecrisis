﻿using System;
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
            DX.SetGraphMode(800, 600, 32);
            Director.init();

            Sprite sp = new Sprite("yyk.jpg");            
            Scene sc = new Scene();
            sc.LocalPos = new Vect(0, 0);
            sc.AddChild( sp );

            ShowSound ss = new ShowSound();
            sc.AddChild(ss);

            // ループを抜けたらENDも呼ばれる
            Director.StartLoop( sc );
        }
    }
}
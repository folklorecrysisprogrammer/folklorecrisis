using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using System.Windows.Forms;

namespace DXEX.Base
{
    /*ゲームのシーン管理をするクラス*/
   public static class Director
    {
        //現在のScene
        static private Scene mainScene;
        static public Scene GetScene() { return mainScene; }

        //次に切り替えるScene
        static private Scene nextScene;

        //シーン切り替え
        static public void ChangeScene(Scene nextScene)
        {
            mainScene.Dispose();
            mainScene = nextScene;
        }

        //DXライブラリの簡易初期化
        static public void init() {
            //初期化
            DX.DxLib_Init();
            DX.SetDrawScreen(DX.DX_SCREEN_BACK);
        }

        //メインループを起動
        static public void StartLoop(Scene _mainScene) {
            mainScene = _mainScene;
            MainLoop();
            DX.DxLib_End(); // DXライブラリ終了処理
        }

        //メインループ
        static private void MainLoop()
        {
            
            while (DX.ScreenFlip() == 0 && DX.ProcessMessage() == 0 && DX.ClearDrawScreen() == 0)
            {
                //シーンのUpdate
                mainScene.LoopDo();
                //透明度リセット
                DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA,255);

                FpsControl.Fps();
                FpsControl.FpsShow();
                //キー判定の更新
                KeyControl.UpdateKey();
            }
        }
       
    }
}

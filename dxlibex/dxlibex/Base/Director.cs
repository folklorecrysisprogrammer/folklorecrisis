using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using System.Windows.Forms;

namespace DXEX
{
    /*ゲームのシーン管理をするクラス*/
   public static class Director
    {

        //現在のScene（メインFormに当てる）
        static private Scene mainScene;
        //子Formに当てるScene
        static private SubSceneList subSceneList=new SubSceneList();
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
        //DXライブラリの簡易初期化(フォームと連携する場合)
        static public void init(Form mainForm)
        {
            //DxLibの親ウインドウをこのフォームウインドウにセット
            DX.SetUserWindow(mainForm.Handle);
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

        //シーンの追加（フォームと連携する場合のみ使用）
        static public void AddSubScene(Scene scene)
        {
            subSceneList.AddCheck(scene);
        }

        //シーンの削除（フォームと連携する場合のみ使用）
        static public void RemoveSubScene(Scene scene)
        {
            subSceneList.RemoveCheck(scene);
        }

        //メインループを起動(フォームと連携する場合)
        static public void StartLoop(Form mainForm,Scene _mainScene)
        {
            mainScene = _mainScene;
            AddSubScene(mainScene);
             //ループ
            MainLoop(mainForm);
            // DXライブラリ終了処理
            DX.DxLib_End(); 
        }
        //メインループを起動(フォームと連携する場合)
        static public void StartLoop(Form mainForm)
        {
            //ループ
            MainLoop(mainForm);
            // DXライブラリ終了処理
            DX.DxLib_End();
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
                //キー判定の更新
                KeyControl.Getkey();
            }
        }
        //メインループ(フォームと連携する場合)
        static private void MainLoop(Form mainForm)
        {

            while (mainForm.Created && DX.ProcessMessage() == 0)
            {
                DX.ClearDrawScreen();
                FpsControl.Fps();
                subSceneList.Lock();
                subSceneList.GetList.ForEach(
                    (scene) => {
                        DX.ClearDrawScreen();
                        DX.SetScreenFlipTargetWindow(scene.control.Handle);
                        scene.LoopDo();
                        FpsControl.FpsShow();
                        DX.SetDrawScreen(DX.DX_SCREEN_BACK);
                        DX.ScreenFlip();
                        //
                    }
                );
                subSceneList.UnLock();
                
              //  DX.SetScreenFlipTargetWindow(IntPtr.Zero);
               // mainScene.LoopDo();
                DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, 255);
                
                KeyControl.Getkey();
                Application.DoEvents();
            }
        }
    }
}

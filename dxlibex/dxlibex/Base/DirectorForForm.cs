using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DxLibDLL;

namespace DXEX.Base
{
    //ゲームのシーン管理をするクラス
    //Formと連携したいときにはDirectorではなくこちらをお使いください
    public static class DirectorForForm
    {
        //SceneList
        static private SubSceneList subSceneList = new SubSceneList();

        //DXライブラリの簡易初期化
        static public void init(Form mainForm)
        {
            DX.SetUserWindow(mainForm.Handle);
            DX.DxLib_Init();
            DX.SetDrawScreen(DX.DX_SCREEN_BACK);
        }

        //メインループを起動
        static public void StartLoop(Form mainForm, Scene StartScene)
        {
            AddSubScene(StartScene);

            //ループを呼ぶ
            MainLoop(mainForm);

            DX.DxLib_End();
        }
        static public void StartLoop(Form mainForm)
        {
            //ループを呼ぶ
            MainLoop(mainForm);

            DX.DxLib_End();
        }

        //メインループ
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
                    }
                );
                subSceneList.UnLock();
                DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, 255);
                KeyControl.Getkey();
                Application.DoEvents();
            }
        }

        //シーンの追加
        static public void AddSubScene(Scene scene)
        {
            subSceneList.AddCheck(scene);
        }

        //シーンの削除
        static public void RemoveSubScene(Scene scene)
        {
            subSceneList.RemoveCheck(scene);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace DXEX
{
    //フレームレート計測クラス
   public  static class FpsControl
    {
        static int wait = 30;
        static int count = 1;
        static int startTime=0;
        static double fps=1;

        //フレームレート計測＆フレームレート表示
        public static void Fps()
        {
            if (count == wait) {
                count = 1;
                fps=wait/(double)(DX.GetNowCount() - startTime)*1000.0;
                startTime = DX.GetNowCount();
            }
            else
            {
                count++;
            }
            DX.DrawString(0, 0, "FPS"+fps.ToString("F"), DX.GetColor(255, 255, 255));
        }

        public static double GetFps() { return fps; }
    }
}

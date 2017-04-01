using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXEX
{
    //コルーチンクラス
    public abstract class Coroutine
    {

        private IEnumerator ie;
        //待機時間
        private int wait = 0;
        //unityのようなUpdate関数
        public virtual IEnumerator Update(){ yield return 0;}
        //コンストラクタ
        public Coroutine() {
            ie = Update();
        }
        //Update関数を実行する関数
        public void UpdateDo()
        {
            if (wait == 0)
            {
                if (!ie.MoveNext())
                {
                    ie = Update();
                }
                else
                {
                    //待機時間を得る
                    wait = (int)ie.Current;
                }
            }
            else
            {
                //待機時間カウントする
                wait--;
            }
        }
    }
}

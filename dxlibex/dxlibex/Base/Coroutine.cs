using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXEX.Base
{
    //コルーチンクラス
    public abstract class Coroutine
    {

        private IEnumerator ie;
        //待機時間
        private int wait = 0;
        //途中中断可能なUpdate関数
        public virtual IEnumerator IeUpdate(){ yield return 0;}
        //毎フレーム呼ばれるUpdate関数。
        public virtual void Update() { }
        //コンストラクタ
        public Coroutine() {
            ie = IeUpdate();
        }
        //Update関数を実行する関数
        public void UpdateDo()
        {
            Update();
            if (wait == 0)
            {
                if (!ie.MoveNext())
                {
                    ie = IeUpdate();
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

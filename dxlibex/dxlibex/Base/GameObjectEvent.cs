using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXEX
{

    //GameObjectの各種イベント
    //ユーザーは任意にオーバーライドしてください。
    public partial class GameObject : Colision, IDisposable
    {
        //Angleが変更されたら呼ばれる関数
        protected virtual void UpdateAngle() { }

        //LocalPosが変更されたら呼ばれる処理
        protected virtual void UpdateLocalPos() { }

        //Scaleが変更されたら呼ばれる処理
        protected virtual void UpdateScale() { }
    }
}

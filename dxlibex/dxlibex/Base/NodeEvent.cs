using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXEX.Base
{

    //Nodeの各種イベント
    //ユーザーは任意にオーバーライドしてください。
    public partial class Node : GameObject
    {

        //親に取り付けられた時に呼ばれる
        protected virtual void Attach() { }

        //親から取り外された時に呼ばれる
        protected virtual void Detach() { }

    }
}
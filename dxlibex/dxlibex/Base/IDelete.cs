using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXEX.Base
{
    //削除すべきかどうかを判断するインターフェイス
    interface IDelete
    {
        bool RemoveFlag { get; set; }
    }
}

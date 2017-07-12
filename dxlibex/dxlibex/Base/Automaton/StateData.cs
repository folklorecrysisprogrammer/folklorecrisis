using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXEX.Base
{
    /// <summary>
    /// オートマトンの状態を保存するオブジェクト
    /// </summary>
    /// <typeparam name="Statetype">状態の型</typeparam>
    internal class StateData<Statetype>
    {
        public Statetype State { get; set; }
    }
}

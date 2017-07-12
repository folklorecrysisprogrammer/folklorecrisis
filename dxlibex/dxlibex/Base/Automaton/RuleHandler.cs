using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXEX.Base
{
    /// <summary>
    /// SemanticActionでRuleを間接的に操作するためのクラス
    /// </summary>
    public class RuleHandler<InputType,StateType>
        where InputType : IComparable
        where StateType :IComparable
    {
        internal RuleHandler(Rule<InputType,StateType> rule)
        {

        }
    }
}

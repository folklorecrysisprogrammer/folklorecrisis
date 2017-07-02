using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXEX.Base
{
    //セマンティックアクション。意味は分からんが、かっこいいからこの名前を付けた
    public class SemanticAction<InputType>
        where InputType : IComparable
    {
        virtual public void Do(Rule<InputType> rule)
        {
        }
    }


    //テスト
    public class KFCAction : SemanticAction<string>
    {
        public override void Do(Rule<string> rule)
        {
            DebugMessage.Mes("ケンタッキーフライドチキン", true);
        }
    }

    public class FC2Action : SemanticAction<string>
    {
        public override void Do(Rule<string> rule)
        {
            DebugMessage.Mes("みんな大好きFC2", true);
        }
    }
    public class Hao123Action : SemanticAction<string>
    {
        public override void Do(Rule<string> rule)
        {
            DebugMessage.Mes("実質ウイルス", true);
        }
    }
}

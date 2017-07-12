using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXEX.Base
{
    /// <summary>
    /// セマンティックアクションインターフェイス(意味解析の用語)
    /// </summary>
    /// <typeparam name="InputType">入力データの型</typeparam>
    /// <typeparam name="StateType">状態の型</typeparam>
    abstract public class SemanticAction<InputType,StateType>
        where InputType : IComparable
        where StateType : IComparable
    {
        public abstract void Do(RuleHandler<InputType, StateType> ruleh);
        /// <summary>
        /// 複製の関数
        /// この関数はRuleを複製するたびに呼ばれる非常に大切な関数です
        /// </summary>
        /// <returns>複製したもの</returns>
         public virtual SemanticAction<InputType, StateType> Clone()
        {
           return(SemanticAction<InputType,StateType>) this.MemberwiseClone();
        }
    }


    //テスト
    public class KFCAction : SemanticAction<string,string>
    {
        public override void Do(RuleHandler<string,string> ruleh)
        {
            DebugMessage.Mes("ケンタッキーフライドチキン", true);
        }
    }

    public class FC2Action : SemanticAction<string,string>
    {
        public override void Do(RuleHandler<string,string> ruleh)
        {
            DebugMessage.Mes("みんな大好きFC2", true);
        }
    }
    public class Hao123Action : SemanticAction<string,string>
    {
        public override void Do(RuleHandler<string,string> ruleh)
        {
            DebugMessage.Mes("実質ウイルス", true);
        }
    }
}

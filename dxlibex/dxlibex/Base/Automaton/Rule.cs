using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXEX.Base
{
    //ルール<発火の入力の型>
    public class Rule<InputType,StateType>
        where InputType : IComparable
        where StateType : IComparable
    {
        //発火するときに実行されるアクション
        List<SemanticAction<InputType,StateType>> actionList =
            new List<SemanticAction<InputType,StateType>>();

        //発火するときの入力データ
        readonly InputType inputData;

        public InputType InputData { get { return inputData; } }

        public Rule(InputType inputData)
        {
            this.inputData = inputData;
        }

        //発火
       internal void DoSemanticAction()
        {
            foreach (var action in actionList)
            {
                action.Do(new RuleHandler<InputType,StateType>(this));
            }
        }

        //実行するアクションを追加
        public static Rule<InputType,StateType> operator +(Rule<InputType,StateType> rule,SemanticAction<InputType,StateType> action)
        {
            rule.actionList.Add(action);
            return rule;
        }

        //二つのルールを合成してRulesにする
        public static Rules<InputType,StateType> operator |(Rule<InputType,StateType> rule1, Rule<InputType,StateType> rule2)
        {
            return new Rules<InputType, StateType>(rule1)|rule2;
        }

       /* public Rule<InputType,StateType> Clone()
        {
            
        }*/
    }


  
}

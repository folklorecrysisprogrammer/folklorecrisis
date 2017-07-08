using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXEX.Base
{
    //ルール<発火の入力の型>
    public class Rule<InputType>
        where InputType : IComparable
    {
        //発火するときに実行されるアクション
        List<SemanticAction<InputType>> actionList = new List<SemanticAction<InputType>>();

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
                action.Do(this);
            }
        }

        //実行するアクションを追加
        public static Rule<InputType> operator +(Rule<InputType> rule,SemanticAction<InputType> action)
        {
            rule.actionList.Add(action);
            return rule;
        }

        //二つのルールを合成してRulesにする
        public static Rules<InputType> operator |(Rule<InputType> rule1, Rule<InputType> rule2)
        {
            return new Rules<InputType>(rule1)|rule2;
        }
    }


    //ルールの集まり
    public class Rules<InputType>
        where InputType:IComparable
    {
        Rule<InputType>[] ruleList;
        public Rules(Rule<InputType> rule)
        {
            ruleList = new Rule<InputType>[1];
            ruleList[0]=rule;
        }

        public Rules(Rules<InputType> rules)
        {
            ruleList = new Rule<InputType>[rules.ruleList.Length];
            for(int i=0; i < ruleList.Length; i++)
            {
                ruleList[i] = rules.ruleList[i];
            }
        }

        public Rules(Rules<InputType> rules,Rule<InputType> rule)
        {
            ruleList = new Rule<InputType>[rules.ruleList.Length+1];
            int i;
            for (i = 0; i < ruleList.Length-1; i++)
            {
                ruleList[i] = rules.ruleList[i];
            }
            ruleList[i] = rule;
            
        }

        public Rules(Rules<InputType> rules1, Rules<InputType> rules2)
        {
            ruleList = new Rule<InputType>[rules1.ruleList.Length+rules2.ruleList.Length];
            for (int i = 0; i < rules1.ruleList.Length; i++)
            {
                ruleList[i] = rules1.ruleList[i];
            }
            for (int i = 0; i < rules2.ruleList.Length; i++)
            {
                ruleList[i+rules1.ruleList.Length] = rules2.ruleList[i];
            }
        }

        //Rulesに新しいルールを追加
        public static Rules<InputType> operator |(Rules<InputType> rules, Rule<InputType> rule)
        {
            return new Rules<InputType>(rules,rule);
        }

        //二つのRulesを合体
        public static Rules<InputType> operator |(Rules<InputType> rules1, Rules<InputType> rules2)
        {
            return new Rules<InputType>(rules1, rules2);
        }

        //入力を与える
        internal void Input(InputType inputData)
        {
            foreach(var rule in ruleList)
            {
                if (rule.InputData.CompareTo(inputData) == 0)
                {
                    rule.DoSemanticAction();
                    return;
                }
            }
        }
    }
}

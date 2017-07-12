using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXEX.Base
{
    //ルールの集まり
    public class Rules<InputType,StateType>
        where InputType : IComparable
        where StateType : IComparable
    {
        Rule<InputType,StateType>[] ruleList;
        public Rules(Rule<InputType, StateType> rule)
        {
            ruleList = new Rule<InputType, StateType>[1];
            ruleList[0] = rule;
        }

        public Rules(Rules<InputType, StateType> rules)
        {
            ruleList = new Rule<InputType, StateType>[rules.ruleList.Length];
            for (int i = 0; i < ruleList.Length; i++)
            {
                ruleList[i] = rules.ruleList[i];
            }
        }

        public Rules(Rules<InputType, StateType> rules, Rule<InputType, StateType> rule)
        {
            ruleList = new Rule<InputType, StateType>[rules.ruleList.Length + 1];
            int i;
            for (i = 0; i < ruleList.Length - 1; i++)
            {
                ruleList[i] = rules.ruleList[i];
            }
            ruleList[i] = rule;

        }

        public Rules(Rules<InputType, StateType> rules1, Rules<InputType, StateType> rules2)
        {
            ruleList = new Rule<InputType, StateType>[rules1.ruleList.Length + rules2.ruleList.Length];
            for (int i = 0; i < rules1.ruleList.Length; i++)
            {
                ruleList[i] = rules1.ruleList[i];
            }
            for (int i = 0; i < rules2.ruleList.Length; i++)
            {
                ruleList[i + rules1.ruleList.Length] = rules2.ruleList[i];
            }
        }

        //Rulesに新しいルールを追加
        public static Rules<InputType, StateType> operator |(Rules<InputType, StateType> rules, Rule<InputType, StateType> rule)
        {
            return new Rules<InputType, StateType>(rules, rule);
        }

        //二つのRulesを合体
        public static Rules<InputType, StateType> operator |(Rules<InputType, StateType> rules1, Rules<InputType, StateType> rules2)
        {
            return new Rules<InputType, StateType>(rules1, rules2);
        }

        //入力を与える
        internal void Input(InputType inputData)
        {
            foreach (var rule in ruleList)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXEX.Base
{
    //ルール
    public class Rule<Key>
    {
        List<SemanticAction<Key>> actionList = new List<SemanticAction<Key>>();
        Key state;

        public Key State { get { return state; } }

        public Rule(Key state)
        {
            this.state = state;
        }

        public static Rule<Key> operator +(Rule<Key> rule,SemanticAction<Key> action)
        {
            rule.actionList.Add(action);
            return rule;
        }
        public static Rules<Key> operator |(Rule<Key> rule1, Rule<Key> rule2)
        {
            return new Rules<Key>(rule1)|rule2;
        }
    }


    //ルールの集まり
    public class Rules<Key>
    {
        List<Rule<Key>> ruleList = new List<Rule<Key>>();
        public Rules(Rule<Key> rule)
        {
            ruleList.Add(rule);
        }
        public static Rules<Key> operator |(Rules<Key> rules, Rule<Key> rule)
        {
            rules.ruleList.Add(rule);
            return rules;
        }
    }
}

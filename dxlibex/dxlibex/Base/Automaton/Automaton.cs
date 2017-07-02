using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXEX.Base
{
    //全ての元凶
    class Automaton<StateType, InputType>
        where InputType : IComparable
    {
        //状態ごとにRulesと結びつけて保存するリスト
        Dictionary<StateType, Rules<InputType>> RulesMap=new Dictionary<StateType, Rules<InputType>>();
        //現在の状態
        public StateType NowState { get; private set; }
        public void SetState(StateType state, Rules<InputType> rules)
        {
        }
    }
}

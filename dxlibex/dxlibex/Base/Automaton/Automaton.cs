using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXEX.Base
{

    /// <summary>
    /// オートマトン風C++の潮風添えソテー
    /// </summary>
    /// <typeparam name="StateType">状態の型</typeparam>
    /// <typeparam name="InputType">入力データの型</typeparam>
   public class Automaton<StateType, InputType>
        where InputType : IComparable
        where StateType :IComparable
    {
        /// <summary>
        /// 状態ごとにRulesと結びつけて保存するリスト
        /// </summary>
        Dictionary<StateType, Rules<InputType>> rulesMap=new Dictionary<StateType, Rules<InputType>>();

        /// <summary>
        /// 現在の状態
        /// </summary>
        StateData<StateType> nowStateData=new StateData<StateType>();
        /// <summary>
        ///nowStateゲッター 
        /// </summary>
        public StateType NowState { get { return nowStateData.State; }}

        /// <summary>
        /// コンストラクタ、初期状態を設定する
        /// </summary>
        /// <param name="state">初期状態</param>
        public Automaton(StateType initState) {
            nowStateData.State = initState;
        }

        /// <summary>
        /// 状態とルールを紐づけして登録する
        /// </summary>
        /// <param name="state">状態を指定</param>
        /// <param name="rules">指定した状態の時に適応されるルール</param>
        public void SetStateWithRules(StateType state, Rules<InputType> rules)
        {
            if (rulesMap.Keys.Contains(state))
                throw new Exception("その状態のルールは既に存在しています");
            rulesMap[state] = rules;
        }

        /// <summary>
        /// 入力を与える
        /// </summary>
        /// <param name="input">与える入力データ</param>
        public void Input(InputType input)
        {
            rulesMap[NowState].Input(input);
        }
    }
}

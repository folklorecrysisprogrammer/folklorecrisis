using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXEX.Base
{
    /// <summary>
    /// ルールのテンプレート（説明になってない）
    /// </summary>
    /// <typeparam name="InputType">入力データの型</typeparam>
    /// <typeparam name="StateType">状態の型</typeparam>
    class RuleTemplate<InputType, StateType>
        where InputType : IComparable
        where StateType : IComparable
    {
        /// <summary>
        ///発火するときに実行されるセマンティックアクションを生成する関数のリスト 
        /// </summary>
        List<Func<SemanticAction<InputType, StateType>>> actionMakeFuncList;

        /// <summary>
        /// 発火するときの入力データ
        /// </summary>
        readonly InputType inputData;

        public InputType InputData { get { return inputData; } }

        public RuleTemplate(InputType inputData)
        {
            this.inputData = inputData;
            actionMakeFuncList=
                new List<Func<SemanticAction<InputType, StateType>>>();
        }

        /// <summary>
        /// コピーコンストラクタ
        /// </summary>
        /// <param name="ruleTemplate"></param>
        public RuleTemplate(RuleTemplate<InputType,StateType> ruleTemplate)
        {
            this.inputData = ruleTemplate.inputData;
            actionMakeFuncList =new List<Func<SemanticAction<InputType, StateType>>>(ruleTemplate.actionMakeFuncList);
        }


        /// <summary>
        ///実行するセマンティックアクションを生成する関数を追加 
        /// </summary>
        /// <param name="ruleTemplate">追加先となるRuleTemplate</param>
        /// <param name="actionMakeFunc">追加するセマンティックアクション生成関数</param>
        /// <returns></returns>
        public static RuleTemplate<InputType, StateType> operator +
            (
                RuleTemplate<InputType, StateType> ruleTemplate, 
                Func<SemanticAction<InputType, StateType>> actionMakeFunc
            )
        {
            ruleTemplate.actionMakeFuncList.Add(actionMakeFunc);
            return ruleTemplate;
        }

        /// <summary>
        /// 自身を複製する。
        /// 複製されたオブジェクトを変更しても、オリジナルには影響が無い
        /// ことを保証します。
        /// </summary>
        /// <returns>複製されたRuleTemplate</returns>
        public RuleTemplate<InputType,StateType> Clone()
        {
            return new RuleTemplate<InputType, StateType>(this);
        }

        /// <summary>
        /// 二つのルールを合成してRuleTemplateGroupを作成する 
        /// </summary>
        /// <param name="ruleTemplate1">左辺のRuleTemplate</param>
        /// <param name="ruleTemplate2">右辺のRuleTemplate</param>
        /// <returns></returns>
        public static RuleTemplateGroup<InputType, StateType> operator |(
            RuleTemplate<InputType, StateType> ruleTemplate1, RuleTemplate<InputType, StateType> ruleTemplate2)
        {
            return new RuleTemplateGroup<InputType, StateType>(ruleTemplate1) | ruleTemplate2;
        }
    }


}

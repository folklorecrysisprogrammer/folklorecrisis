using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXEX.Base
{
    /// <summary>
    /// RuleTemplateの集まり
    /// こいつを元にしてRulesを作成する
    /// </summary>
    /// <typeparam name="InputType">入力データの型</typeparam>
    /// <typeparam name="StateType">状態の型</typeparam>
    class RuleTemplateGroup<InputType, StateType>
        where InputType : IComparable
        where StateType : IComparable
    {
        /// <summary>
        /// RuleTemplateのリスト
        /// </summary>
        RuleTemplate<InputType, StateType>[] ruleTemplateList;


        /// <summary>
        /// コピーコンストラクタ
        /// </summary>
        /// <param name="ruleTemplateGroup"></param>
        public RuleTemplateGroup(RuleTemplateGroup<InputType, StateType> ruleTemplateGroup)
        {
            ruleTemplateList = (RuleTemplate<InputType,StateType>[])ruleTemplateGroup.ruleTemplateList.Clone();
        }

        /// <summary>
        /// 自身を複製する。
        /// 複製されたオブジェクトを変更しても、オリジナルには影響が無い
        /// ことを保証します。
        /// </summary>
        /// <returns>複製されたRuleTemplateGroup</returns>
        public RuleTemplateGroup<InputType,StateType> Clone()
        {
            return new RuleTemplateGroup<InputType, StateType>(this);
        }

        public RuleTemplateGroup(RuleTemplate<InputType, StateType> ruleTemplate)
        {
            ruleTemplateList = new RuleTemplate<InputType, StateType>[1];
            ruleTemplateList[0] = ruleTemplate.Clone();
        }

        public RuleTemplateGroup(
            RuleTemplateGroup<InputType, StateType> ruleTemplateGroup, RuleTemplate<InputType, StateType> ruleTemplate)
        {
            ruleTemplateList = 
                new RuleTemplate<InputType, StateType>[ruleTemplateGroup.ruleTemplateList.Length + 1];
            ruleTemplateGroup.ruleTemplateList.CopyTo(ruleTemplateList, 0);
            ruleTemplateList[ruleTemplateList.Length - 1] = ruleTemplate.Clone();
        }



        public RuleTemplateGroup(
            RuleTemplateGroup<InputType, StateType> ruleTemplateGroup1,
            RuleTemplateGroup<InputType, StateType> ruleTemplateGroup2)
        {
            ruleTemplateList =
                new RuleTemplate<InputType, StateType>
                [
                    ruleTemplateGroup1.ruleTemplateList.Length + ruleTemplateGroup2.ruleTemplateList.Length
                ];
            ruleTemplateGroup1.ruleTemplateList.CopyTo(ruleTemplateList, 0);
            ruleTemplateGroup2.ruleTemplateList.CopyTo(ruleTemplateList, ruleTemplateGroup1.ruleTemplateList.Length);
        }


        /// <summary>
        /// RuleTemplateGroupに新しいRuleTemplateを追加した
        /// RuleTemplateGroupを新規に作成
        /// </summary>
        /// <param name="ruleTemplateGroup">追加先のRuleTemplateGroup</param>
        /// <param name="ruleTemplate">追加したいRuleTemplate</param>
        /// <returns></returns>
        public static RuleTemplateGroup<InputType, StateType> operator |(
            RuleTemplateGroup<InputType, StateType> ruleTemplateGroup,
            RuleTemplate<InputType, StateType> ruleTemplate)
        {
            return new RuleTemplateGroup<InputType, StateType>(ruleTemplateGroup, ruleTemplate);
        }

        //
        /// <summary>
        ///二つのRuleTemplateGroupを合体した
        ///RuleTemplateGroupを新規に作成
        /// </summary>
        /// <param name="ruleTemplateGroup1">左辺のRuleTemplateGroup</param>
        /// <param name="ruleTemplateGroup2">右辺のRuleTemplateGroup</param>
        /// <returns></returns>
        public static RuleTemplateGroup<InputType, StateType> operator |(
            RuleTemplateGroup<InputType, StateType> ruleTemplateGroup1,
            RuleTemplateGroup<InputType, StateType> ruleTemplateGroup2)
        {
            return new RuleTemplateGroup<InputType, StateType>(ruleTemplateGroup1, ruleTemplateGroup2);
        }
    }
}

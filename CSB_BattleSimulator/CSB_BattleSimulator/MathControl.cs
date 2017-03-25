using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSB_BattleSimulator
{
    //計算結果をほぞんする用
    struct ResultData
    {
        //術ダメージ
        public int magicDamage;
        //クリティカル確率
        public double criticalPercent;
        //クリティカル加算ダメージ
        public int criticalAddDamage;
        //振れ幅
        public int rangeMax;
        public int rangeMin;
        //ダメージ
        public int maxDamage;
        public int minDamage;
        //クリティカル時ダメージ
        public int criticalMaxDamage;
        public int criticalMinDamage;
    }

    //計算するクラス
    class MathControl
    {
        //攻撃側キャラ
        CharaStates charaS1;
        //攻撃受ける側キャラ
        CharaStates charaS2;
        //コマンドデータ
        CommandData commandD;
        //コンストラクタ
        public MathControl(CharaStates _charaS1, CharaStates _charaS2,CommandData _commandD)
        {
            charaS1 = _charaS1;
            charaS2 = _charaS2;
            commandD = _commandD;
        }


        //計算結果を得る
        public ResultData GetResult()
        {
            switch (commandD.CommandType.GetEnum())
            {
                case CommandType.通常攻撃:
                    return StandardAttack();

                case CommandType.技_属性なし:
                    return WazaAttack();

                case CommandType.技_属性アリ:
                    return WazaElementAttack();

                case CommandType.術:
                    return MagicAttack();

                case CommandType.回復術技:
                    return Restore();

            }
            return new ResultData();
        }


        //通常攻撃計算処理
        private ResultData StandardAttack()
        {
            ResultData rd=new ResultData();
            int damage=(int)(charaS1[State.武力] - charaS2[State.防御] * 10);
            if (damage <= 0) damage = 1;
            rd.criticalPercent = GetCriticalPercent();
            rd.criticalAddDamage = CriticalDamage();
            rd.rangeMax = Range(50);
            rd.rangeMin = Range(1);
            rd.maxDamage = damage-rd.rangeMin;
            if (rd.maxDamage <= 0) rd.maxDamage = 1;
            rd.minDamage = damage- rd.rangeMax;
            if (rd.minDamage <= 0) rd.minDamage = 1;
            rd.criticalMaxDamage = rd.maxDamage + rd.criticalAddDamage;
            rd.criticalMinDamage = rd.minDamage + rd.criticalAddDamage;
            return rd;
        }


        //技（無属性）攻撃計算処理
        private ResultData WazaAttack()
        {
            ResultData rd = new ResultData();
            int damage = (int)(charaS1[State.武力]*commandD.Scale.Value - charaS2[State.防御] * 10);
            if (damage <= 0) damage = 1;
            rd.criticalPercent = GetCriticalPercent();
            rd.criticalAddDamage =CriticalDamage();
            rd.rangeMax = Range(50);
            rd.rangeMin = Range(1);
            rd.maxDamage = damage - rd.rangeMin;
            if (rd.maxDamage <= 0) rd.maxDamage = 1;
            rd.minDamage = damage - rd.rangeMax;
            if (rd.minDamage <= 0) rd.minDamage = 1;
            rd.criticalMaxDamage = rd.maxDamage + rd.criticalAddDamage;
            rd.criticalMinDamage = rd.minDamage + rd.criticalAddDamage;
            return rd;
        }


        //技（属性）攻撃計算処理
        private ResultData WazaElementAttack()
        {
            ResultData rd = new ResultData();
            var damage = (charaS1[State.武力]) + charaS1[State.知性]/ 2.0;
            damage = damage * commandD.Scale.Value-charaS2[State.防御]*10;
            if (damage <= 0) damage = 1;
            rd.criticalPercent = GetCriticalPercent();
            rd.criticalAddDamage =CriticalDamage();
            rd.rangeMax = Range(50);
            rd.rangeMin = Range(1);
            var maxDamageTemp = (int)damage - rd.rangeMin;
            var minDamageTemp = (int)damage - rd.rangeMax;
            if (maxDamageTemp <= 0) maxDamageTemp = 1;
            if (minDamageTemp <= 0) minDamageTemp = 1;
            rd.maxDamage = WazaElementDamage(maxDamageTemp);
            rd.minDamage = WazaElementDamage(minDamageTemp);
            rd.criticalMaxDamage = WazaElementDamage(maxDamageTemp + rd.criticalAddDamage);
            rd.criticalMinDamage = WazaElementDamage(minDamageTemp + rd.criticalAddDamage);
            return rd;
        }


        //術攻撃計算処理
        private ResultData MagicAttack()
        {
            var rd = new ResultData();
            int damage =(int)(charaS1[State.知性] * commandD.Scale.Value - charaS2[State.精神] * 10);
            if (damage <= 0) damage = 1;
            rd.magicDamage = MagicElementDamage(damage);
            return rd;
            
        }

        //回復技計算処理
        private ResultData Restore()
        {
            var rd = new ResultData();
            int damage = (int)(
                (charaS1[commandD.HpUpMath1.GetState()]+
                charaS2[commandD.HpUpMath2.GetState()])*
                commandD.Scale.Value
                );
            rd.magicDamage = damage;
            return rd;
        }


        //クリティカル確率計算処理
        private double GetCriticalPercent()
        {
            var percent = charaS1[State.器用] - charaS2[State.加護];
            if (percent <= 0) percent = 1;
            return (percent*0.1);
        }


        //クリティカルダメージ計算処理
        private int CriticalDamage()
        {
            int damage =(int)(charaS1[State.器用]*2 - charaS2[State.加護]);
            if (damage <= 0) damage = 1;
            return (damage + 500) * 10;
        }


        //振れ幅補正値計算処理
        private int Range(int rand) {
            double range = charaS2[State.回避] - charaS1[State.命中];
            if (range <=0) range = 1;
            range *= 150;
            return (int)(range * rand / 100);
        }


        //技（属性アリ）の時、属性相性によってダメージ量を変化させる
        private int WazaElementDamage(int damage)
        {
            switch (charaS2.Element.GetEnum())
            {
                case ElementType.なし:
                    return damage;

                case ElementType.吸収:
                    return 0;

                case ElementType.弱点:
                    return damage * 2;

                case ElementType.耐性:
                    return (int)(damage / 2.0);

            }
            return -1;
        }


        //術の時、属性相性によってダメージ量を変化させる処理
        private int MagicElementDamage(int damage)
        {
            switch (charaS2.Element.GetEnum())
            {
                case ElementType.なし:
                    return damage;

                case ElementType.吸収:
                    return -damage;

                case ElementType.弱点:
                    return damage * 3;

                case ElementType.耐性:
                    return (int)(damage / 3.0);

            }
            return -1;
        }

    }
}

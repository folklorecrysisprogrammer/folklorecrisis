using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSB_BattleSimulator
{
    //コマンドタイプ種類定義
    public enum CommandType
    {
        通常攻撃=0,
        技_属性なし,
        技_属性アリ,
        術,
        回復術技
    }


    //コマンドタイプをComboBoxから受け取るクラス
    class CommandTypeData : Combo<CommandType>
    {
        public CommandTypeData(ComboBox cb) : base(cb) { }
    }


    //回復コマンド発動側のどのステータスを計算に使用するか識別
    public enum HpUpMath1
    {
        武力= 0,
        器用,
        命中,
        知性
    }


    //回復コマンド発動側のどのステータスを計算に使用するかをComboBoxから受け取るクラス
    class HpUpMath1Data : Combo<HpUpMath1>
    {


        //コンストラクタ
        public HpUpMath1Data(ComboBox cb) : base(cb) { }


        //計算に使用するステータスを返す
        public State GetState()
        {
            return (State)Enum.Parse(typeof(State), Enum.GetName(typeof(HpUpMath1), GetEnum()));
        }
    }


    //回復対象のどのステータスを計算に使用するか識別
    public enum HpUpMath2
    {
        防御=0,
        加護,
        回避,
        精神
    }


    //回復対象のどのステータスを計算に使用するかをComboBoxから受け取るクラス
    class HpUpMath2Data : Combo<HpUpMath2>
    {

        //コンストラクタ
        public HpUpMath2Data(ComboBox cb) : base(cb) { }


        //計算に使用するステータスを返す
        public State GetState()
        {
           return (State)Enum.Parse(typeof(State), Enum.GetName(typeof(HpUpMath2), GetEnum()));
        }

    }

    //コマンドデータ群
    class CommandData
    {
        //コマンドタイプ
        public CommandTypeData CommandType { get;private set; }
        //術技倍率
        public StateData Scale { get; private set; }
        //回復量計算の計算方式データ
        public HpUpMath1Data HpUpMath1 { get; private set; }
        public HpUpMath2Data HpUpMath2 { get; private set; }

        //コンストラクタ
        public CommandData(ComboBox _commandTypeData, TextBox _scale,
            ComboBox _hpUpMath1Data, ComboBox _hpUpMath2Data)
        {
            //各種クラスを作成
            CommandType = new CommandTypeData(_commandTypeData);
            Scale = new StateData(_scale);
            HpUpMath1 = new HpUpMath1Data(_hpUpMath1Data);
            HpUpMath2 = new HpUpMath2Data(_hpUpMath2Data);
        }
    }
}

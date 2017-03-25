using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSB_BattleSimulator
{
    //textBoxから値を受け取るクラス
    class StateData
    {
        private TextBox tb;

        //TextBoxの値をdoubleで返す
        public double Value { get {
                try
                {
                    return double.Parse(tb.Text);
                }
                //doubleに変換失敗したら、"0"にする
                catch
                {
                    tb.Text = "0";
                    return 0;
                }
            } }

        //コンストラクタ
        public StateData(TextBox _tb)
        {
            tb = _tb;
        }
    }

    //comboボックスの値を任意のEnumに変換して返すクラス
    class Combo<EnumType>
        where EnumType:struct
    {
        //値を任意のEnumで得る
        public EnumType GetEnum() { return (EnumType)(object)cb.SelectedIndex;}

        private ComboBox cb;

        //コンストラクタ
        public Combo(ComboBox _cb)
        {
            cb = _cb;
        }
    }
}

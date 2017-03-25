using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSB_BattleSimulator
{

    //属性相性識種類定義
    enum ElementType
    {
        なし = 0, 弱点, 耐性, 吸収
    }


    //属性相性をComboBoxから受け取るクラス
    class ElementData : Combo<ElementType>
    {
        public ElementData(ComboBox _cb) : base(_cb) { }
    }


    //ステータス種類定義
    enum State
    {
        武力 = 0, 防御, 知性,
        精神, 命中, 回避,
        器用, 加護
    }


    //キャラが持つステータス群
    class CharaStates
    {

        //ステータス
        //（武力, 防御, 知力,精神, 命中, 回避,器用, 加護）
        private StateData[] states = new StateData[8];


        //ステータスの任意の要素を返す
        public double this[State sk]
        {
            get { return states[(int)sk].Value; }
        }


        //属性相性
        public ElementData Element { get; private set; }

        //コンストラクタ
        //ステータスと属性相性の値をtextBox,comboBoxから受け取る
        public CharaStates(TextBox[] textBoxes, ComboBox comboBox)
        {
            for (int i = 0; i < 8; i++)
            {
                states[i] = new StateData(textBoxes[i]);
            }
            Element = new ElementData(comboBox);
        }

        public CharaStates(TextBox[] textBoxes)
        {
            for (int i = 0; i < 8; i++)
            {
                states[i] = new StateData(textBoxes[i]);
            }
        }

    }
}

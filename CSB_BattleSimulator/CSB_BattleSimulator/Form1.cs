using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSB_BattleSimulator
{
    public partial class Form1 : Form
    {
        //攻撃側キャラステータス
        private CharaStates charaStates;
        //受ける側キャラステータス
        private CharaStates charaStates2;
        //コマンドデータ
        private CommandData commandData;
        //計算するクラス
        private MathControl mathControl;


        //初期化
        public Form1()
        {
            InitializeComponent();

            //各comboBox初期値設定
            element.SelectedIndex = 0;
            commandType.SelectedIndex = 0;
            hpUpMath1.SelectedIndex = 0;
            hpUpMath2.SelectedIndex = 0;

            //comboBox,textBoxの参照を渡して、値を得られるようにする
            var textBoxes = new TextBox[]{
                state1,state2,state3,state4,state5,state6,
                state7,state8 };
            var textBoxes2 = new TextBox[]{
                _state1,_state2,_state3,_state4,_state5,_state6,
                _state7,_state8 };
            charaStates = new CharaStates(textBoxes);
            charaStates2 = new CharaStates(textBoxes2, element);
            commandData = new CommandData(commandType, scale, hpUpMath1, hpUpMath2);
            mathControl = new MathControl(charaStates, charaStates2, commandData);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        //「計算する」ボタンが押された時に呼ばれる処理
        private void mathButton_Click(object sender, EventArgs e)
        {
            //計算を行う
            ResultData rd = mathControl.GetResult();

            //計算結果をTextBoxに出力---------------------------------------
            //クリティカル率
            criticalPercentBox.Text = rd.criticalPercent.ToString() + "%";
            //クリティカル加算ダメージ
            criticalAddDamageBox.Text = rd.criticalAddDamage.ToString();
            //振れ幅
            rangeMaxBox.Text = rd.rangeMax.ToString();
            rangeMinBox.Text = rd.rangeMin.ToString();
            //振れ幅考慮ダメージ
            rangeMaxDamageBox.Text = rd.maxDamage.ToString();
            rangeMinDamageBox.Text = rd.minDamage.ToString();
            //振れ幅考慮クリティカルダメージ
            rangeCriticalMaxDamageBox.Text = rd.criticalMaxDamage.ToString();
            rangeCriticalMinDamageBox.Text = rd.criticalMinDamage.ToString();
            //術＆回復のダメージ
            magicDamageBox.Text = rd.magicDamage.ToString();
        }
    }
}

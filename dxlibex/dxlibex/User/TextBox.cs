using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using DXEX.Base;

namespace DXEX.User
{
    class TextBox : Node
    {
        //文字のカラー
        private uint color = DX.GetColor(50, 50, 50);
        //カーソルカラー
        private uint curColor = DX.GetColor(255, 100, 255);
        //IME使用時カーソルカラー
        private uint imeCurColor = DX.GetColor(0, 255, 255);
        //IME使用時の変換文字列の下線
        private uint imeLineColor = DX.GetColor(0, 155, 0);
        //IME使用時の選択対象の変換候補文字列の色
        private uint imeSelectColor = DX.GetColor(200, 100, 100);
        //IME使用時の入力モード文字列の色(『全角ひらがな』等)
        private uint imeModeColor = DX.GetColor(50, 255, 50);
        //入力文字列の縁の色
        private uint edgeColor = DX.GetColor(255, 255, 200);
        //IME使用時の選択対象の変換候補文字列の縁の色
        private uint imeSelectEdgeColor = DX.GetColor(20, 20, 200);
        //IME使用時の入力モード文字列の縁の色
        private uint imeModeEdgeColor = DX.GetColor(100, 100, 100);
        //IME使用時の変換候補ウインドウの縁の色
        private uint imeSelectWinEdgeColor = DX.GetColor(100, 100, 100);
        //IME使用時の変換候補ウインドウの下地の色
        private uint imeSelectWinFColor = DX.GetColor(100, 100, 100);
        //入力文字列の選択部分( SHIFTキーを押しながら左右キーで選択 )の周りの色
        private uint selectBackColor = DX.GetColor(0, 50, 150);
        //入力文字列の選択部分( SHIFTキーを押しながら左右キーで選択 )の色
        private uint selectColor = DX.GetColor(200, 200, 0);
        //入力文字列の選択部分( SHIFTキーを押しながら左右キーで選択 )の縁の色
        private uint selectEdgeColor = DX.GetColor(100, 100, 100);
        //IME使用時の入力文字列の色
        private uint imeColor = DX.GetColor(200, 20, 200);

        //文字列の縦幅横幅
        private Vect rect;
        //描画幅
        public double DrawWidth;
        public const double DrawHeight = 16;
        public uint Color
        {
            get { return color; }
            set { color = value; }
        }

        //キー入力用ハンドル
        private int keyHandle = DX.MakeKeyInput(10000, DX.FALSE, DX.FALSE, DX.FALSE);
        //キー入力中か判別するフラグ
        private bool inputFlag = false;
        //表示する文字
        private string text="";
        private StringBuilder strb = new StringBuilder(10000);
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                rect.x = DX.GetDrawStringWidth(text, text.Length);
            }
        }

        //キー入力開始
        public void StartInput()
        {
            if (inputFlag) return;
            inputFlag = true;
            DX.SetActiveKeyInput(keyHandle);

        }

        //コンストラクタ
        public TextBox(string _text, uint _color)
        {
            color = _color;
            Text = _text;
            SetInputColor();
        }
        public TextBox(string _text)
        {
            Text = _text;
            SetInputColor();
        }
        public TextBox()
        {
            SetInputColor();
        }
        private void SetInputColor()
        {
            DX.SetKeyInputStringColor(
                color, curColor, imeColor, imeCurColor, imeLineColor, imeSelectColor,
                imeModeColor, edgeColor, imeSelectEdgeColor, imeModeEdgeColor,
                imeSelectWinEdgeColor, imeSelectWinFColor, selectBackColor, selectColor,
                selectEdgeColor);
        }

        //描画処理
        public sealed override void Draw()
        {

            DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, GlobalOpacity);
            //左上の座標
            Vect leftup = new Vect();
            leftup.SetVect(GlobalPos.x - DrawWidth * anchor.x, GlobalPos.y - DrawHeight * anchor.y);
            DX.SetDrawArea((int)leftup.x, (int)leftup.y, (int)(leftup.x + DrawWidth), (int)(leftup.y + DrawHeight));
            if (inputFlag == false)
            {
                DX.DrawRotaString((int)(GlobalPos.x), (int)(GlobalPos.y),
                                  scale.x, scale.y, anchor.x * DrawWidth, anchor.y * DrawHeight,
                                  0, color, color, DX.FALSE, text);
            }
            else
            {
                if (DX.CheckKeyInput(keyHandle) == 1 || DX.GetActiveKeyInput()!=keyHandle)
                {
                    inputFlag = false;
                    DX.GetKeyInputString(strb, keyHandle);
                    Text = strb.ToString();
                }
                else
                {


                    DX.DrawKeyInputString((int)leftup.x, (int)leftup.y, keyHandle);

                }

            }
            DX.SetDrawAreaFull();
        }
    }
}

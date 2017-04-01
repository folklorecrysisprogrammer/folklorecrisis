using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace DXEX
{
    //画面に文字を表示するオブジェクト
   public class Letter:Colision
    {
        //文字のカラー
        private uint color=DX.GetColor(255,255,255);
        //文字列の縦幅横幅
        private Size rect;
        public uint Color
        {
            get { return color; }
            set { color = value; }
        }

        //表示する文字
        private string text;
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                rect.Width = DX.GetDrawStringWidth(text, text.Length);
            }
        }

        //コンストラクタ
        public Letter(string _text,uint _color)
        {
            color = _color;
            Text=_text;
        }
        public Letter(string _text) {
            Text=_text;
        }
        public Letter() {}

        //描画処理
        public sealed override void Draw()
        {
            
            DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, GlobalOpacity);
            DX.DrawRotaString((int)(GlobalPos.x), (int)(GlobalPos.y),
                              scale.x, scale.y, anchor.x*rect.Width, anchor.y * 16,
                              Utility.DegToRad(GlobalAngle), color,color, DX.FALSE,text);
            base.Draw();
        }
    }
}

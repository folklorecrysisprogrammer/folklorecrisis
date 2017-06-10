using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEdit
{
    public class MapChipConfig: DXEX.Node
    {
        public static int isPassEditMode = DxLibDLL.DX.FALSE;

        public bool IsEnablePass { get; set; }
        DXEX.Sprite chipStateSprite;

        /* マップチップの情報を初期化する */
        public MapChipConfig()
        {
            IsEnablePass = true;

            chipStateSprite = new DXEX.Sprite();
            AddChild(chipStateSprite);

            SetChipStateSprite();
        }

        public void ChangeIsEnablePass(bool value)
        {
            IsEnablePass = value;
            SetChipStateSprite();
        }


        private void SetChipStateSprite()
        {
            // 情報をもとに表示する内容を決定する
            string cp = System.IO.Directory.GetCurrentDirectory();
            if (IsEnablePass)
                cp += "\\Image\\ChipInfo1.png";
            else
                cp += "\\Image\\ChipInfo2.png";
            DXEX.Sprite temp = new DXEX.Sprite(cp);

            // テクスチャを一時スプライトからコピー
            chipStateSprite.SetTexture( temp.GetTexture() );

            // テクスチャは明示的に破棄する
            temp.ClearTexture();
        }
    }
}

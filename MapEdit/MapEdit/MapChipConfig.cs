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
            chipStateSprite.anchor.SetVect(0.5, 0.5);
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
            chipStateSprite.SetTexture(cp);
        }
    }
}

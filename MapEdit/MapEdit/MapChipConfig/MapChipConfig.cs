using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEdit.MapChipConfig
{
    // 1マスあたりのコンフィグ情報(通行可能など)を設定
    public class MapChipConfig
    {
        // ToDo: そもそも通行判定を設定中かとか持つべきじゃない

        public bool EnablePass { get; set; }
        // DXEX.Sprite chipStateSprite;

        /* マップチップの情報を初期化する */
        public MapChipConfig()
        {
            EnablePass = true;

            /*
            chipStateSprite = new DXEX.Sprite();
            chipStateSprite.anchor.SetVect(0.5, 0.5);
            AddChild(chipStateSprite);

            SetChipStateSprite();
            */
        }

        public void ChangeIsEnablePass(bool value)
        {
            EnablePass = value;
        }
        public void ChangeIsEnablePass()
        {
            EnablePass = !EnablePass;
        }

        private void SetChipStateSprite()
        {
            // 情報をもとに表示する内容を決定する
            string cp = System.IO.Directory.GetCurrentDirectory();
            if (EnablePass)
                cp += "\\Image\\ChipInfo1.png";
            else
                cp += "\\Image\\ChipInfo2.png";
            // chipStateSprite.SetTexture(cp);
        }
    }
}

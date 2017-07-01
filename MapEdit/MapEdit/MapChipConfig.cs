using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEdit
{
    public class MapChipConfig
    {
        //エディットモードの種類
        public enum PassEditModeKind
        {
            通行判定,
            編集中
        }
        //エディットモードを保持する変数
        public static PassEditModeKind passEditMode = PassEditModeKind.通行判定;

        //エディットモード変更
        public static void ChangePassEditMode()
        {
            if (passEditMode == PassEditModeKind.通行判定)
                passEditMode = PassEditModeKind.編集中;
            else 
                passEditMode = PassEditModeKind.通行判定;
        }

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

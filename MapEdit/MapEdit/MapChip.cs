using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace MapEdit
{
    //マップチップを表示、マップチップデータを保持するクラス
    public class MapChip:DXEX.Sprite
    {

        public int MapChipSize {
            get { return mapChipSize;}
            set { mapChipSize = value;ScaleFix();}
        }
        private int mapChipSize;

        public int Id { get; set; }

        public MapChipConfig mcc;

        /* サイズを指定しIDを初期化するコンストラクタ */
        public MapChip(int mapChipSize)
        {
            MapChipSize = mapChipSize;
            Id = -1;
            mcc = new MapChipConfig();
            AddChild(mcc);
        }

        protected override void UpdateAngle()
        {
            angle = (int)angle % 360;
            switch ((int)angle / 90)
            {
                case 0:
                    offsetPos.SetVect(0, 0);
                    break;
                case 1:
                    offsetPos.SetVect(mapChipSize, 0);
                    break;
                case 2:
                    offsetPos.SetVect(mapChipSize, mapChipSize);
                    break;
                case 3:
                    offsetPos.SetVect(0, mapChipSize);
                    break;
            }
        }

        protected override void UpdateTexture()
        {
            ScaleFix();
        }

        private void ScaleFix()
        {
            scale.SetVect(
                mapChipSize / Rect.Width,
                mapChipSize / Rect.Height
            );
        }

        //マップチップを上下反転
        public void TurnVertical()
        {
            if (Angle == 0 || Angle == 180)
            {
                Angle += 180;
            }
            SwitchTurnFlag();
        }

        //マップチップを左右反転
        public void TurnHorizontal()
        {
            if (Angle == 90 || Angle == 270)
            {
                Angle += 180;
            }
            SwitchTurnFlag();
        }

        public void SwitchTurnFlag()
        {
            if (turnFlag == DX.TRUE)
            {
                turnFlag = DX.FALSE;
            }
            else
            {
                turnFlag = DX.TRUE;
            }
        }
    }
}


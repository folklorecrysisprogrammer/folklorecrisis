using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DxLibDLL;

using System.Drawing;
namespace MapEdit
{
    public class SelectImage:DXEX.Node
    {
        public PictureBox SelectPicture { get;private set; }

        public string FilePath { get; set; }

        public SelectImage(PictureBox selectPicture)
        {
            this.SelectPicture = selectPicture;
             FilePath= "";
        }
        //選択画像の回転、反転、
        private void Rotate(RotateFlipType rft)
        {
            var image = (Image)SelectPicture.Image.Clone();
            image.RotateFlip(rft);
            SelectPicture.Image = image;
            Angle = (int)Angle % 360;
        }

        public void Reset()
        {
            Angle = 0;
            turnFlag = DX.FALSE;
        }

        public void RotateLeft()
        {
            Angle += 270;
            Rotate(RotateFlipType.Rotate270FlipNone);
        }

        public void RotateRight()
        {
            Angle += 90;
            Rotate(RotateFlipType.Rotate90FlipNone);
        }

        public void TurnHorizontal()
        {
            if (Angle == 90 || Angle == 270)
            {
                Angle += 180;
            }
            if (turnFlag == DX.TRUE)
            {
                turnFlag = DX.FALSE;
            }else
            {
                turnFlag = DX.TRUE;
            }
            Rotate(RotateFlipType.RotateNoneFlipX);
        }

        public void turnVertical()
        {
            if (Angle == 0 || Angle == 180)
            {
                Angle += 180;
            }
            if (turnFlag == DX.TRUE)
            {
                turnFlag = DX.FALSE;
            }
            else
            {
                turnFlag = DX.TRUE;
            }
            Rotate(RotateFlipType.RotateNoneFlipY);
        }
    }
}

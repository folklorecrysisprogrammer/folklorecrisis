using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MapEdit
{
    //マップパレットにて、マウス操作で
    //マップチップを入れ替えるクラス
    class MouseSwap
    {
        private Point tempPoint;
        private Point tempPoint2;
        //入れ替え対象のマス座標を指定
        public void Start(Point point)
        {
            tempPoint = point;
            tempPoint2 = point;
        }

        //Startで登録したマス座標にあるmapChipを任意のマス座標にあるmapChipと入れ替え
        public void Move(Point point, MapPalletData mapPalletData,MapChipResourceManager mcrm)
        {
            if (tempPoint2 == point) return;
            if (tempPoint == point)
            {
                mapPalletData.SwapMapChip(tempPoint2.X, tempPoint2.Y, tempPoint.X, tempPoint.Y, mcrm);
                tempPoint2 = point;
                return;
            }
            mapPalletData.SwapMapChip(point.X, point.Y, tempPoint2.X, tempPoint2.Y, mcrm);
            mapPalletData.SwapMapChip(tempPoint2.X, tempPoint2.Y, tempPoint.X, tempPoint.Y, mcrm);
            tempPoint2 = point;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEdit
{
    //マップパレットに表示する画像を管理するクラス
    class MapPalletData
    {
        private MapChip[,] mapChips;
        private int index = 0;

        //配列ぽく振るまう
        public MapChip this[int x, int y]
        {
            get { return mapChips[x, y]; }
        }

        public MapPalletData()
        {
            mapChips = new MapChip[6, 50];
        }

        public void AddMapChip(MapChip mapChip)
        {
            int x = index % 6;
            int y = index / 6;
            mapChip.anchor.SetVect(0, 0);
            mapChip.LocalPos = new DXEX.Vect(x*40,y*40);
            mapChips[x,y]=mapChip;
            index++;
        }

    }
}

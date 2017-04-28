using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEdit
{
    //マップパレットに表示する画像を管理するクラス
    public class MapPalletData
    {
        private MapChip[,] mapChips;
        private int index = 0;

        //配列ぽく振るまう
        public MapChip this[int x, int y]
        {
            get { return mapChips[x, y]; }
        }

        //初期化
        public MapPalletData()
        {
            mapChips = new MapChip[6, 50];
        }

        //マップpalletに新しいマップチップを登録する
        public void AddMapChip(MapChip mapChip)
        {
            int x = index % 6;
            int y = index / 6;
            mapChip.anchor.SetVect(0, 0);
            mapChip.LocalPos = new DXEX.Vect(x*40,y*40);
            mapChips[x,y]=mapChip;
            index++;
        }

        //マップpalletに新しいマップチップを登録する
        public void RemoveMapChip(int x,int y)
        {
            int lastx = (index-1) % 6;
            int lasty = (index-1) / 6;
            if(x!=lastx || y != lasty)
            {
                mapChips[lastx, lasty].LocalPos = mapChips[x, y].LocalPos;
                mapChips[lastx, lasty].Id = mapChips[x, y].Id;
                mapChips[x, y] = mapChips[lastx, lasty];
            }
            mapChips[lastx, lasty] = null;
            index--;
        }

        public void ClearMapChip()
        {
            if (index == 0) return;
            foreach (var item in mapChips)
            {
                if (item != null)
                {
                    item.Dispose();
                }

            }
            index = 0;
            mapChips = new MapChip[6, 50];

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DXEX;

namespace MapEdit
{
    //マップパレットに表示する画像を管理するクラス
    public class MapPalletData
    {
        private MapChip[,] mapChips;
        private int index = 0;

        public bool ExsitMapChip(int x,int y)
        {
            return mapChips[x, y] == null ? false : true;
        }

        //mccのEnablePassFlagを反転する
        public void ReverseEnablePassFlag(int x,int y)
        {
            bool temp = mapChips[x, y].mcc.IsEnablePass;
            mapChips[x, y].mcc.ChangeIsEnablePass(!temp);
        }

        public Id GetId(int x,int y)
        {
            return mapChips[x, y].Id;
        }

        public Texture GetTexture(int x,int y)
        {
            return mapChips[x, y].GetTexture();
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

        //指定座標のマップチップを入れ替える
        public void SwapMapChip(int x1,int y1,int x2,int y2,MapChipResourceManager mcrm)
        {
            mcrm.SwapImageFile(mapChips[x1, y1].Id.value, mapChips[x2, y2].Id.value);
            var temp = mapChips[x1, y1].LocalPos;
            mapChips[x1, y1].LocalPos = mapChips[x2, y2].LocalPos;
            mapChips[x2, y2].LocalPos = temp;
            var tempChips=mapChips[x1, y1];
            mapChips[x1, y1] = mapChips[x2, y2];
            mapChips[x2, y2] = tempChips;
        }

        //マップpalletにからマップチップを削除する
        public void RemoveMapChip(int x,int y,MapChipResourceManager mcrm)
        {
            mcrm.PopImageFile(mapChips[x,y].Id.value);
            mapChips[x, y].Dispose();
            int lastx = (index-1) % 6;
            int lasty = (index-1) / 6;
            if(x!=lastx || y != lasty)
            {
                mapChips[lastx, lasty].LocalPos = mapChips[x, y].LocalPos;
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

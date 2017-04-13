using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MapEdit
{
    //マップチップ画像をBMPとして保持するクラス
    public class MapChipResourceManager
    {
        private List<Bitmap> bitmapList=new List<Bitmap>();
        private readonly int mapChipSize;

        public MapChipResourceManager(int mapChipSize)
        {
            this.mapChipSize = mapChipSize;
        }

        public int pushImageFile(string fileName)
        {
            var bitmap = new Bitmap(mapChipSize, mapChipSize);
            Graphics g = Graphics.FromImage(bitmap);
            //画像を扱うアルゴリズムを設定する
            g.PixelOffsetMode = PixelOffsetMode.Half;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.DrawImage((Bitmap)Bitmap.FromFile(fileName), 0, 0, mapChipSize, mapChipSize);
            bitmapList.Add(bitmap);
            return bitmapList.Count - 1;
        }

        public Bitmap GetBitmap(int id)
        {
            if (id == -1) return null;
            return (Bitmap)bitmapList[id].Clone();
        }

        /*マップチップを一枚にまとめて返す
         * IDの配置↓
         *   0, 1, 2, 3, 4, 5
         *   6, 7, 8, 9,10,11
         *  12,13,14,15,16,17
         *  
        */
        public Bitmap GetBitmapSheet()
        {
            if (bitmapList.Count == 0) return null;
            int ySize = bitmapList.Count / 6;
            int nowId = 0;
            if (bitmapList.Count % 6 != 0) ySize++;
            Bitmap resultBitmap = new Bitmap(6 * mapChipSize, ySize*mapChipSize);
            Graphics g = Graphics.FromImage(resultBitmap);
            for(int y=0; y < ySize; y++)
            {
                for(int x = 0; x < 6; x++)
                {
                    g.DrawImage(bitmapList[nowId], x * mapChipSize, y * mapChipSize);
                    nowId++;
                    if (nowId == bitmapList.Count) return resultBitmap;
                }
            }
            return null;
        }

        //一番最後のIdを返す
        public int LastId()
        {
            return bitmapList.Count - 1;
        }
    }
}

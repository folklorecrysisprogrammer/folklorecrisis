using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MapEdit
{
    //マップチップ画像をBMPとして保持するクラス
    public static class MapChipResourceManager
    {
        static private List<Bitmap> bitmapList=new List<Bitmap>();

        static public int pushImageFile(string fileName)
        {
            bitmapList.Add((Bitmap)Bitmap.FromFile(fileName));
            return bitmapList.Count - 1;
        }

        static public Bitmap GetBitmap(int id)
        {
            if (id == -1) return null;
            return (Bitmap)bitmapList[id].Clone();
        }
    }
}

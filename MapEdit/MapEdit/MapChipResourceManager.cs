using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace MapEdit
{
    //マップチップ画像をBMP&Textureとして保持し、IDで紐づけして管理するクラス
    public class MapChipResourceManager
    {
        private List<Bitmap> bitmapList=new List<Bitmap>();
        private List<DXEX.Texture> textureList = new List<DXEX.Texture>();
        private readonly int mapChipSize;

        public MapChipResourceManager(int mapChipSize)
        {
            this.mapChipSize = mapChipSize;
        }

        //新しいマップチップの画像を登録する
        //返り値はId
        public int PushImageFile(string fileName)
        {
            //Bitmapを用意
            var bitmap = new Bitmap(mapChipSize, mapChipSize);
            Graphics g = Graphics.FromImage(bitmap);
            //画像を扱うアルゴリズムを設定する
            g.PixelOffsetMode = PixelOffsetMode.Half;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            //MapChipSizeの大きさにリサイズしてリストに登録
            g.DrawImage((Bitmap)Bitmap.FromFile(fileName), 0, 0, mapChipSize, mapChipSize);
            bitmapList.Add(bitmap);
            //Textureをリストに登録登録
            textureList.Add(DXEX.TextureCache.GetTexture(fileName));
            return bitmapList.Count - 1;
        }

        //idから画像リソースを破棄
        public void PopImageFile(int id)
        {
            textureList[id].Dispose();
            int end = bitmapList.Count - 1;
            if (bitmapList.Count != id+1 && bitmapList.Count > 1)
            {
                bitmapList[id] = bitmapList[end];
                textureList[id] = textureList[end];
            }
            bitmapList.RemoveAt(end);
            textureList.RemoveAt(end);
        }

        public void SwapImageFile(int id1,int id2)
        {
            var tempb = bitmapList[id1];
            var tempt = textureList[id1];
            bitmapList[id1] = bitmapList[id2];
            textureList[id1] = textureList[id2];
            bitmapList[id2] = tempb;
            textureList[id2] = tempt;
        }

        //IDから画像リソースを得る
        public Bitmap GetBitmap(int id)
        {
            return (Bitmap)bitmapList[id].Clone();
        }
        public DXEX.Texture GetTexture(int id)
        {
            return textureList[id];
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

        public MapChipResourceManager LoadProject(MapInfoFromText mift,string filePath)
        {
            var newMcrm = new MapChipResourceManager(mift.MapChipSize);
            newMcrm.LoadBitmapSheet(mift.LastId, filePath);
            return newMcrm;
        } 

        //ビットマップを読み込み
        //マップチップ画像データを
        private void LoadBitmapSheet(int lastId,string filePath)
        {
            var bitmap = (Bitmap)Bitmap.FromFile(filePath);
            bitmapList.Clear();
            textureList.Clear();
            int yCount= bitmap.Height / mapChipSize;
            int allNum = 6 * yCount;
            DXEX.Texture[] textures =
            DXEX.TextureCache.GetTextureAtlas(filePath, allNum, 6, yCount, mapChipSize, mapChipSize);
            for (int id = 0; id <= lastId; id++)
            {
                textureList.Add(textures[id]);
            }
            for (int y = 0; y < yCount; y++)
            {
                for (int x = 0; x < 6; x++)
                {
                    Bitmap resultBitmap = bitmap.Clone(
                        new Rectangle
                        (
                            x * mapChipSize, y * mapChipSize,
                            mapChipSize, mapChipSize
                        )
                        ,bitmap.PixelFormat
                    );
                    bitmapList.Add(resultBitmap);
                    if (bitmapList.Count - 1 == lastId)
                        return;
                }
            }
            

        }

        //最後のID
        public int LastID()
        {
            return bitmapList.Count - 1;
        }


    }
}

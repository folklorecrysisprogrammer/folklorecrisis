using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace DXEX.Base
{
    public class TextureAtlasCache:ResourceCache<TextureAtlasCore,int[]>
    {

        //画像を分割してTextureAtlasを返す
        //(filePath,分割した数,Xの分割数,Yの分割数,分割した画像のXsize,分割した画像のYsize)
        public TextureAtlas GetTextureAtlas(string filePath ,int AllNum,int XNum, int YNum,
                                              int XSize, int YSize)
        {
            int[] gh = new int[AllNum];
            int flag=DX.LoadDivGraph(filePath, AllNum, XNum, YNum,XSize,YSize, out gh[0]);
            if (flag == -1)
            {
                throw new Exception("画像の読み込みに失敗しました");
            }
            var newCore=new TextureAtlasCore(gh);
            AddNewResourceCore(filePath, newCore);
            return new TextureAtlas(newCore);
        }

        //読み込み済のTextureAtlasを返す
        public TextureAtlas GetTextureAtlas(string filePath)
        {
            return new TextureAtlas(GetResourceCore(filePath));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace DXEX.Base
{
    //グラフィックハンドルを持ち、それの参照数を管理するクラス
    public class TextureAtlasCore : ResourceCore<int[]>
    {
        //コンストラクタ
        internal TextureAtlasCore(int[] ghs) : base(ghs)
        {
        }
        //画像リソース開放
        internal override void ResourceFree()
        {
            foreach (var it in resourceData)
            {
                DX.DeleteGraph(it);
            }
        }

    }

    public class TextureAtlas : ResourceProvider<TextureAtlasCore, int[], TextureAtlas>
    {

        internal TextureAtlas(TextureAtlasCore textureAtlasCore)
            : base(textureAtlasCore){}

        //複製
        public override TextureAtlas Clone()
        {
            return new TextureAtlas(resourceCore);
        }

        //グラフィックハンドルarrayを返す
        public int[] Gh
        {
            get { return (int[])resourceCore.resourceData.Clone(); }
        }
        //i番目のグラフィックハンドルを返す
        public int GetGh(int index)
        {
            return resourceCore.resourceData[index];
        }
        public int this[int index]
        {
            get { return resourceCore.resourceData[index]; }
        }
        //i番目のGhをITextureにラップして返す
        public ITexture GetITexture(int index)
        {
            return new _TextureAtlas(this,index);
        }
        //Ghの配列をITextureにラップした配列で返す
        public ITexture[] GetITextureList()
        {
            ITexture[] iTextureList = new ITexture[Gh.Length];
            for (int i = 0; i < Gh.Length; i++)
            {
                iTextureList[i]= new _TextureAtlas(this, i);
            }
            return iTextureList;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXEX.Base
{
    //画像のハンドルを一つ返すインターフェイス
    //これは,TextureとTextureAtlasの持つハンドルを
    //同じインターフェイスで扱えることを目的としたものです。
    public interface ITexture : IDisposable
    {
        ITexture Clone();
        int Gh { get; }
    }

    //TextureクラスをITextureにラップして扱える
   public class _Texture : ITexture
    {
        public void Dispose()
        {
            Dispose(false);
        }
        private void Dispose(bool isFinalize)
        {
            texture = null;
            //デストラクタを呼ばないようにする
            if (!isFinalize) GC.SuppressFinalize(this);
        }

        //デストラクタ
        ~_Texture()
        {
            Dispose(true);
        }
        private Texture texture;
        public _Texture(Texture texture)
        {
            this.texture = texture;
        }
        public ITexture Clone()
        {
            return new _Texture(texture.Clone());
        }
        public int Gh { get { return texture.Gh; }}
    }

    //TextureAtlasクラスをITextureにラップして扱える
    public class _TextureAtlas : ITexture
    {
        public void Dispose()
        {
            Dispose(false);
        }
        private void Dispose(bool isFinalize)
        {
            textureAtlas = null;
            //デストラクタを呼ばないようにする
            if (!isFinalize) GC.SuppressFinalize(this);
        }

        //デストラクタ
        ~_TextureAtlas()
        {
            Dispose(true);
        }
        readonly int index;
        private TextureAtlas textureAtlas;
        //引数（TextureAtlas,TextureAtlasのindex番目を指定）
        public _TextureAtlas(TextureAtlas textureAtlas,int index)
        {
            this.index = index;
            this.textureAtlas = textureAtlas;
        }
        public ITexture Clone()
        {
            return new _TextureAtlas(textureAtlas.Clone(),index);
        }
        public int Gh { get { return textureAtlas[index]; } }
    }
}

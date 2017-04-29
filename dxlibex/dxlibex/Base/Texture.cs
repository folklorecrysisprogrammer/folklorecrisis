using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace DXEX.Base
{
    //グラフィックハンドルを持ち、それの参照数を管理するクラス
   public class TextureCore:IDisposable
    {
        //グラフィックハンドル
        public int Gh { get; private set; }
        //参照カウンタ
        private int refCount=0;
        //誰にも参照されてないならtrueを返す
        public bool NotUsing() { return refCount == 0; }
        //参照を1増やす
        public void Retain()
        {
            refCount++;
        }
        //参照を1減らす
        public void Release()
        {
            if (refCount == 0) return;
            refCount--;
        }
        //コンストラクタ
        public TextureCore(int _gh)
        {
            Gh = _gh;
        }

        //画像リソース開放
        public void Dispose()
        {
            DX.DeleteGraph(Gh);
        }

    }

    //TextureCoreを保存するクラス
   public class Texture:IDisposable
    {
        private TextureCore textureCore=null;

        //TextureCoreの参照数を1増やす
        public Texture(TextureCore _textureCore)
        {
            textureCore = _textureCore;
            textureCore.Retain();
        }

        //複製
        public Texture Clone()
        {
            return new Texture(textureCore);
        }

        //グラフィックハンドルを返す
        public int Gh{ get { return textureCore.Gh; } }
        //TextureCoreの参照数を1減らす
        public void Dispose()
        {
            Dispose(false);

        }

        private void Dispose(bool isFinalize)
        {
            DebugMessage.Mes(
                "Texture.Dispose():すでにDisposeされています",
                textureCore == null
            );
            textureCore.Release();
            textureCore = null;
            //デストラクタを呼ばないようにする
            if (!isFinalize) GC.SuppressFinalize(this);
        }

        //デストラクタ
        ~Texture()
        {
            Dispose(true);
        }
    }
}

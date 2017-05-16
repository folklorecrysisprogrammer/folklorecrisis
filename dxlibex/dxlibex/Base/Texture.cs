using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace DXEX.Base
{
    //グラフィックハンドルを持ち、それの参照数を管理するクラス
    public class TextureCore:ResourceCore<int>
    {
        //コンストラクタ
        internal TextureCore(int gh):base(gh)
        {
        }
        //画像リソース開放
        internal override void ResourceFree()
        {
            DX.DeleteGraph(resourceData);
        }

    }

    //TextureCoreを保存するクラス
   public class Texture:ResourceProvider<TextureCore,int,Texture>
    {

        //TextureCoreの参照数を1増やす
       internal Texture(TextureCore textureCore):base(textureCore)
        {
        }

        //複製
        public override Texture Clone()
        {
            return new Texture(resourceCore);
        }

        //グラフィックハンドルを返す
        public int Gh{ get { return resourceCore.resourceData; } }
    }
}

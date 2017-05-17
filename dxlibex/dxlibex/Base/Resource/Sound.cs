using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace DXEX.Base
{
    //サウンドハンドルを持ち、それの参照数を管理するクラス
    public class SoundCore : ResourceCore<int>
    {
        //コンストラクタ
        internal SoundCore(int gh) : base(gh)
        {
        }
        //サウンドリソース開放
        internal override void ResourceFree()
        {
            DX.DeleteSoundMem(resourceData);
        }

    }

    //SoundCoreを保存するクラス
    public class Sound : ResourceProvider<SoundCore, int, Sound>
    {

        //TextureCoreの参照数を1増やす
        internal Sound(SoundCore soundCore) : base(soundCore)
        {
        }

        //複製
        public override Sound Clone()
        {
            return new Sound(resourceCore);
        }

        //サウンドハンドルを返す
        public int Gh { get { return resourceCore.resourceData; } }
    }
}

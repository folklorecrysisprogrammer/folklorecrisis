using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace DXEX.Base
{
    //サウンドのメモリ管理クラス

    public class SoundCache : ResourceCache<SoundCore, int>
    {
        //Soundを返す
        public Sound GetSound(string filePath)
        {
            //キャッシュされていたらそれを使う
            if (isKey(filePath) == true)
            {
                return new Sound(GetResourceCore(filePath));
            }
            int gh = DX.LoadSoundMem(filePath);
            if (gh == -1)
            {
                throw new Exception("音声の読み込みに失敗しました");
            }
            var newCore = new SoundCore(gh);
            AddNewResourceCore(filePath, newCore);
            return new Sound(newCore);
        }
    }
}

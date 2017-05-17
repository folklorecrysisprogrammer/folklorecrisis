using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace DXEX.Base
{
    /*画像のメモリ管理クラス
     * 
     * 画像を読み込むときに、既に読み込んでいる画像があればそれを使いまわして
     * メモリを節約する。
     * 要するに、画像のキャッシュです。
     * また、使用されていないキャッシュは
     * NotUsingTextureDelete()を呼ぶことで
     * メモリから解放できます
     * Spriteクラス等に使用されています。
    */

    public class TextureCache:ResourceCache<TextureCore,int>
    {
        //Textureを返す
        public Texture GetTexture(string filePath)
        {
            //キャッシュされていたらそれを使う
            if (isKey(filePath) == true)
            {
                return new Texture(GetResourceCore(filePath));
            }
            int gh = DX.LoadGraph(filePath);
            if (gh == -1)
            {
                throw new Exception("画像の読み込みに失敗しました");
            }
            var newCore = new TextureCore(gh);
            AddNewResourceCore(filePath, newCore);
            return new Texture(newCore);
        }
    }
}

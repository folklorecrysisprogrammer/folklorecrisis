using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace DXEX
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

    public static class TextureCache
    {
        //画像キャッシュdata
       static public Dictionary<string,TextureCore> textures = new Dictionary<string, TextureCore>();

        //Textureを返す
        static public Texture GetTexture(string filePath)
        {
            if (textures.ContainsKey(filePath) == true)
            {
                return new Texture(textures[filePath]);
            }
            int gh = DX.LoadGraph(filePath);
            if (gh == -1)
            {
                throw new Exception("画像の読み込みに失敗しました");
            }
            textures.Add(filePath, new TextureCore(gh));
            return new Texture(textures.Last().Value);
        }

        //使用していない画像リソースを解放
        static public void NotUsingTextureDelete() {
            var keys=new List<string>(); 
            foreach(var key in textures.Keys)
            {
                if (textures[key].NotUsing())
                {
                    keys.Add(key);
                }
            }
            foreach (var key in keys)
            {
                textures[key].Dispose();
                textures.Remove(key);
            }
        }

        //全ての画像リソースを解放
        static public void AllTextureDelete()
        {
            foreach (var key in textures.Keys)
            {
                textures[key].Dispose();
            }
            textures.Clear();
        }
    }
}

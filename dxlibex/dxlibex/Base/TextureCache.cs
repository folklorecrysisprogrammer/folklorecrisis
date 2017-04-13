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
        static public Dictionary<string, TextureCore> textureList = new Dictionary<string, TextureCore>();
        //分割画像キャッシュdata
        static public List<TextureCore> textureAtlasList = new List<TextureCore>();

        //Textureを返す
        static public Texture GetTexture(string filePath)
        {
            if (textureList.ContainsKey(filePath) == true)
            {
                return new Texture(textureList[filePath]);
            }
            int gh = DX.LoadGraph(filePath);
            if (gh == -1)
            {
                throw new Exception("画像の読み込みに失敗しました");
            }
            textureList.Add(filePath, new TextureCore(gh));
            return new Texture(textureList.Last().Value);
        }

        //画像を分割してTexture配列を返す
        static public Texture[] GetTextureAtlas(string filePath ,int AllNum,int XNum, int YNum,
                                              int XSize, int YSize)
        {
            int[] gh = new int[AllNum];
            Texture[] textures = new Texture[AllNum]; 
            int flag=DX.LoadDivGraph(filePath, AllNum, XNum, YNum,XSize,YSize, out gh[0]);
            if (flag == -1)
            {
                throw new Exception("画像の読み込みに失敗しました");
            }
            for (int i = 0; i < AllNum; i++)
            {
                textureAtlasList.Add(new TextureCore(gh[i]));
                textures[i] = new Texture(textureAtlasList.Last());
            }
            return textures;
        }

        //使用していない画像リソースを解放
        static public void NotUsingTextureDelete() {
            var removeKeys=new List<string>(); 
            foreach(var key in textureList.Keys)
            {
                if (textureList[key].NotUsing())
                {
                    removeKeys.Add(key);
                }
            }
            foreach (var key in removeKeys)
            {
                textureList[key].Dispose();
                textureList.Remove(key);
            }
            var removeList = new List<TextureCore>();
            textureAtlasList.ForEach((textureCore) => {
                if (textureCore.NotUsing())
                {
                    removeList.Add(textureCore);
                }
            });
            removeList.ForEach((textureCore) =>{
                textureCore.Dispose();
                textureAtlasList.Remove(textureCore);
            }
                );

        }

        //全ての画像リソースを解放
        static public void AllTextureDelete()
        {
            foreach (var key in textureList.Keys)
            {
                textureList[key].Dispose();
            }
            textureAtlasList.ForEach((textureCache) =>{
                textureCache.Dispose();
            });
            textureAtlasList.Clear();
            textureList.Clear();
        }
    }
}

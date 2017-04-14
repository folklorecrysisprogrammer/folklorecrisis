using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXEX
{
    //アニメーションコンポーネント
    class Anime:Component<Sprite>
    {
        //画像List
        List<Texture> textures = new List<Texture>();
        //Listの読み込み位置
        private int index = 0;
        //コマ送り速さ（フレーム単位）
        private uint speed = 0;
        public uint Speed { get { return speed; } set { speed = value; } }
        //アニメーションする
        public override IEnumerator Update()
        {
            if (speed == 0) yield break;
            yield return (int)speed;
            if (index < textures.Count)
            {
                owner.SetTexture(textures[index]);
                index++;
            }
            else index = 0;

        }
        //アニメーション画像追加
        public void AddTexture(string path)
        {
            textures.Add(TextureCache.GetTexture(path));
        }
        public void AddTexture(Texture texture)
        {
            textures.Add(texture);
        }

        //ここにアニメーション画像の削除関数が入る予定

        //
    }
}

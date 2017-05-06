using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DXEX.Base;
namespace DXEX.User
{
    //アニメーションコンポーネント
    public class Anime:Component<Sprite>
    {
        //画像List
        Dictionary<string,Texture[]> textures = new Dictionary<string,Texture[]>();
        //Listの読み込み位置
        private int index = 0;
        //再生止めるフラグ
        private bool StopFlag = false;
        //止める
        public void Stop() { StopFlag = true; }
        //再生
        public void Play() { StopFlag = false;index = 0; }
        //途中再開
        public void Resume() { StopFlag = false;}

        //コマ送り速さ（フレーム単位）
        private uint speed = 0;
        public uint Speed { get { return speed; } set { speed = value; } }
        //現在再生中のアニメーション
        private Texture[] CurrentAnimeTex;
        //アニメーションする
        public override IEnumerator Update()
        {
            while (true)
            {
                if (speed == 0|| StopFlag==true) yield break;
                index %= CurrentAnimeTex.Length;
                if (index < CurrentAnimeTex.Length)
                {
                    owner.SetTexture(CurrentAnimeTex[index]);
                    index++;
                    
                }
                yield return (int)speed;
            }
        }
        //アニメーション選択
        public void SetAnime(string animekey)
        {
            CurrentAnimeTex = textures[animekey];
            if (owner != null)
            {
                index = 1;
                owner.SetTexture(CurrentAnimeTex[0]);
            }
        }
        //アニメーション画像追加
        public void AddTextureList(string animeKey,Texture[] textureList)
        {
            textures[animeKey]=textureList;
        }
        
        //ここにアニメーション画像の削除関数が入る予定

        //
    }
}

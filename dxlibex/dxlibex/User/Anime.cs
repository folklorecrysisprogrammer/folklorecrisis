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
        //AnimeDataList
        Dictionary<string,AnimeData> animeDataList = new Dictionary<string,AnimeData>();
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
        //状態
        public int? State { get { return nowAnimeData.State; } set { nowAnimeData.State = value; } }
        //現在再生中のアニメーション
        private AnimeData nowAnimeData;
        //アニメーションする
        public override IEnumerator IeUpdate()
        {
            while (true)
            {
                if (speed == 0|| StopFlag==true) yield break;
                index %= nowAnimeData.texes.Length;
                if (index < nowAnimeData.texes.Length)
                {
                    owner.SetTexture(nowAnimeData.texes[index]);
                    index++;
                    
                }
                yield return (int)speed;
            }
        }
        //遅延カウントする
        public override void Update()
        {
            nowAnimeData.LazyCount();
        }
        //アニメーション選択
        public void SetAnime(string animekey)
        {
            nowAnimeData = animeDataList[animekey].Reset();
            if (owner != null)
            {
                index = 1;
                owner.SetTexture(nowAnimeData.texes[0]);
            }
        }
        //アニメーション画像追加
        public void AddAnime(string animeKey, Texture[] textureList, AnimeTrigger[] triggers = null, int? defaultState = null)
        {
            animeDataList[animeKey]=new AnimeData(this,textureList,triggers,defaultState);
        }
        
        //ここにアニメーション画像の削除関数が入る予定

        //
    }
}

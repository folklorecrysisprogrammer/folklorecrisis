using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DXEX.Base;
namespace DXEX.User
{
    //アニメーションのデータを持ち、遷移を管理するクラス
    class AnimeData
    {
        //現在遅延中のTrigger
        AnimeTrigger CurrentTrigger=null;
        //アニメーションに使うテクスチャー
        public readonly Texture[] texes;
        //遷移Triggerの配列
        readonly AnimeTrigger[] triggers;
        //アニメーションを切り替えさせるAnime
        readonly Anime anime;
        //Trigger遅延をカウントする用
        int lazyCounter=0;
        //現在の状態
        int state=-1;
        public int State {
            get { return state; }
            set { if (state == value) return;state=value ;StateCheck(); }
        }
        public AnimeData(Anime anime,Texture[] texes,AnimeTrigger[] triggers=null)
        {
            this.texes = texes;
            this.triggers = triggers;
            this.anime = anime;
        }
        //再利用できるように初期化
        public AnimeData Reset()
        {
            state = -1;
            lazyCounter = 0;
            CurrentTrigger = null;
            return this;
        }
        //遅延カウント
        public void LazyCount()
        {
            if (CurrentTrigger != null)
            {
                lazyCounter++;
                if (CurrentTrigger.lazyTime <= lazyCounter)
                {
                    anime.SetAnime(CurrentTrigger.animeKey);
                }
            }
        }
        //ステートが変更された時にTriggerの遷移条件と一致するかcheck
        private void StateCheck()
        {
            foreach(var trigger in triggers)
            {
                if (trigger.invokeState == state)
                {
                    if (trigger.lazyTime == 0)
                    {
                        anime.SetAnime(trigger.animeKey);
                        return;
                    }
                    else
                    {
                        lazyCounter = 0;
                        CurrentTrigger = trigger;
                        return;
                    }
                }
            }
            CurrentTrigger = null;
        }
    }
}

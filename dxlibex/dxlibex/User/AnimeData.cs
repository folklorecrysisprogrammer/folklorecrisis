using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DXEX.Base;
namespace DXEX.User
{
    class AnimeData
    {
        AnimeTrigger CurrentTrigger=null;
        public readonly Texture[] texes;
        readonly AnimeTrigger[] triggers;
        readonly Anime anime;
        int lazyCount=0;
        int state=0;
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
        public AnimeData Reset()
        {
            state = 0;
            lazyCount = 0;
            CurrentTrigger = null;
            StateCheck();
            return this;
        }
        public void Update()
        {
            if (CurrentTrigger != null)
            {
                lazyCount++;
                if (CurrentTrigger.lazyTime <= lazyCount)
                {
                    anime.SetAnime(CurrentTrigger.animeKey);
                }
            }
        }
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
                        lazyCount = 0;
                        CurrentTrigger = trigger;
                        return;
                    }
                }
            }
        }
    }
}

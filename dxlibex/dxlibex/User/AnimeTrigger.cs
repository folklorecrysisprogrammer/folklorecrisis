using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXEX.User
{
    //アニメーションが遷移する状態を定義する
    public class AnimeTrigger
    {
        //遷移条件
        public readonly int invokeState;
        //遷移するアニメキー名を指定
        public readonly string animeKey;
        //遷移の遅延（フレーム単位）
        public readonly int lazyTime;
        public AnimeTrigger(int invokeState,string animeKey,int lazyTime=0)
        {
            this.invokeState = invokeState;
            this.animeKey = animeKey;
            this.lazyTime = lazyTime;
        }
    }
}

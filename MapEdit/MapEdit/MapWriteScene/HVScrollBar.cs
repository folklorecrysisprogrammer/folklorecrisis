using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapEdit
{
    //縦と横のスクロールバーを持ち、制御するクラス
    public class HVScrollBar
    {
        protected readonly ScrollBar hScroll;
        protected readonly ScrollBar vScroll;
        protected event Action ValueChenged;

        protected HVScrollBar(ScrollBar hScroll,ScrollBar vScroll)
        {
            this.hScroll = hScroll;
            this.vScroll = vScroll;

            //スクロールバーがスクロールされたら、
            //OnValueChenged関数が呼ばれるようにしておく
            hScroll.ValueChanged += OnValueChenged;
            vScroll.ValueChanged += OnValueChenged;
        }

        //ValueChengedイベントを発火する
        public void OnValueChenged(object o,EventArgs e)
        {
            ValueChenged?.Invoke();
        }

        //スクロールバーの変化量を設定
        //一回スクロールで一マス移動するようにする
        public void SetScrollDelta(int delta)
        {
            vScroll.SmallChange = delta;
            vScroll.LargeChange = delta;
            hScroll.SmallChange = delta;
            hScroll.LargeChange = delta;
        }

        //終了処理
        protected void Dispose()
        {
            hScroll.ValueChanged -= OnValueChenged;
            vScroll.ValueChanged -= OnValueChenged;
        }
    }
}

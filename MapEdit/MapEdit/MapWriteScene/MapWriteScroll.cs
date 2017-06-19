using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MapEdit
{
    //MapWriteSceneをスクロールするためのクラス
    public class MapWriteScroll:IDisposable
    {
        private readonly HScrollBar hScroll;
        private readonly VScrollBar vScroll;
        private readonly MapWriteScene mws;

        public MapWriteScroll(HScrollBar hScroll, VScrollBar vScroll, MapWriteScene mws,Size mapSize,int mapChipSize)
        {
            this.hScroll = hScroll;
            this.vScroll = vScroll;
            this.mws = mws;
            SetScrollDelta(mapChipSize);
            SetScrollMaximum(mapSize,mapChipSize);
            //スクロールバーの値が更新されたら、mwsの位置を更新する処理を呼ぶ
            hScroll.ValueChanged += UpdateValue;
            vScroll.ValueChanged += UpdateValue;
        }

        public void Dispose()
        {
            hScroll.ValueChanged -= UpdateValue;
            vScroll.ValueChanged -= UpdateValue;
        }

        //スクロールバーの値を元に、mwsの位置を更新する
        public void UpdateValue( object o,EventArgs e)
        {
            mws.LocalPosX = -hScroll.Value;
            mws.LocalPosY = -vScroll.Value;
        }

        //スクロールバーの変化量を設定
        //一回スクロールで一マス移動するようにする
        public void SetScrollDelta(int mapChipSize)
        {
            vScroll.SmallChange = mapChipSize;
            vScroll.LargeChange = mapChipSize;
            hScroll.SmallChange = mapChipSize;
            hScroll.LargeChange = mapChipSize;
        }

        //スクロールバーの最大値を設定
        public void SetScrollMaximum(Size mapSize,int mapChipSize)
        {
            hScroll.Value = 0;
            vScroll.Value = 0;
            mws.LocalPos = new DXEX.Vect(0, 0);
            int temp = mapSize.Width * mapChipSize - mws.GetControl.Size.Width;
            if (temp < 0) hScroll.Maximum = 0;
            else hScroll.Maximum = temp;
            temp = mapSize.Height * mapChipSize - mws.GetControl.Size.Height;
            if (temp < 0) vScroll.Maximum = 0;
            else vScroll.Maximum = temp;
        }

        //スクロールバーの値を範囲内に収めながら加算する
        static public void ScrollBarAddValue(ScrollBar scrollBar, int plus)
        {
            if (scrollBar.Value + plus > scrollBar.Maximum)
            {
                scrollBar.Value = scrollBar.Maximum;
            }
            else if (scrollBar.Value + plus < scrollBar.Minimum)
            {
                scrollBar.Value = scrollBar.Minimum;
            }
            else
            {
                scrollBar.Value += plus;
            }
        }

        //キーが押されたらスクロールバーをスクロールする
        public void KeyScroll(KeyEventArgs e)
        {
            mws.control.Focus();

            //WASDキーが押されていたら、スクロールバーをスクロール
            if (e.KeyData == Keys.D)
            {
                ScrollBarAddValue(hScroll, hScroll.LargeChange);
                hScroll.Focus();
            }
            if (e.KeyData == Keys.A)
            {
                ScrollBarAddValue(hScroll, -hScroll.LargeChange);
                hScroll.Focus();
            }
            if (e.KeyData == Keys.S)
            {
                ScrollBarAddValue(vScroll, vScroll.LargeChange);
                vScroll.Focus();
            }
            if (e.KeyData == Keys.W)
            {
                ScrollBarAddValue(vScroll, -vScroll.LargeChange);
                vScroll.Focus();
            }
        }
    }
}

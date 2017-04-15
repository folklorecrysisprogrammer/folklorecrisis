using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapEdit
{
    //MapWriteSceneをスクロールするためのクラス
   public class MapWriteScroll
    {
        private HScrollBar hScroll;
        private VScrollBar vScroll;
        private MapWriteScene mws;

        public MapWriteScroll(HScrollBar hScroll,VScrollBar vScroll,MapWriteScene mws)
        {
            this.hScroll = hScroll;
            this.vScroll = vScroll;
            this.mws = mws;

            //スクロールバーがスクロールされたら、
            //フォーカスを当てるようにしてmouseホイールしやすくする
            hScroll.Scroll += (o, e) =>
            {
                hScroll.Focus();
            };
            vScroll.Scroll += (o, e) =>
            {
                vScroll.Focus();
            };


            SetScrollDelta();
            SetScrollMaximum();
        }

        //スクロールバーの値を元に、mwsの位置を更新する
        public void UpdateValue()
        {
            mws.LocalPosX = -hScroll.Value;
            mws.LocalPosY = -vScroll.Value;
            mws.UpdateShowMapImage();
        }

        //スクロールバーの変化量を設定
        //一回スクロールで一マス移動するようにする
        public void SetScrollDelta()
        {
            vScroll.SmallChange = mws.GetMapData().MapChipSize;
            vScroll.LargeChange = mws.GetMapData().MapChipSize;
            hScroll.SmallChange = mws.GetMapData().MapChipSize;
            hScroll.LargeChange = mws.GetMapData().MapChipSize;
        }

        //スクロールバーの最大値を設定
        public void SetScrollMaximum()
        {
            hScroll.Value = 0;
            vScroll.Value = 0;
            mws.LocalPos=new DXEX.Vect(0, 0);
            int temp = mws.GetMapData().MapSize.Width * mws.GetMapData().MapChipSize - mws.GetControl.Size.Width;
            if (temp < 0) hScroll.Maximum = 0;
            else hScroll.Maximum = temp;
            temp = mws.GetMapData().MapSize.Height * mws.GetMapData().MapChipSize - mws.GetControl.Size.Height;
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

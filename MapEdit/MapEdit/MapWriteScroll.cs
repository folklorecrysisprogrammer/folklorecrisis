﻿using System;
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


            //スクロールバーの値が更新されたら
            //MapWriteSceneの位置を変更する
            hScroll.ValueChanged += (o, e) =>
            {
                mws.localPos.x = -hScroll.Value;
            };
            vScroll.ValueChanged += (o, e) =>
            {
                mws.localPos.y = -vScroll.Value;
            };
        }

        //スクロールバーの変化量を設定
        //一回スクロールで一マス移動するようにする
        public void SetScrollDelta()
        {
            vScroll.SmallChange = mws.MapChipSize;
            vScroll.LargeChange = mws.MapChipSize;
            hScroll.SmallChange = mws.MapChipSize;
            hScroll.LargeChange = mws.MapChipSize;
        }

        //スクロールバーの最大値を設定
        public void SetScrollMaximum()
        {
            hScroll.Value = 0;
            vScroll.Value = 0;
            mws.localPos.SetVect(0, 0);
            int temp = mws.MapSize.Width * mws.MapChipSize - mws.GetControl.Size.Width;
            if (temp < 0) hScroll.Maximum = 0;
            else hScroll.Maximum = temp;
            temp = mws.MapSize.Height * mws.MapChipSize - mws.GetControl.Size.Height;
            if (temp < 0) vScroll.Maximum = 0;
            else vScroll.Maximum = temp;
        }
    }
}
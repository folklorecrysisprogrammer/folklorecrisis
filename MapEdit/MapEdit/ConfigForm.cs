﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapEdit
{
    //設定ウインドウ
    public partial class ConfigForm : Form
    {
        private readonly Action<Size> changeMapSize;
        private readonly Size mapSize;
        //プルヌ
        private readonly Purunu purunu;

        //初期化
        public ConfigForm(Size mapSize,Action<Size> changeMapSize)
        {
            
            InitializeComponent();
            this.mapSize = mapSize;
            this.changeMapSize = changeMapSize;
            var rnd=new System.Random();
            if (rnd.Next(10) == 0) purunu=new Purunu(chara);

            //マップサイズの値をテキストボックスにセットする
            mapSizeXtextBox.Text = mapSize.Width.ToString();
            mapSizeYtextBox.Text = mapSize.Height.ToString();
        }

        //更新ボタンが押された時,マップチップ、マップサイズの値が変更されてたら、
        //変更を反映する。
        private void updateButton_Click(object sender, EventArgs e)
        {
            int result;
            //各種値が変更されていたら、変更処理を行う
            //もし不正な値であれば、変更処理を行わない

            //MapSizeの変更処理判定
            if (
                    TryParse(mapSizeXtextBox.Text, out result) && 
                    mapSize.Width != result||
                    TryParse(mapSizeYtextBox.Text, out result) &&
                    mapSize.Height != result 
                )
            {
                    changeMapSize(new Size(
                        int.Parse(mapSizeXtextBox.Text),
                        int.Parse(mapSizeYtextBox.Text)
                    ));
            }

            //設定ウインドウを閉じる。
            Dispose();
        }

        //文字列を変換したとき、0より大きい整数になるかどうかを判定
        private bool TryParse(string s,out int result)
        {
           if(int.TryParse(s, out result) && result>0)
            {
                return true;
            }
            return false;
        }
    }
}

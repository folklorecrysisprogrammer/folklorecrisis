using System;
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

    /// <summary>
    ///設定ウインドウ
    /// </summary>
    public partial class ConfigForm : Form
    {
        /// <summary>
        /// 設定でマップサイズを変更したときに呼ばれる関数
        /// </summary>
        private readonly Action<Size> changeMapSize;
        /// <summary>
        /// マップサイズ
        /// </summary>
        private readonly Size mapSize;

        /// <summary>
        ///プルヌ制御
        /// </summary>
        private readonly Purunu purunu;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="mapSize">現在のマップサイズ</param>
        /// <param name="changeMapSize">マップサイズ変更処理を任せる関数</param>
        public ConfigForm(Size mapSize,Action<Size> changeMapSize)
        {
            
            InitializeComponent();
            this.mapSize = mapSize;
            this.changeMapSize = changeMapSize;
            if (new System.Random().Next(10) == 0) purunu=new Purunu(chara);

            //マップサイズの値をテキストボックスにセットする
            mapSizeXtextBox.Text = mapSize.Width.ToString();
            mapSizeYtextBox.Text = mapSize.Height.ToString();
        }
        /// <summary>
        ///更新ボタンが押された時の処理
        ///マップサイズの値が変更されてたら、
        ///changeMapSizeに変更処理を任せる。 
        /// </summary>
        /// <param name="sender">使用しない引数</param>
        /// <param name="e">使用しない引数</param>
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

        
        /// <summary>
        ///文字列を変換したとき、0より大きい整数になるかどうかを判定
        /// </summary>
        /// <param name="s">変換対象文字列</param>
        /// <param name="result">変換結果格納用</param>
        /// <returns>変換成功フラグ</returns>
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

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
    //設定ウインドウ
    public partial class ConfigForm : Form
    {
        private MapWriteScene mws;
        private Timer timer=new Timer();
        private bool runFlag=false;

        //初期化
        public ConfigForm(MapWriteScene mapWriteScene)
        {
            InitializeComponent();
            this.mws = mapWriteScene;
            var myAssembly=System.Reflection.Assembly.GetExecutingAssembly();
            chara.Image =new Bitmap(myAssembly.GetManifestResourceStream("MapEdit.Resources.プルヌ.png"));
            var rnd=new System.Random();
            if (rnd.Next(10) != 0) chara.Visible = false;
            chara.MouseEnter += (o, e) =>
            {
                runFlag = true;
            };
            timer.Interval = 1;
            timer.Start();
            timer.Tick += (o, e) =>
            {
                if (runFlag)
                {
                    chara.Location=new Point(chara.Location.X+3,chara.Location.Y);
                }
            };
            //マップサイズの値をテキストボックスにセットする
            mapSizeXtextBox.Text = this.mws.MapData.MapSizeX.ToString();
            mapSizeYtextBox.Text = this.mws.MapData.MapSizeY.ToString();
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
                    mws.MapData.MapSizeX != result||
                    TryParse(mapSizeYtextBox.Text, out result) &&
                    mws.MapData.MapSizeY != result 
                )
            {
                mws.ClearShowMapImage();
                mws.MapData.MapSize = 
                    new Size(
                        int.Parse(mapSizeXtextBox.Text),
                        int.Parse(mapSizeYtextBox.Text)
                    );
                //スクロールバーの調整
                mws.GetScroll().SetScrollMaximum();
                //表示するスプライトの設定
                mws.AddShowMapImage();
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

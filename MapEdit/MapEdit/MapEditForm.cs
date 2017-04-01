using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DxLibDLL;

namespace MapEdit
{
    //マップチップを置いていくフォーム
    public partial class MapEditForm : Form
    {
        //layerの数
        public const int maxLayer = 3;
        public SelectImageForm selectImageForm;
        //実際にマップを描画するシーン
        private MapWriteScene mapWriteScene;
        //初期化
        public MapEditForm()
        {

            InitializeComponent();
            selectImageForm = new SelectImageForm();
            
           //各種イベントハンドラ
            Load += (o, e) => {
                selectImageForm.Show();
            };
            
            KeyDown += (o, e) =>
            {
                mapWritePanel.Focus();
                if (e.KeyData == Keys.D )
                {
                    ScrollBarAddValue(hScrollBar1, hScrollBar1.LargeChange);
                    hScrollBar1.Focus();
                }
                if (e.KeyData == Keys.A)
                {
                    ScrollBarAddValue(hScrollBar1, -hScrollBar1.LargeChange);
                    hScrollBar1.Focus();
                }
                if (e.KeyData == Keys.S)
                {
                    ScrollBarAddValue(vScrollBar1, vScrollBar1.LargeChange);
                    vScrollBar1.Focus();
                }
                if (e.KeyData == Keys.W)
                {
                    ScrollBarAddValue(vScrollBar1, -vScrollBar1.LargeChange);
                    vScrollBar1.Focus();
                }
            };

            //DXEX初期化
            DXEX.Director.init(this);
            DX.SetAlwaysRunFlag(DX.TRUE);
            //描画領域をセット
            DxLibDLL.DX.SetGraphMode(
                Screen.PrimaryScreen.Bounds.Width,
                Screen.PrimaryScreen.Bounds.Height,
                32
                );

            mapWriteScene = 
                new MapWriteScene(selectImageForm,mapWritePanel,hScrollBar1,vScrollBar1);
            layerComboBox.SelectedIndex = 0;
            drawModeComboBox.SelectedIndex = 0;
            DxLibDLL.DX.SetBackgroundColor(90, 230, 120);
            Show();
            //DXライブラリループ開始
            DXEX.Director.StartLoop(this,mapWriteScene);
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

        //設定ボタンが押された時の処理
        private void 設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var configForm = new ConfigForm(mapWriteScene);
            configForm.ShowDialog(this);
            

        }

        //layerが変更された時の処理
        private void layerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            mapWriteScene.CurrentLayer = layerComboBox.SelectedIndex;
        }

        //描画方法変更された時の処理
        private void drawModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drawModeComboBox.SelectedIndex == 0)
            {
                //ニアレストバイバー
                DX.SetDrawMode(DX.DX_DRAWMODE_NEAREST);
            }
            else
            {
                //バイリニア
                DX.SetDrawMode(DX.DX_DRAWMODE_BILINEAR);
            }
        }

        //画像出力メニューが選択されたときの処理
        private void 画像出力ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mapWriteScene.GetBitmap().Save("w.png",ImageFormat.Png);
        }

        private void rotateRightButton_Click(object sender, EventArgs e)
        {
            mapWriteScene.RotateRight();
        }

        private void rotateLeftButton_Click(object sender, EventArgs e)
        {
            mapWriteScene.RotateLeft();
        }
    }
}

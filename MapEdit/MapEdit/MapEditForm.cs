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
        //マップチップパレットフォーム
        public SelectImageForm sif=new SelectImageForm();
        //実際にマップを描画するシーン
        private MapWriteScene mws;
        //ファイル操作に関係するクラス
        private FileManager filem=new FileManager();
        //初期化
        public MapEditForm()
        {
            InitializeComponent();    
                    
           //メインウインドウのロードが終わったら、
           //パレッドウインドウを表示する。
            Load += (o, e) => {
                sif.Show();
            };

            //メインウインドウに終了命令が出たら
            //パレッドウインドウを速やかに閉じる
            FormClosing += (o, e) =>
            {
                sif.Dispose();
            };
            
            //キーが押された時の処理
            KeyDown += (o, e) =>
            {
                mapWritePanel.Focus();

                //WASDキーが押されていたら、スクロールバーをスクロール
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
            //（見切れないように、画面いっぱいに設定する）
            DxLibDLL.DX.SetGraphMode(
                Screen.PrimaryScreen.Bounds.Width,
                Screen.PrimaryScreen.Bounds.Height,
                32
                );

            //DXライブラリの描画先の背景色を設定する
            DxLibDLL.DX.SetBackgroundColor(100, 240, 130);

            //mapWriteScene初期化
            //mapWritePanelをDXライブラリの描画先に設定
            mws = 
                new MapWriteScene(sif,mapWritePanel,hScrollBar1,vScrollBar1);

            //comboボックスのデフォルト値設定
            layerComboBox.SelectedIndex = 0;
            drawModeComboBox.SelectedIndex = 0;
            
            //メインウインドウ表示
            Show();
            //DXライブラリループ開始
            DXEX.Director.StartLoop(this,mws);
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
            var configForm = new ConfigForm(mws);
            configForm.ShowDialog(this);
        }

        //画像出力メニューが選択されたときの処理
        private void 画像出力ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filem.MapImageOutPut(mws.GetMapData().GetBitmap());
        }

        //保存メニューが選択された時の処理
        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var snpf=new SaveNewProjectForm();
            snpf.ShowDialog(this);
        }

        //layerが変更された時の処理
        private void layerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            mws.CurrentLayer = layerComboBox.SelectedIndex;
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

        //右回転ボタンを押したときの処理
        private void rotateRightButton_Click(object sender, EventArgs e)
        {
            mws.GetMapData().RotateRight();
        }

        //左回転ボタンを押したときの処理
        private void rotateLeftButton_Click(object sender, EventArgs e)
        {
            mws.GetMapData().RotateLeft();
        }

        //上下反転ボタンを押したときの処理
        private void turnVerticalButton_Click(object sender, EventArgs e)
        {
            mws.GetMapData().turnVertical();
        }

        //左右反転ボタンを押したときの処理
        private void turnHorizontalButton_Click(object sender, EventArgs e)
        {
            mws.GetMapData().turnHorizontal();
        }
    }
}

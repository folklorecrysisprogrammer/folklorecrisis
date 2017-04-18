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
        public SelectImageForm sif { get; private set;}
        //プロジェクトデータを保存,上書き,開く機能をするクラス
        private ProjectManager pm;
        //実際にマップを描画するシーン
        public MapWriteScene mws { get; private set; }
        //マップチップリソース管理
        public MapChipResourceManager mcrm { get; private set;}

        //マップをスクロールするスクロールバー
        public VScrollBar Vscroll { get { return vScrollBar1; } }
        public HScrollBar Hscroll { get { return hScrollBar1; } }
        //マップを表示するパネル
        public Panel mwp { get { return mapWritePanel; } }

       // public int MapChipSize { get; private set; } 

        //初期化
        public MapEditForm(int mapChipSize)
        {
            InitializeComponent();
            pm = new ProjectManager(this);
            mcrm = new MapChipResourceManager(mapChipSize);
            sif = new SelectImageForm(this);
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

            //スクロールバーの値が更新されたら、mwsの位置を更新する処理を呼ぶ
            hScrollBar1.ValueChanged += (o, e) =>
            {
                mws.GetScroll().UpdateValue();
            };
            vScrollBar1.ValueChanged += (o, e) =>
            {
                mws.GetScroll().UpdateValue();
            };

            //スクロールバーがスクロールされたら、
            //フォーカスを当てるようにしてmouseホイールしやすくする
            hScrollBar1.Scroll += (o, e) =>
            {
                hScrollBar1.Focus();
            };
            vScrollBar1.Scroll += (o, e) =>
            {
                vScrollBar1.Focus();
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
                new MapWriteScene(this,new Size(20,20),mapChipSize);

            //comboボックスのデフォルト値設定
            layerComboBox.SelectedIndex = 0;
            drawModeComboBox.SelectedIndex = 0;
            
            //メインウインドウ表示
            Show();
            //DXライブラリループ開始
            DXEX.Director.StartLoop(this,mws);
        }

        public void LoadProject(int mapChipSize,Size mapSize) {
            mcrm = new MapChipResourceManager(mapChipSize);
            //mapWriteScene初期化
            //mapWritePanelをDXライブラリの描画先に設定
            DXEX.Director.RemoveSubScene(mws);
            mws.Dispose();
            mws =
                new MapWriteScene(this,mapSize,mapChipSize);
            DXEX.Director.AddSubScene(mws);
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
            FileManager.BitmapOutPut(mws.MapData.GetBitmap());
        }

        //保存メニューが選択された時の処理
        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var snpf=new SaveNewProjectForm(pm);
            snpf.ShowDialog(this);
        }

        //開くメニューが選択された時の処理
        private void 開くToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();

            //ダイアログを表示する
            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                if (pm.LoadProject(fbd.SelectedPath) == false)
                {
                    MessageBox.Show("エラー", "プロジェクトが存在しません", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //上書きメニューが選択された時の処理
        private void 上書きToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pm.OverwriteProject() == false)
            {
                MessageBox.Show("エラー", "プロジェクトが存在しません", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            mws.MapData.RotateRight();
            //スクロールバーの調整
            mws.GetScroll().SetScrollMaximum();
            //表示するスプライトの設定
            mws.UpdateShowMapImage();
        }

        //左回転ボタンを押したときの処理
        private void rotateLeftButton_Click(object sender, EventArgs e)
        {
            mws.MapData.RotateLeft();
            //スクロールバーの調整
            mws.GetScroll().SetScrollMaximum();
            //表示するスプライトの設定
            mws.UpdateShowMapImage();
        }

        //上下反転ボタンを押したときの処理
        private void turnVerticalButton_Click(object sender, EventArgs e)
        {
            mws.MapData.turnVertical();
        }

        //左右反転ボタンを押したときの処理
        private void turnHorizontalButton_Click(object sender, EventArgs e)
        {
            mws.MapData.turnHorizontal();
        }

        //キーが押された時の処理
        private void MapEditForm_KeyDown(object sender, KeyEventArgs e)
        {
                mws.GetScroll().KeyScroll(e);
        }

        //マップ描画のパネルのサイズが変更されたら、
        //マップに表示するスプライトの調整や
        //スクロールバーの調整を行う
        private void mapWritePanel_SizeChanged(object sender, EventArgs e)
        {
            mws.GetScroll().SetScrollMaximum();
            mws.UpdateShowMapImage();
        }

        //パネル上でマウスが操作された時の処理をする
        private void mapWritePanel_MouseDown(object sender, MouseEventArgs e)
        {
            mws.MouseAction(sender, e);
        }
        private void mapWritePanel_MouseMove(object sender, MouseEventArgs e)
        {
            mws.MouseAction(sender, e);
        }
    }
}

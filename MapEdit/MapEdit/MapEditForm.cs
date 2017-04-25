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
       // public MapWriteScene mws { get; private set; }
       // public MapData MapData { get; private set; }
        //private EditMapChip editMapData;
        //private MapShowArea mapShowArea;
        //マップチップリソース管理
        public MapChipResourceManager mcrm { get; private set;}
        public MapEdit mapEdit;

        //マップをスクロールするスクロールバー
       // public VScrollBar Vscroll { get { return vScrollBar1; } }
       // public HScrollBar Hscroll { get { return hScrollBar1; } }
      //  private MapWriteScroll mapWriteScroll;
        //マップを表示するパネル
        public Panel mwp { get { return mapWritePanel; } }

       // public int MapChipSize { get; private set; } 

        //初期化
        public MapEditForm(int mapChipSize)
        {
            InitializeComponent();
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
            //this.MapData = new MapData(this, new Size(20, 20),mapChipSize);
            //mws =
              //  new MapWriteScene(this, mapChipSize);
           // mapShowArea = new MapShowArea(mws, MapData);
           // mapShowArea.UpdateShowMapImage();
           // mapWriteScroll = new MapWriteScroll(hScrollBar1, vScrollBar1, mws, MapData,mapShowArea);

            
            mcrm = new MapChipResourceManager(mapChipSize);
            sif = new SelectImageForm(this);
            mapEdit = new MapEdit(mwp,mcrm, sif, layerComboBox, hScrollBar1, vScrollBar1, new Size(20, 20), mapChipSize);
            pm = new ProjectManager(this);
           // editMapData = new EditMapChip(MapData, sif,mws, layerComboBox);
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
                mapEdit.UpdateScrollValue();
            };
            vScrollBar1.ValueChanged += (o, e) =>
            {
                mapEdit.UpdateScrollValue();
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


            //comboボックスのデフォルト値設定
            layerComboBox.SelectedIndex = 0;
            drawModeComboBox.SelectedIndex = 0;
            
            //メインウインドウ表示
            Show();
            //DXライブラリループ開始
            DXEX.Director.StartLoop(this);
        }

        public void LoadProject(MapDataFromText mdft,int lastId,string path) {
            hScrollBar1.Value = 0;
            vScrollBar1.Value = 0;
            mapEdit.Dispose();
            mcrm = new MapChipResourceManager(mdft.MapChipSize);
            mcrm.LoadBitmapSheet(lastId, path + @"\MapChip.png");
            sif.mps.LoadProject();
            //mapWriteScene初期化
            //mapWritePanelをDXライブラリの描画先に設定
            /*DXEX.Director.RemoveSubScene(mws);
            mws.Dispose();
            MapData = new MapData(this, mapSize, mapChipSize);
            mws =
                new MapWriteScene(this,mapChipSize);
            mapShowArea = new MapShowArea(mws, MapData);
            mapShowArea.UpdateShowMapImage();
            mapWriteScroll = new MapWriteScroll(hScrollBar1, vScrollBar1, mws, MapData, mapShowArea);
            editMapData = new EditMapChip(MapData, sif, mws, layerComboBox);
            DXEX.Director.AddSubScene(mws);*/
            mapEdit = new MapEdit(mdft,mwp,mcrm,sif,layerComboBox,hScrollBar1,vScrollBar1,mdft.MapSize,mdft.MapChipSize);
        }


        //設定ボタンが押された時の処理
        private void 設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var configForm = mapEdit.CreateConfigForm();
            configForm.ShowDialog(this);
        }

        //画像出力メニューが選択されたときの処理
        private void 画像出力ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileManager.BitmapOutPut(mapEdit.GetBitmap());
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
            mapEdit.MapRotateRight();
        }

        //左回転ボタンを押したときの処理
        private void rotateLeftButton_Click(object sender, EventArgs e)
        {
            mapEdit.MapRotateLeft();
        }

        //上下反転ボタンを押したときの処理
        private void turnVerticalButton_Click(object sender, EventArgs e)
        {
            mapEdit.MapTurnVertical();
        }

        //左右反転ボタンを押したときの処理
        private void turnHorizontalButton_Click(object sender, EventArgs e)
        {
            mapEdit.MapTurnHorizontal();

        }

        //キーが押された時の処理
        private void MapEditForm_KeyDown(object sender, KeyEventArgs e)
        {
            mapEdit.KeyScroll(e);
        }

        //マップ描画のパネルのサイズが変更されたら、
        //マップに表示するスプライトの調整や
        //スクロールバーの調整を行う
        private void mapWritePanel_SizeChanged(object sender, EventArgs e)
        {
            mapEdit.mapWritePanel_SizeChanged();
        }

        //パネル上でマウスが操作された時の処理をする
        private void mapWritePanel_MouseDown(object sender, MouseEventArgs e)
        {
            mapEdit.MapMouseAction( e);
        }
        private void mapWritePanel_MouseMove(object sender, MouseEventArgs e)
        {
            mapEdit.MapMouseAction(e);
        }

        private void gridButton_Click(object sender, EventArgs e)
        {
            mapEdit.gridOnOff();
        }
    }
}

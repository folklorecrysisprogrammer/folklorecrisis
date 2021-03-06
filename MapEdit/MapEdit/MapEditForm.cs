﻿using System;
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
        private readonly SelectImageForm sif;
        //プロジェクトデータを保存,上書き,開く機能をするクラス
        private ProjectManager pm;
        //マップチップリソース管理
        private MapChipResourceManager mcrm;
        private MapEditControl mapEdit;


        //初期化
        public MapEditForm(int mapChipSize)
        {
            InitializeComponent();
            //DXEX初期化
            DXEX.DirectorForForm.init(this);
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
            
            mcrm = new MapChipResourceManager(mapChipSize);
            sif = new SelectImageForm(mcrm);
            mapEdit = new MapEditControl(mapWritePanel, hScrollBar1, vScrollBar1, new Size(20, 20), mapChipSize);
            pm = new ProjectManager();
            //メインウインドウのロードが終わったら、
            //パレッドウインドウを表示する。
            Load += (o, e) =>sif.Show();

            //メインウインドウに終了命令が出たら
            //パレッドウインドウを速やかに閉じる
            FormClosing += (o, e) =>sif.Dispose();
            

            //comboボックスのデフォルト値設定
            layerComboBox.SelectedIndex = 0;
            drawModeComboBox.SelectedIndex = 0;
            
            //メインウインドウ表示
            Show();
            //DXライブラリループ開始
            DXEX.DirectorForForm.StartLoop(this);
        }


        //プロジェクトを読み込みしたときの処理
        private void LoadProject(MapInfoFromText mift,string path) {
            DXEX.TextureCache.AllTextureDelete();
            mcrm = mcrm.LoadProject(mift, path + @"\MapChip.png");
            sif.LoadProject(mcrm);
            mapEdit = mapEdit.LoadProject(mift,mapWritePanel,mcrm,sif,hScrollBar1,vScrollBar1);
        }

        //ProjectInfoの生成
        private ProjectInfo GetProjectInfo()
            =>new ProjectInfo(mcrm.GetBitmapSheet(),mcrm.LastID(),mapEdit.MapDataControl.ConvertData.GetMapDataText());


        //プロジェクトの保存
        public void SaveNewProject(string folderPath,string projectName)
        {
            if (pm.SaveNewProject(folderPath, projectName,GetProjectInfo()) == false)
                MessageBox.Show("パスが存在しません", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        //設定ボタンが押された時の処理
        private void 設定ToolStripMenuItem_Click(object sender, EventArgs e)
            =>mapEdit.CreateConfigForm().ShowDialog(this);


        //画像出力メニューが選択されたときの処理
        private void 画像出力ToolStripMenuItem_Click(object sender, EventArgs e)
            =>FileManager.BitmapOutPut(mapEdit.MapDataControl.ConvertData.GetBitmap(mcrm));


        //保存メニューが選択された時の処理
        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
            =>new SaveNewProjectForm(this).ShowDialog(this);


        //開くメニューが選択された時の処理
        private void 開くToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            fbd.SelectedPath = System.IO.Directory.GetCurrentDirectory();

            //ダイアログを表示する
            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                MapInfoFromText mift;
                try
                {
                     mift= pm.LoadProject(fbd.SelectedPath);
                }
                catch
                {
                    MessageBox.Show("エラー", "プロジェクトが存在しません", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                LoadProject(mift, fbd.SelectedPath);
            }
        }


        //上書きメニューが選択された時の処理
        private void 上書きToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pm.OverwriteProject(GetProjectInfo()) == false)
                MessageBox.Show("エラー", "プロジェクトが存在しません", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        //描画方法変更された時の処理
        private void drawModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drawModeComboBox.SelectedIndex == 0)
                //ニアレストバイバー
                DX.SetDrawMode(DX.DX_DRAWMODE_NEAREST);
            else
                //バイリニア
                DX.SetDrawMode(DX.DX_DRAWMODE_BILINEAR);
        }


        //右回転ボタンを押したときの処理
        private void rotateRightButton_Click(object sender, EventArgs e)
            =>mapEdit.MapDataControl.Turn.RotateRight();


        //左回転ボタンを押したときの処理
        private void rotateLeftButton_Click(object sender, EventArgs e)
            =>mapEdit.MapDataControl.Turn.RotateLeft();


        //上下反転ボタンを押したときの処理
        private void turnVerticalButton_Click(object sender, EventArgs e)
            =>mapEdit.MapDataControl.Turn.TurnVertical();


        //左右反転ボタンを押したときの処理
        private void turnHorizontalButton_Click(object sender, EventArgs e)
            =>mapEdit.MapDataControl.Turn.TurnHorizontal();


        //キーが押された時の処理
        private void MapEditForm_KeyDown(object sender, KeyEventArgs e)
            =>mapEdit.MapWriteScroll.KeyScroll(e);


        //マップ描画のパネルのサイズが変更されたら、
        //マップに表示するスプライトの調整や
        //スクロールバーの調整を行う
        private void mapWritePanel_SizeChanged(object sender, EventArgs e)
            =>mapEdit.
                MapWriteScroll.
                SetScrollMaximum(mapEdit.MapDataControl.MapSize, mapEdit.MapDataControl.MapChipSize);


        //パネル上でマウスが操作された時の処理をする
        private void mapWritePanel_MouseDown(object sender, MouseEventArgs e)
            =>mapEdit.MapMouseAction(e.Location,layerComboBox.SelectedIndex,sif.GetSelectMapChip());

        private void mapWritePanel_MouseMove(object sender, MouseEventArgs e)
            =>mapEdit.MapMouseAction(e.Location, layerComboBox.SelectedIndex, sif.GetSelectMapChip());


        //グリッドをOn、OFFする
        private void gridButton_Click(object sender, EventArgs e)
            =>MapGrid.GridOnOf();
    }
}

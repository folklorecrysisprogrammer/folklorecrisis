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
    public partial class SelectImageForm : Form
    {

        public MapPalletScene mps { get; }

        public SelectMapChipScene SelectMapChipScene { get; }

        public MapEditForm MeForm { get;}

        public MapChip GetSelectMapChip() { return SelectMapChipScene.MapChip; }

        //右回転ボタンを押したときの処理
        private void rotateRightButton_Click(object sender, EventArgs e)
        {
            SelectMapChipScene.MapChip.Angle+=90;
        }

        //左回転ボタンを押したときの処理
        private void rotateLeftButton_Click(object sender, EventArgs e)
        {
            SelectMapChipScene.MapChip.Angle += 270;
        }

        //上下反転ボタンを押したときの処理
        private void turnVerticalButton_Click(object sender, EventArgs e)
        {
            SelectMapChipScene.MapChip.TurnVertical();
        }

        //左右反転ボタンを押したときの処理
        private void turnHorizontalButton_Click(object sender, EventArgs e)
        {
            SelectMapChipScene.MapChip.TurnHorizontal();
        }

        public SelectImageForm(MapEditForm meform)
        {
            
            InitializeComponent();
            MeForm = meform;
            SelectMapChipScene = new SelectMapChipScene(selectPicture);
            mps = new MapPalletScene(palletPanel,meform,this);
            
            DXEX.Director.AddSubScene(mps);
            DXEX.Director.AddSubScene(SelectMapChipScene);
            //ドラッグされた時のイベント
            DragEnter += (object sender, DragEventArgs e) =>
            {
                //ファイルがドラッグされている場合、カーソルを変更する
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    e.Effect = DragDropEffects.Copy;
                }
            };
            //ドラッグドロップイベント
            DragDrop += (object o, DragEventArgs e) =>
            {
                //ドロップされたファイルの一覧を取得
                string[] fileName = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                if (fileName.Length <= 0)
                {
                    return;
                }
                for (int i = 0; i < fileName.Length; i++)
                {
                    mps.AddMapChip(fileName[i]);
                }
            };
            //フォームを閉じるのを無効化する
            FormClosing += (o, e) =>
            {
                e.Cancel = true;
            };

            vScrollBar1.SmallChange = 40;
            vScrollBar1.LargeChange = 40;
            vScrollBar1.Scroll += (o, e)=>{
                vScrollBar1.Focus();
            };
            vScrollBar1.Maximum = 50 * 40 - palletPanel.Size.Height;
            vScrollBar1.ValueChanged += (o, e) =>
            {
                mps.LocalPosY = -vScrollBar1.Value;
            };
        }
    }
}

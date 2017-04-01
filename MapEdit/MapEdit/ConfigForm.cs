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
        private MapWriteScene mapWriteScene;
        private Timer timer=new Timer();
        private bool runFlag=false;

        //初期化
        public ConfigForm(MapWriteScene mapWriteScene)
        {
            InitializeComponent();
            this.mapWriteScene = mapWriteScene;
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
            //マップチップ、マップサイズの値をテキストボックスにセットする
            mapChipSizeTextBox.Text = this.mapWriteScene.MapChipSize.ToString();
            mapSizeXtextBox.Text = this.mapWriteScene.MapSize.Width.ToString();
            mapSizeYtextBox.Text = this.mapWriteScene.MapSize.Height.ToString();
        }

        //更新ボタンが押された時,マップチップ、マップサイズの値が変更されてたら、
        //変更を反映する。
        private void updateButton_Click(object sender, EventArgs e)
        {
            if (mapWriteScene.MapChipSize != int.Parse(mapChipSizeTextBox.Text))
            {
                mapWriteScene.MapChipSize = int.Parse(mapChipSizeTextBox.Text);
            }
            if (mapWriteScene.MapSize.Width != int.Parse(mapSizeXtextBox.Text)||
                mapWriteScene.MapSize.Height != int.Parse(mapSizeYtextBox.Text))
            {
                mapWriteScene.MapSize = new Size(
                    int.Parse(mapSizeXtextBox.Text),
                    int.Parse(mapSizeYtextBox.Text)
                );
            }

            //設定ウインドウを閉じる。
            Dispose();
        }
    }
}
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
    public partial class SelectImageForm : Form
    {


        private MapPalletScene mapPalletScene;

        private SelectMapChipScene selectMapChipScene;

        public MapChip GetSelectMapChip() { return selectMapChipScene.MapChip; }

        //右回転ボタンを押したときの処理
        private void rotateRightButton_Click(object sender, EventArgs e)
        {
            selectMapChipScene.MapChip.Angle+=90;
        }

        //左回転ボタンを押したときの処理
        private void rotateLeftButton_Click(object sender, EventArgs e)
        {
            selectMapChipScene.MapChip.Angle += 270;
        }

        //上下反転ボタンを押したときの処理
        private void turnVerticalButton_Click(object sender, EventArgs e)
        {
            selectMapChipScene.MapChip.TurnVertical();
        }

        //左右反転ボタンを押したときの処理
        private void turnHorizontalButton_Click(object sender, EventArgs e)
        {
            selectMapChipScene.MapChip.TurnHorizontal();
        }

        public SelectImageForm()
        {
            
            InitializeComponent();
            selectMapChipScene = new SelectMapChipScene(selectPicture);
            mapPalletScene = new MapPalletScene(palletPanel,selectMapChipScene);
            
            DXEX.Director.AddSubScene(mapPalletScene);
            DXEX.Director.AddSubScene(selectMapChipScene);
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
                    mapPalletScene.AddMapChip(fileName[i]);
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
                mapPalletScene.LocalPosY = -vScrollBar1.Value;
            };
        }
    }
}

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

        private readonly MapPalletScene mps;

        private readonly SelectMapChipScene sms;

        public MapChip GetSelectMapChip() { return sms.MapChip; }


        //右回転ボタンを押したときの処理
        private void rotateRightButton_Click(object sender, EventArgs e)
        {
            sms.MapChip.Angle+=90;
            vScrollBar1.Focus();
        }

        //左回転ボタンを押したときの処理
        private void rotateLeftButton_Click(object sender, EventArgs e)
        {
            sms.MapChip.Angle += 270;
            vScrollBar1.Focus();
        }

        //上下反転ボタンを押したときの処理
        private void turnVerticalButton_Click(object sender, EventArgs e)
        {
            sms.MapChip.TurnVertical();
            vScrollBar1.Focus();
        }

        //左右反転ボタンを押したときの処理
        private void turnHorizontalButton_Click(object sender, EventArgs e)
        {
            sms.MapChip.TurnHorizontal();
            vScrollBar1.Focus();
        }

        public void LoadProject()
        {
            mps.LoadProject();
            sms.resetMapChip();
        }

        /* コンストラクタ */
        //  :イベントの設定(ドラッグ時とか)
        //  :マップのパレットと，選択中マップチップのやつ
        public SelectImageForm(MapEditForm meform)
        {
            
            InitializeComponent();
            sms = new SelectMapChipScene(selectPicture);
            mps = new MapPalletScene(palletPanel,meform,sms);
            
            DXEX.DirectorForForm.AddSubScene(mps);
            DXEX.DirectorForForm.AddSubScene(sms);
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

        //アクティブになったら、スクロールバーをスクロールできるようにする
        private void SelectImageForm_Activated(object sender, EventArgs e)
        {
            vScrollBar1.Focus();
        }

        // 通行判定編集モード
        private void button2_Click(object sender, EventArgs e)
        {
            MapChipConfig.isPassEditMode++;
            MapChipConfig.isPassEditMode %= 2;

            string str = "編集中";
            if (MapChipConfig.isPassEditMode == 0)
            {
                str = "通行判定"; // 編集中でない = FALSE
                mps.SetDrawMapChipInfo(true); // 通行判定チップ表示する
            }
            else if (MapChipConfig.isPassEditMode == 1)
            {
                mps.SetDrawMapChipInfo(true); // 通行判定チップ表示しない
            }

            PassEditMode.Text = str;
        }
    }
}

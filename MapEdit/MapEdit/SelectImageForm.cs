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
    //小っちゃい方のフォーム
    public partial class SelectImageForm : Form
    {

        private readonly MapPalletScene mps;

        private readonly SelectMapChipScene sms;

        public MapChip GetSelectMapChip() { return sms.MapChip; }




        //右回転ボタンを押したときの処理
        private void rotateRightButton_Click(object sender, EventArgs e)
        {
            vScrollBar1.Focus();
            sms.DoAction(SelectMapChipScene.ActionKind.rotateRight);
        }
        //左回転ボタンを押したときの処理
        private void rotateLeftButton_Click(object sender, EventArgs e)
        {
            vScrollBar1.Focus();
            sms.DoAction(SelectMapChipScene.ActionKind.rotateLeft);
        }

        //上下反転ボタンを押したときの処理
        private void turnVerticalButton_Click(object sender, EventArgs e)
        {
            vScrollBar1.Focus();
            sms.DoAction(SelectMapChipScene.ActionKind.TurnVertical);
        }

        //左右反転ボタンを押したときの処理
        private void turnHorizontalButton_Click(object sender, EventArgs e)
        {
            vScrollBar1.Focus();
            sms.DoAction(SelectMapChipScene.ActionKind.TurnHorizontal);
        }


        public void LoadProject(MapChipResourceManager mcrm)
        {
            mps.LoadProject(mcrm);
            sms.resetMapChip();
        }


        /* コンストラクタ */
        //  :イベントの設定(ドラッグ時とか)
        //  :マップのパレットと，選択中マップチップのやつ
        public SelectImageForm(MapChipResourceManager mcrm)
        {

            InitializeComponent();
            sms = new SelectMapChipScene(selectPicture);
            mps = new MapPalletScene(palletPanel, mcrm, sms);

            DXEX.DirectorForForm.AddSubScene(mps);
            DXEX.DirectorForForm.AddSubScene(sms);

            vScrollBar1.SmallChange = 40;
            vScrollBar1.LargeChange = 40;

            vScrollBar1.Scroll += (o, e)
                => vScrollBar1.Focus();

            vScrollBar1.Maximum = 50 * 40 - palletPanel.Size.Height;

            vScrollBar1.ValueChanged +=
                (o, e) => mps.LocalPosY = -vScrollBar1.Value;
        }

        //フォームを閉じるのを無効化する
        protected override void OnFormClosing(FormClosingEventArgs e)
            => e.Cancel = true;


        //ドラッグされた時のイベント
        protected override void OnDragEnter(DragEventArgs e)
        {
            //ファイルがドラッグされている場合、カーソルを変更する
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }


        //ドラッグドロップイベント
        protected override void OnDragDrop(DragEventArgs e)
        {
            //ドロップされたファイルの一覧を取得
            string[] fileName = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (fileName.Length <= 0)
                return;
            for (int i = 0; i < fileName.Length; i++)
                mps.AddMapChip(fileName[i]);
        }


        //アクティブになったら、スクロールバーをスクロールできるようにする
        private void SelectImageForm_Activated(object sender, EventArgs e)
            =>vScrollBar1.Focus();

        // 通行判定編集モード
        private void PassEditMode_Click(object sender, EventArgs e)
        {
            MapChipConfig.isPassEditMode++;
            MapChipConfig.isPassEditMode %= 2;

            if (MapChipConfig.isPassEditMode == 0)
            {
                PassEditMode.Text = "通行判定"; // 編集中でない = FALSE
                mps.SetDrawMapChipInfo(true); // 通行判定チップ表示する
            }
            else if (MapChipConfig.isPassEditMode == 1)
            {
                PassEditMode.Text = "編集中";
                mps.SetDrawMapChipInfo(false); // 通行判定チップ表示しない
            }
        }
    }
}

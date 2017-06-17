using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MapEdit
{
    //マップチップをマウスクリックで書き換えるクラス
    class EditMapChip
    {
        private readonly MapDataControl mapData;
        private readonly SelectImageForm sif;
        //現在のレイヤー
        private readonly ComboBox layerComboBox;
        private int CurrentLayer { get { return layerComboBox.SelectedIndex; } }
        public EditMapChip(MapDataControl mapData,SelectImageForm sif,ComboBox layerComboBox)
        {
            this.layerComboBox = layerComboBox;
            this.sif = sif;
            this.mapData = mapData;
        }
        //マウスでマップを書く処理
        public void MouseAction(Point point)
        {
            //マップサイズ範囲外なら終了
            if (point.X >= mapData.MapSizeX || point.Y >= mapData.MapSizeY ||
                point.X < 0 || point.Y < 0) return;

            //マップ処理
            if ((Control.MouseButtons & MouseButtons.Left)
                == MouseButtons.Left)
            {
                //左クリックされている時の処理
                //マップを書く
                mapData[point.X, point.Y].PutImage(sif.GetSelectMapChip(), CurrentLayer);
            }
            if ((Control.MouseButtons & MouseButtons.Right)
                == MouseButtons.Right)
            {
                //右クリックされている時の処理
                //マップをクリアします
                mapData[point.X, point.Y].ClearImage(CurrentLayer);
            }
        }
    }
}

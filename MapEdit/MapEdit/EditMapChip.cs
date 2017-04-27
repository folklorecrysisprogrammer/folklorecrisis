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
        private readonly MapData mapData;
        private readonly SelectImageForm sif;
        private readonly MapWriteScene mws;
        //現在のレイヤー
        private readonly ComboBox layerComboBox;
        private int CurrentLayer { get { return layerComboBox.SelectedIndex; } }
        public EditMapChip(MapData mapData,SelectImageForm sif,MapWriteScene mws,ComboBox layerComboBox)
        {
            this.mws = mws;
            this.layerComboBox = layerComboBox;
            this.sif = sif;
            this.mapData = mapData;
        }
        //マウスでマップを書く処理
        public void MouseAction(MouseEventArgs e)
        {
            Point point = mws.LocationToMap(e.Location, mapData.MapChipSize);
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

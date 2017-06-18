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
    public class EditMapChipController:MapDataController
    {
        public EditMapChipController(MapData mapData)
        :base(mapData){}

        //指定のマスのマップチップをwriteMapChipで置き換える
        public void EditWrite(Point point,MapChip writeMapChip,int currentLayer)
        {
            //マップサイズ範囲外なら終了
            if (point.X >= mapData.list.GetLength(0) || point.Y >= mapData.list.GetLength(1) ||
                point.X < 0 || point.Y < 0) return;
            //マップチップ置き換え
            mapData.list[point.X, point.Y].PutImage(writeMapChip, currentLayer);
        }

        //指定のマスのマップチップを空にする
        public void EditErase(Point point,int currentLayer)
        {
            //マップサイズ範囲外なら終了
            if (point.X >= mapData.list.GetLength(0) || point.Y >= mapData.list.GetLength(1) ||
                point.X < 0 || point.Y < 0) return;
            //マップチップを空にします
            mapData.list[point.X, point.Y].ClearImage(currentLayer);
        }
    }
}

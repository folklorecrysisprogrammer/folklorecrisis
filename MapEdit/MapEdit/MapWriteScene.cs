using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace MapEdit
{
    //マップを表示するシーン
    public class MapWriteScene : MapSceneBase
    {
        //マップのグリッド
        private readonly MapGrid mapGrid;

        //初期化                                 //描画先をmwpにする
        public MapWriteScene(Control control,int mapChipSize) : base(control)
        {
            mapGrid = new MapGrid(this,mapChipSize);
            AddChild(mapGrid,1);;
            localPos.SetVect(0, 0);
        }

    }
}

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
        public MapGrid MapGrid { get; }

        //初期化                                 //描画先をmwpにする
        public MapWriteScene(Control control,int mapChipSize) : base(control)
        {
            MapGrid = new MapGrid(this,mapChipSize);
            AddChild(MapGrid,1);;
            localPos.SetVect(0, 0);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace MapEdit
{
    //マップに線を引く
   public class MapGrid:DXEX.Node
    {
        private static List<MapGrid> mapGridList=new List<MapGrid>();
        private readonly MapSceneBase msb;
        private readonly uint color=DX.GetColor(0,0,0);
        private readonly int mapChipSize;
        //グリッドを描画するかをオンオフする
        public static void GridOnOf()
        {
            if (mapGridList[0].DrawFlag)
            {
                mapGridList.ForEach((x) => { x.DrawDisable(); });
            }
            else
            {
                mapGridList.ForEach((x) => { x.DrawEnable(); });
            }
        }
        //コンストラクタ
        public MapGrid(MapSceneBase msb,int mapChipSize)
        {
            this.msb = msb;
            this.mapChipSize = mapChipSize;
            
        }
        
        protected override void Attach()
        {
            mapGridList.Add(this);
        }

        protected override void Detach()
        {
            mapGridList.Remove(this);
        }

        //グリッド描画
        public override void Draw()
        {
            int fixX = (int)msb.LocalPosX % mapChipSize;
            int fixY = (int)msb.LocalPosY % mapChipSize;
            for(int x = 0; x < msb.control.Size.Width + mapChipSize; x+= mapChipSize)
            {
                DX.DrawLine(x+fixX, 0, x+fixX, msb.control.Size.Height,color);
            }
            for (int y = 0; y < msb.control.Size.Height+mapChipSize; y += mapChipSize)
            {
                DX.DrawLine(0,y+fixY, msb.control.Size.Width,y+fixY, color);
            }
        }
    }
}

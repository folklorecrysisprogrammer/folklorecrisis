using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace MapEdit
{
    //マップに線を引く
    class MapGrid:DXEX.Node
    {
        private readonly MapWriteScene mws;
        private readonly uint color=DX.GetColor(0,0,0);
        public MapGrid(MapWriteScene mws)
        {
            this.mws = mws;
        }

        public override void Draw()
        {
            int fixX = (int)mws.LocalPosX % mws.MapData.MapChipSize;
            int fixY = (int)mws.LocalPosY % mws.MapData.MapChipSize;
            for(int x = 0; x < mws.control.Size.Width; x+= mws.MapData.MapChipSize)
            {
                DX.DrawLine(x+fixX, 0, x+fixX, mws.control.Size.Height,color);
            }
            for (int y = 0; y < mws.control.Size.Height; y += mws.MapData.MapChipSize)
            {
                DX.DrawLine(0,y+fixY, mws.control.Size.Width,y+fixY, color);
            }
        }
    }
}

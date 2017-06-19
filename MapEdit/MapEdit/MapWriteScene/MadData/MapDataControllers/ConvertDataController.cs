using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MapEdit
{
    //mapData.listから他のデータ形式に変換するクラス
    public class ConvertDataController:MapDataController
    {
        public ConvertDataController(MapData mapData)
            : base(mapData) { }

        //マップ全体をBitmapに変換する
        public Bitmap GetBitmap(MapChipResourceManager mcrm)
        {
            Bitmap unitedImg = new Bitmap(mapData.MapChipSize * mapData.MapSizeX, mapData.MapChipSize * mapData.MapSizeY);
            Graphics g = Graphics.FromImage(unitedImg);
            for (int countY = 0; countY < mapData.MapSizeY; ++countY)
            {
                for (int countX = 0; countX < mapData.MapSizeX; ++countX)
                {
                    Bitmap bitmap = mapData.List[countX, countY].GetBitmap(mcrm);
                    g.DrawImage(bitmap, mapData.MapChipSize * countX, mapData.MapChipSize * countY);
                }
            }
            return unitedImg;
        }

        // mapData.listの情報を文字列として書き出す
        public StringBuilder GetMapDataText()
        {
            StringBuilder mapDataText = new StringBuilder();
            mapDataText.Append(mapData.MapChipSize + "," + mapData.MapSizeX + "," + mapData.MapSizeY);
            for (int y = 0; y < mapData.MapSizeY; y++)
            {
                mapDataText.Append(Environment.NewLine);
                for (int x = 0; x < mapData.MapSizeX; x++)
                {
                    MapOneMass mom = mapData.List[x, y];
                    MapChip[] ChipId = mom.mapChips;
                    for (int i = 0; i < ChipId.Length; i++)
                    {
                        int Id = ChipId[i].Id;
                        int Angle = (int)(ChipId[i].Angle / 90.0);
                        int turnFlag = ChipId[i].turnFlag;
                        mapDataText.Append(Id + "," + Angle + "," + turnFlag + ",");
                    }
                }
            }
            return mapDataText;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace MapEdit
{
    //マップ情報をtxtから取得するクラス
    //こいつにカプセル化の概念は必要ない。
    //ただのデータをまとめただけのものだ
    public class MapInfoFromText
    {
        public int MapChipSize { get; }
        public Size MapSize { get; }
        public int[] Id { get; }
        public int[] Angle { get; }
        public int[] Turn { get; }
        public int LastId { get; }
        public MapInfoFromText(StreamReader sr,int lastId)
        {
            LastId = lastId;
            string[] data=sr.ReadLine().Split(',');
            MapChipSize =int.Parse(data[0]);
            MapSize = new Size(int.Parse(data[1]), int.Parse(data[2]));
            Id = new int[MapSize.Height * MapSize.Width * MapEditForm.maxLayer];
            Angle = new int[MapSize.Height * MapSize.Width * MapEditForm.maxLayer];
            Turn = new int[MapSize.Height * MapSize.Width * MapEditForm.maxLayer];
            int count=0;
            while (sr.Peek() > -1)
            {
                data = sr.ReadLine().Split(',');
                for(int i = 0; i < data.Length-1; i += 3)
                {
                    Id[count] = int.Parse(data[i]);
                    Angle[count] = int.Parse(data[i+1])*90;
                    Turn[count] = int.Parse(data[i+2]);
                    count++;
                }
            }

        }
    }
}

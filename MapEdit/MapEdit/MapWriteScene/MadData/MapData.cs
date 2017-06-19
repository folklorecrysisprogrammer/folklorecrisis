using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEdit
{
    //マップの基本的な情報をまとめたクラス
    public class MapData
    {
        public MapData(int mapChipSize,MapOneMass[,] list)
        {
            this.list = list;
            MapChipSize = mapChipSize;
        }
        //マップチップサイズ
        public int MapChipSize { get; }
        //マップのマスごとの情報を保持
        private MapOneMass[,] list;

        public MapOneMass[,] List{
            get { return list; }
            set { list = value; ChangeList(); }
        }

        //Listが変更された時に呼ばれるイベント
        public event Action ChangeList;

        //配列の要素数の簡易取得
        public int MapSizeX { get { return list.GetLength(0); } }
        public int MapSizeY { get { return list.GetLength(1); } }
    }
}

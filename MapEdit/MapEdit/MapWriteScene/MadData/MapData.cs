using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEdit
{
    ///<summary>
    ///マップの基本的な情報をまとめたクラス
    ///</summary>
    public class MapData
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="mapChipSize">マップチップサイズ</param>
        /// <param name="list">マップチップ配列</param>
        public MapData(int mapChipSize,MapOneMass[,] list)
        {
            this.list = list;
            MapChipSize = mapChipSize;
        }
        ///<summary>
        ///マップチップサイズ
        ///</summary>
        public int MapChipSize { get; }

        /// <summary>
        ///マップのマスごとの情報を保持
        /// </summary>
        private MapOneMass[,] list;

        /// <summary>
        /// listのゲッターセッター
        /// </summary>
        public MapOneMass[,] List{
            get { return list; }
            set { list = value; ChangeList(); }
        }

        
        /// <summary>
        ///Listが変更された時に呼ばれるイベント
        /// </summary>
        public event Action ChangeList;

        
        /// <summary>
        ///配列の要素数X取得 
        /// </summary>
        public int MapSizeX { get { return list.GetLength(0); } }
        /// <summary>
        ///配列の要素数Y取得 
        /// </summary>
        public int MapSizeY { get { return list.GetLength(1); } }
    }
}

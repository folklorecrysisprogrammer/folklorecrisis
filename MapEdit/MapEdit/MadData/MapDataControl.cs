using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MapEdit
{
    //MapWriteSceneに配置するマップチップを統合的に管理するクラス
    public class MapDataControl
    {
        //マップのマスごとの情報を保持
        private MapData mapData;
        //マップチップサイズ
        public int MapChipSize { get { return mapData.MapChipSize; } }
        //マップチップの横の数と縦の数
        public Size MapSize
        {
            get { return new Size(MapSizeX,MapSizeY); }
            set { ChangeMapSize.Change(value.Width, value.Height); }
        }
        public int MapSizeX { get { return mapData.MapSizeX; } }
        public int MapSizeY { get { return mapData.MapSizeY; }}

        //mapData.listを操作する各種コントローラ
        public MapShowAreaController MapShowArea { get; }
        public EditMapChipController EditMapChip { get; }
        public TurnController Turn { get; }
        public ChangeMapSizeController ChangeMapSize { get; }
        public MapChipIdController MapChipId { get; }
        public ConvertDataController ConvertData { get; }
        public LoadMapDataListController LoadMapDataList { get; } 

        //コンストラクタ
        public MapDataControl(Size mapSize,int mapChipSize)
        {
            //mapData.list初期化
            mapData = new MapData(mapChipSize);
            mapData.list = new MapOneMass[mapSize.Width, mapSize.Height];
            for (int x = 0; x < MapSizeX; x++)
            {
                for (int y = 0; y < MapSizeY; y++)
                {
                    mapData.list[x, y] = new MapOneMass(mapChipSize);
                    mapData.list[x, y].LocalPos = new DXEX.Vect(x * MapChipSize, y * MapChipSize);
                }
            }

            //コントローラ初期化
            MapShowArea = new MapShowAreaController(mapData);
            EditMapChip = new EditMapChipController(mapData);
            Turn = new TurnController(mapData);
            ChangeMapSize = new ChangeMapSizeController(mapData);
            MapChipId = new MapChipIdController(mapData);
            ConvertData = new ConvertDataController(mapData);
            LoadMapDataList = new LoadMapDataListController(mapData);
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// (SelectImageFormとMapEditFormで使いまわすことを目標に)
namespace MapEdit.MapChipConfig
{
    /*
     *  マップチップのコンフィグ情報の保持・表示
     *  必要な個数だけ MapChipConfig を持つ
     */
    class MapChipConfigManager
    {
        /*
         * 変数
         */
        // エディットモードの種類
        // (複数シーンで共有する変数なのでstatic変数に≒enum は無理)
        public static bool IsPassEditMode { get; private set; }

        MapChipConfig[,] mcc; // マップチップの個数と同じだけ必要


        /*
         * 関数
         */
         public MapChipConfigManager( int x, int y)
        {
            // SelectImageForm で使う ⇒ ( 6,50 ) らしい
            mcc = new MapChipConfig[x, y];
        }

        //エディットモードを変更するインタフェースの提供
        public static bool ChangePassEditMode()
        {
            return ( IsPassEditMode = !IsPassEditMode );
        }

        // クリック時などに，マップチップの設定情報を更新
        public void UpdateMapChipConfigData()
        {

        }

        // MapChipConfigの情報をスプライトへと反映
        // ( レイヤーが1つ / レイヤーが3つの差を吸収しないといけない )
        public void UpdateSprite() {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEdit 
{
    public class MapChipConfigSprite : DXEX.Node
    {
        private DXEX.Sprite sp;

        /* MapChipConfig の情報を受けてスプライトを設定 */
        public MapChipConfigSprite()
        {
            // 未指定の場合は，通行可能状態( 〇 )
            // 情報をもとに表示する内容を決定する
            string cp = System.IO.Directory.GetCurrentDirectory();
            cp += "\\Image\\ChipInfo1.png"; // 通行可能スプライト仮置き
            sp = new DXEX.Sprite(cp);
            AddChild(sp);

            Console.Write("OK");
        }
    }
}

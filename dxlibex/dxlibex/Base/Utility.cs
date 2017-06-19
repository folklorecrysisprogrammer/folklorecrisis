using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXEX.Base
{
    //何かと役立つ関数特集
   static public class Utility
    {
        //度数法を弧度法に変換
        static public double DegToRad(double deg)
        {
            return deg / 180 * Math.PI;
        }
        //弧度法を度数法に変換
        static public double RadToDeg(double rad)
        {
            return rad * 180 / Math.PI;
        }

        //Rule生成関数
        public static Rule<Key>Input<Key>(Key key)
        {
            return new Rule<Key>(key);
        }

    }
}

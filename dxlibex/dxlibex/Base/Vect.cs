using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXEX.Base
{
    //ベクトルクラス
    public struct Vect
    {
        //コンストラクタ
        public Vect(double _x, double _y) { x = _x; y = _y; }

        //X成分、Y成分セット
        public void SetVect(double _x, double _y) { x = _x; y = _y; }

        //X成分
        public double x;

        //Y成分
        public double y;

        //演算子オーバーロード
        public static Vect operator +(Vect left, Vect right)
        {
            return new Vect(left.x + right.x, left.y + right.y);
        }
        public static Vect operator *(double scale, Vect v)
        {
            return new Vect(v.x * scale, v.y * scale);
        }
        public static Vect operator *(Vect v, double scale)
        {
            return new Vect(v.x * scale, v.y * scale);
        }
        public static Vect operator -(Vect left, Vect right)
        {
            return new Vect(left.x - right.x, left.y - right.y);
        }

        //ベクトルの大きさを返す
        public double Size()
        {
            return Math.Sqrt(x * x + y * y);
        }

        //内積
        public double Dot(Vect v)
        {
            return x * v.x + y * v.y;
        }   

        //外積
        public double Cross(Vect v)
        {
            return x * v.y -v.x *y;
        }

        /*任意の座標を中心として回転させる（回転中心座標,回転角度（度数法））
         * この時ベクトルは位置ベクトルと考える*/
        public Vect RotationTo(Vect cv, double angle)
        {
            Vect vv;
            vv.x = Math.Cos(Utility.DegToRad(angle)) * (x - cv.x) + Math.Sin(Utility.DegToRad(angle)) * (cv.y - y) + cv.x;
            vv.y = Math.Cos(Utility.DegToRad(angle)) * (y - cv.y) + Math.Sin(Utility.DegToRad(angle)) * (x - cv.x) + cv.y;
            return vv;
        }
        //X成分だけを返す版
        public double RotationToX(Vect cv, double angle)
        {
            double _x;
            _x = Math.Cos(Utility.DegToRad(angle)) * (x - cv.x) + Math.Sin(Utility.DegToRad(angle)) * (cv.y - y) + cv.x;
            return _x;
        }
        //Y成分だけを返す版
        public double RotationToY(Vect cv, double angle)
        {
            double _y;
            _y = Math.Cos(Utility.DegToRad(angle)) * (y - cv.y) + Math.Sin(Utility.DegToRad(angle)) * (x - cv.x) + cv.y;
            return _y;
        }
        //角度から単位ベクトルを生成(度数法)
        static public Vect MakeAngle(double angle)
        {
            return new Vect(Math.Cos(Utility.DegToRad(angle)), Math.Sin(Utility.DegToRad(angle)));
        }
    }
}

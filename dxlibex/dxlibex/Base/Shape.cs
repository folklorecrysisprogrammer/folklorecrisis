using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;


//まるで未完成
namespace DXEX
{
    //物の形を表す基底クラス
    public abstract class Shape
    {
        public Node node;
        public Shape(Node _node) { node = _node; }
        public abstract void DebugDraw();
        public abstract bool CheckHit(Shape shape);
        public abstract bool CheckHit(Rect shape);
        public abstract bool CheckHit(Point shape);
    }

    //長方形のクラス
   public class Rect:Shape
    {
        //四角形の縦幅横幅
        public Size size;
        //四角形の縦幅横幅一時保存用
        private Size tsize;
        public Rect(Node _node,Size _size) : base(_node) { size = _size; }
        //四角形の四隅の座標
        private Vect[] corner = new Vect[4];
        public override void DebugDraw()
        {
            DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, 200);
            SetCorner();
            DX.DrawQuadrangle(
                (int)corner[0].x, (int)corner[0].y,
                (int)corner[1].x, (int)corner[1].y,
                (int)corner[2].x, (int)corner[2].y,
                (int)corner[3].x, (int)corner[3].y,DX.GetColor(255,20,20),DX.TRUE);   
        }
        //cornerを計算で求める
        private void SetCorner()
        {
            tsize.Width = size.Width * node.Scale.x;
            tsize.Height = size.Height * node.Scale.y;
            corner[0].SetVect(
                node.GlobalPos.x - (tsize.Width * node.anchor.x), 
                node.GlobalPos.y - (tsize.Height * node.anchor.y)
            );
            corner[1].SetVect(corner[0].x + tsize.Width, corner[0].y);
            corner[2].SetVect(corner[1].x, corner[1].y + tsize.Height);
            corner[3].SetVect(corner[0].x , corner[2].y);
            corner[0] = corner[0].RotationTo(node.GlobalPos, node.GlobalAngle);
            corner[1] = corner[1].RotationTo(node.GlobalPos, node.GlobalAngle);
            corner[2] = corner[2].RotationTo(node.GlobalPos, node.GlobalAngle);
            corner[3]=corner[3].RotationTo(node.GlobalPos, node.GlobalAngle);
        }
        public override bool CheckHit(Rect rect) { return false; }
        public override bool CheckHit(Shape shape) {return shape.CheckHit(this); }
        public override bool CheckHit(Point point) {
            SetCorner();
            for(int i = 0; i < 4; i++)
            {
                if(
                    (corner[(i+1)%4] - corner[i]).Cross(point.Position - corner[i])<=0
                   )
                {
                    return false;
                }
            }
            return true;
        }
    }

    //点のクラス
   public class Point : Shape
    {
        public Point(Node _node) : base(_node) { }
        public Vect Position { get { return node.GlobalPos; } }
        public override void DebugDraw()
        { }
        public override bool CheckHit(Point point) { return false; }
        public override bool CheckHit(Shape shape) { return shape.CheckHit(this); }
        public override bool CheckHit(Rect rect)
        {
            return rect.CheckHit(this);
        }
    }


    //縦横のサイズを保存するクラス
    public struct Size
    {
        private Vect size;
        public Size(double x,double y)
        {
            size=new Vect(x, y);
        }
        public Size(Vect _size)
        {
            size = _size;
        }
        //横幅ゲッターセッター
        public double Width { get { return size.x; } set { size.x = value; } }
        //縦幅ゲッターセッター
        public double Height { get { return size.y; } set { size.y = value; } }
        //演算子オーバーロード
        public static Size operator +(Size left, Size right)
        {
            return new Size(left.size+right.size);
        }
        public static Size operator -(Size left, Size right)
        {
            return new Size(left.size - right.size);
        }
        public static Size operator *(double scale, Size v)
        {
            return new Size(v.size * scale);
        }
        public static Size operator *(Size v, double scale)
        {
            return new Size(v.size * scale);
        }
    }
}

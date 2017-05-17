using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;


//まるで未完成
namespace DXEX.Base
{
    //物の形を表す基底クラス
    public abstract class Shape
    {
        public Node node;
        public Shape(Node _node) { node = _node; }
        [System.Diagnostics.Conditional("DEBUG")]
        public abstract void DebugDraw();
        public abstract bool CheckHit(Shape shape);
        public abstract bool CheckHit(Rect shape);
        public abstract bool CheckHit(Point shape);
    }

    //長方形のクラス
   public class Rect:Shape
    {
        //四角形の縦幅横幅
        public Vect size;
        //四角形の縦幅横幅一時保存用
        private Vect tsize;
        public Rect(Node _node,Vect _size) : base(_node) { size = _size; }
        //四角形の四隅の座標
        private Vect[] corner = new Vect[4];
        public sealed override void DebugDraw()
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
            tsize.x = size.x * node.Scale.x;
            tsize.y = size.y * node.Scale.y;
            corner[0].SetVect(
                node.GlobalPos.x - (tsize.x * node.anchor.x), 
                node.GlobalPos.y - (tsize.y * node.anchor.y)
            );
            corner[1].SetVect(corner[0].x + tsize.x, corner[0].y);
            corner[2].SetVect(corner[1].x, corner[1].y + tsize.y);
            corner[3].SetVect(corner[0].x , corner[2].y);
            corner[0] = corner[0].RotationTo(node.GlobalPos, node.GlobalAngle);
            corner[1] = corner[1].RotationTo(node.GlobalPos, node.GlobalAngle);
            corner[2] = corner[2].RotationTo(node.GlobalPos, node.GlobalAngle);
            corner[3]=corner[3].RotationTo(node.GlobalPos, node.GlobalAngle);
        }
        public sealed override bool CheckHit(Rect rect) { return false; }
        public sealed override bool CheckHit(Shape shape) {return shape.CheckHit(this); }
        public sealed override bool CheckHit(Point point) {
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
        public sealed override void DebugDraw()
     { }
        public sealed override bool CheckHit(Point point) { return false; }
        public sealed override bool CheckHit(Shape shape) { return shape.CheckHit(this); }
        public sealed override bool CheckHit(Rect rect)
        {
            return rect.CheckHit(this);
        }
    }
}

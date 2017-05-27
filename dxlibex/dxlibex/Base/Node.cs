using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DXEX.Base
{
    
    /*GameObjectクラスを親子関係を設定できるように拡張したクラス
     * 毎フレームの処理は親ノードから先に呼ばれる
     * そのあと子ノードはレイヤーが低い順から呼ばれる
     * 終了処理はその逆順で呼ばれる
     * 子は一つしか親を持てない
     * 親は複数の子を持てる
     * 子の管理はChildListクラスに任せる*/


  //親子関係が設定可能なクラス
  public partial class Node : GameObject
    {
        //レイヤー
        private int layer;
        public int Layer
        {
            get { return layer; }
        }

        //親Node
        private Node parent = null;
        public Node Parent
        {
            get { return parent; }
        }

        //グローバル座標取得
        public Vect GlobalPos{
            get
            {
                return ChangeGlobalPos(localPos);
            }
        }

        //ローカル座標をグローバル座標に変換
        public Vect ChangeGlobalPos(Vect localPos)
        {
            if (parent == null) return localPos + offsetPos;
            return
                parent.GlobalPos +
                (localPos.x + offsetPos.x) * XAxes +
                (localPos.y + offsetPos.y) * YAxes;
        }
        //グローバル座標をローカル座標に変換
        public Vect ChangeLocalPos(Vect globalPos)
        {
            Vect localPos= globalPos - GlobalPos;
            return localPos.x*XAxes+localPos.y * YAxes;
        }

        //グローバル角度取得
        public double GlobalAngle
        {
            get
            {
                if (parent == null) return angle;
                return parent.GlobalAngle+angle;
            }
        }

        //グローバル不透明度取得
        public byte GlobalOpacity
        {
            get
            {
                if (parent == null) return Opacity;
                return (byte)(Opacity-255+parent.GlobalOpacity);
            }
        }
      
        //X軸更新
        private void UpdateXAxes()
        {
            XAxes = Vect.MakeAngle(GlobalAngle);
            foreach (var node in childList.GetList())
            {
                node.UpdateXAxes();
            }
        }
        public override double Angle
        {
            get{return angle;}
            set
            {
                base.Angle = value;
                UpdateXAxes();
            }
        }

        //子Nodeリスト
        private ChildList childList=new ChildList();

        public List<Node> GetAllChildren()
        {
            return childList.GetList();
        }

        //親Nodeをセットする（返り値trueで成功）
        private void SetParent(Node _parent)
        {
            DebugMessage.Mes("Base.Node.SetParent:DisposeされたNodeです", DisposedFlag == true);
            DebugMessage.Mes("Base.Node.SetParent:既に親Nodeが存在しています！", parent != null);
                parent = _parent;
            UpdateXAxes();
        }
        
        //子を追加する
        public void AddChild(Node child,int _layer=0)
        {
            DebugMessage.Mes("Base.Node.AddChild:DisposeされたNodeにAddChildしました", DisposedFlag == true);
            DebugMessage.Mes("Base.Node.AddChild:自身に自身を追加しようとしました！", this== child);
            child.layer = _layer;
            child.SetParent(this);
            childList.AddCheck(new LayerNode(child));
            child.Attach();
        }

        //子を外す
        public void RemoveChild(Node child)
        {
            DebugMessage.Mes("Base.Node.RemoveChild:削除する子Nodeが存在しません！",this!=child.parent);
            child.parent = null;
            childList.RemoveCheck(new LayerNode(child));
            child.Detach();
        }

        //親から自分を取りはずす
        public void RemoveFromParent()
        {
            DebugMessage.Mes("Base.Node.RemoveFromParent:親Nodeが存在しません！", parent == null);
            parent.RemoveChild(this);
        }

        //指定のタグの子Nodeを渡す
     /*   public List<Node> GetChildrenTag(int tag)
        {
            return childList.GetTagList(tag);
        }*/

        //毎ループ呼ばれる処理
        public sealed override void LoopDo()
        {
            base.LoopDo();
            if (DrawFlag == true)
            {
                Draw();
                DebugDraw();
            }
            childList.ChildLoopDo();
        }

        //終了処理
        protected override void Dispose(bool isFinalize)
        {
            childList.DisposeAllChildren();
            if (parent != null) RemoveFromParent();
            base.Dispose(isFinalize);
        }
        //描画有効化
        public sealed override void DrawEnable()
        {
            base.DrawEnable();
            foreach(var node in childList.GetList())
            {
                    node.DrawEnable();
            }
        }
        //描画無効化
        public sealed override void DrawDisable()
        {
            base.DrawDisable();
            foreach (var node in childList.GetList())
            {
                node.DrawDisable();
            }
        }
        //一時停止
        public sealed override void Pause()
        {
            base.Pause();
            foreach (var node in childList.GetList())
            {
                node.Pause();
            }
        }
        //再開
        public sealed override void Resume()
        {
            base.Resume();
            foreach (var node in childList.GetList())
            {
                node.Resume();
            }
        }

        //タグ変更
    /*    private void ChangeTag(int newTag)
        {
            if (parent == null) return;
            parent.childList.ChangeTag(this,newTag);
        }*/
       /* public sealed override int Tag
        {
            get{return base.Tag;}
            set
            {
                ChangeTag(value);
                base.Tag = value;
            }
        }*/
    }
}

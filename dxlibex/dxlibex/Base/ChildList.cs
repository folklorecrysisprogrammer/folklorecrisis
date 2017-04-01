using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXEX
{
    //layerを保存する形式
       struct LayerNode
        {

            public Node node;
            public readonly int layer;
            public LayerNode(Node _node)
            {
                node = _node;
                layer = node.Layer;
            }
        }

    /*Nodeが持つ子Nodeを管理するリスト
     * 子Nodeの検索、追加、削除、LoopDo呼び出し等を行う
     */
    class ChildList :ExclusiveList<SortedDictionary<int, List<Node>>,LayerNode>
    {
    

        //全ての子NodeのLoopDo()を呼ぶ
        public void ChildLoopDo()
        {
            Lock();
            foreach (var list in list.Values)
            {
                foreach (var node in list)
                {
                    node.LoopDo();
                }
            }
            UnLock();
        }

        //全ての子を終了する
        public void DisposeAllChildren()
        {
            Lock();
            foreach (List<Node> list in list.Values)
            {
                foreach(var node in list)
                {
                    node.Dispose();
                }
            }
            UnLock();
        }

        

        //リストにNodeを追加する
        protected override void Add(LayerNode child)
        {
            if (list.ContainsKey(child.layer) == false)
            {
                list.Add(child.layer, new List<Node>());
            }
            list[child.layer].Add(child.node);
        }



        //リストからNodeを削除する
        protected override void Remove(LayerNode child)
        {
            list[child.layer].Remove(child.node);
        }
        

        //全ての子Nodeを渡す
        public  List<Node> GetList() {
            List<Node> mergeList=new List<Node>();
            foreach(var list in list.Values.ToList())
            {
                mergeList.AddRange(list);
            }
            return mergeList;
        }
        /*   //追加、削除の予約を保存する形式
       private struct MoveNode
        {

            public Node node;
            public int layer;
            public enum Mode { add, remove };
            public Mode mode;
            public MoveNode(Node _node, Mode _mode)
            {
                node = _node;
                mode = _mode;
                layer = node.Layer;
            }
        }
        //子Nodeレイヤー別リスト
        //private SortedDictionary<int, List<Node>> childList = new SortedDictionary<int, List<Node>>();
        //子Nodeタグ別リスト
       // private Dictionary<int, List<Node>> childTagList = new Dictionary<int, List<Node>>();*/
        /*
        //指定のタグの子Nodeを渡す
        public List<Node> GetTagList(int tag)
        {
            if (childTagList.ContainsKey(tag) == false) return new List<Node>();
            return childTagList[tag];
        }
        //Nodeのタグ変更   (新しい方のタグを渡す)
        public void ChangeTag(Node node,int newTag)
        {
            DebugMessage.Mes("ChildList.ChangeTag:childTagListに存在しません！", childTagList[node.Tag].IndexOf(node)==-1);
            //タグ別リスト付け替え
            childTagList[node.Tag].Remove(node);
            AddChildTagList(node, newTag);
        }
        public void AddChildTagList(Node node,int tag)
        {
            if (childTagList.ContainsKey(tag) == false)
            {
                childTagList.Add(tag,new List<Node>());
            }
            childTagList[tag].Add(node);
        }
        //childListを列挙しているとき、他の処理がnodeChildListを削除＆追加をしないようにロックする
        int lockCount =0;
        void Lock() { lockCount++; }
        void UnLock() { lockCount--;if(lockCount == 0) Merge(); }
        //リスト移動予約
        private List<MoveNode> moveChild = new List<MoveNode>();*/
        /*  private void AddChild(Node child)
  {
      if (childList.ContainsKey(child.Layer) == false)
      {
          childList.Add(child.Layer, new List<Node>());
      }
      childList[child.Layer].Add(child);
  }*/
        /* private void RemoveChild(MoveNode child)
         {
             childList[child.layer].Remove(child.node);
         }*/
        /*  //リストに追加するNodeを予約する
          public void AddChildCheck(Node child)
          {
              AddChildTagList(child, child.Tag);
              if (lockCount == 0) { AddChild(child); }
              else { moveChild.Add(new MoveNode(child, MoveNode.Mode.add)); }
          }

          //リストから削除するNodeを予約する
          public void RemoveChildCheck(Node child)
          {
              childTagList[child.Tag].Remove(child);
              if (lockCount == 0) { RemoveChild(child); }
              else { moveChild.Add(new MoveNode(child, MoveNode.Mode.remove)); }
          }

          //予約してた、追加、削除を実行
          private void Merge() {
              foreach(var mnode in moveChild)
              {
                  if (mnode.mode == MoveNode.Mode.add)
                  {
                      AddChild(mnode);
                  }
                  else
                  {
                      RemoveChild(mnode);
                  }

              }
              moveChild.Clear();//予約リストを空に
          }*/
    }
}

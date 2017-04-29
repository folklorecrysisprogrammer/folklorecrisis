using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXEX.Base
{
    //排他制御可能なList
    abstract class ExclusiveList<ListType, ItemType>
        where ListType : new()
    {
        //予約するのに使う保存形式
        private struct MoveItem
        {
            public ItemType item;
            //追加予約か、削除予約かを判別する用
            public enum Mode { add, remove };
            public Mode mode;
            public MoveItem(ItemType _item, Mode _mode)
            {
                item = _item;
                mode = _mode;
            }
        }
        //排他制御の対象のリスト
        protected ListType list = new ListType();

        //リスト移動予約
        private List<MoveItem> moveItemList = new List<MoveItem>();

        //要素を回してる間に削除＆追加をしないように排他制御する----------------------------------
        int lockCount = 0;
        public void Lock() { lockCount++; }
        public void UnLock() { lockCount--; if (lockCount == 0) Merge(); }
        //--------------------------------------------------------------------------

        //リストに追加する
        protected abstract void Add(ItemType item);
        //リストから削除する
        protected abstract void Remove(ItemType item);

        //リストに追加するNodeを予約する
        public void AddCheck(ItemType item)
        {
            if (lockCount == 0) { Add(item); }
            else { moveItemList.Add(new MoveItem(item, MoveItem.Mode.add)); }
        }

        //リストから削除するNodeを予約する
        public void RemoveCheck(ItemType item)
        {
            if (lockCount == 0) { Remove(item); }
            else { moveItemList.Add(new MoveItem(item, MoveItem.Mode.remove)); }
        }

        //予約してた、追加、削除を実行
        private void Merge()
        {
            foreach (var item in moveItemList)
            {
                if (item.mode == MoveItem.Mode.add)
                {
                    Add(item.item);
                }
                else
                {
                    Remove(item.item);
                }

            }
            moveItemList.Clear();//予約リストを空に
        }
    }
}

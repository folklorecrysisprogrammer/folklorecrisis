using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEdit
{
    //Idを管理する神
    class IdManager
    {
        private List<WriteId> idList = new List<WriteId>();

        //新しいIDを生成
        public Id AddId()
        {
            var writeId = new WriteId(idList.Count);
            idList.Add(writeId);
            return writeId;
        }
        //Idゲット
        public Id GetId(int idValue)
        {
            return idList[idValue];
        }

        //Idを削除
        public void RemoveId(int idValue)
        {
            //Id終了処理
            idList[idValue].Dispose();
            //末尾の要素でないなら、末尾の要素と入れ替え
            if (idValue != idList.Count - 1)
            {
                idList[idValue] = idList.Last();
                idList[idValue].SetId(idValue);
            }
            //末尾要素削除
            idList.RemoveAt(idList.Count - 1);
        }

        //Idを入れ替え
        public void SwapId(int idValue1, int idValue2)
        {
            var temp = idList[idValue1];
            idList[idValue1] = idList[idValue2];
            idList[idValue2] = temp;
            idList[idValue1].SetId(idValue1);
            idList[idValue2].SetId(idValue2);
        }
    }
}

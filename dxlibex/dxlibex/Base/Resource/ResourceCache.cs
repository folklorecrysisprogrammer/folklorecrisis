using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXEX.Base
{
    public abstract class ResourceCache<ResourceCoreT,ResourceDataT>
        where ResourceCoreT:ResourceCore<ResourceDataT>
    {
        //ResourceCoreキャッシュList
        private Dictionary<string, ResourceCoreT> ResourceCoreList
            = new Dictionary<string, ResourceCoreT>();

        //新しいリソースコアを登録
        protected void AddNewResourceCore(string key,ResourceCoreT resourceCore)
        {
            ResourceCoreList.Add(key, resourceCore);
        }

        //リソースコアを返す
        protected ResourceCoreT GetResourceCore(string key)
        {
            if (!ResourceCoreList.ContainsKey(key)) throw new Exception("登録されてないキーダゾ");
            return ResourceCoreList[key];
        }
        //キーが存在するかcheck
        protected bool isKey(string key) {
            return ResourceCoreList.ContainsKey(key);
        } 


        //使用していないリソースを解放
        public void NotUsingResourceDelete()
        {
            var removeKeys = new List<string>();
            foreach (var key in ResourceCoreList.Keys)
            {
                if (ResourceCoreList[key].NotUsingFree())
                {
                    removeKeys.Add(key);
                }
            }
            foreach (var key in removeKeys)
            {
                ResourceCoreList.Remove(key);
            }

        }

        //全てのリソースを解放
        public void AllResourceDelete()
        {
            foreach (var key in ResourceCoreList.Keys)
            {
                ResourceCoreList[key].ResourceFree();
            }
            ResourceCoreList.Clear();
        }
    }
}

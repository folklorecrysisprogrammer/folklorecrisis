using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXEX.Base
{
    //何らかのResourceCoreのResourceDataを提供するクラス
    public abstract class ResourceProvider<ResourceCoreT,ResourceDataT,SubClassT>
        where ResourceCoreT:ResourceCore<ResourceDataT>
        where SubClassT:ResourceProvider<ResourceCoreT,ResourceDataT,SubClassT>
    {
        internal protected ResourceCoreT resourceCore;
        //resourceCoreの参照数を1増やす
        internal protected ResourceProvider(ResourceCoreT resourceCore)
        {
            this.resourceCore = resourceCore;
            resourceCore.Retain();
        }

        //複製のインターフェイス
        public abstract SubClassT Clone();

        public void Dispose()
        {
            Dispose(false);

        }

        private void Dispose(bool isFinalize)
        {
            if (resourceCore == null) throw new Exception("ResourceProviderはすでにDisposeされています");
            //resourceCoreの参照数を1減らす
            resourceCore.Release();
            resourceCore = null;
            //デストラクタを呼ばないようにする
            if (!isFinalize) GC.SuppressFinalize(this);
        }

        //デストラクタ
        ~ResourceProvider()
        {
            Dispose(true);
        }
    }

    //何らかのリソースの参照数の管理とリソースの開放を担うクラス
    public abstract class ResourceCore<ResourceDataT>
    {
        //何らかのリソース
       internal  ResourceDataT resourceData { get; private set; }
        //参照カウンタ
        private int refCount = 0;

        //誰にも参照されてないならリソース開放する
        internal bool NotUsingFree() {
            if(refCount == 0)
            {
                ResourceFree();
                return true;
            }
            return false;
        }
        //参照を1増やす
        internal void Retain()
        {
            refCount++;
        }
        //参照を1減らす
        internal void Release()
        {
            if (refCount == 0) throw new Exception("ふつうここは呼ばれないはずなんだが");
            refCount--;
        }
        //コンストラクタ
        internal protected ResourceCore(ResourceDataT resourceData)
        {
            this.resourceData = resourceData;
        }

        //リソース開放（オーバーライドしてください）
        internal abstract void ResourceFree();
    }
}

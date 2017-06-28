using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEdit
{
    public class Id
    {
        protected int id;

        //Disposeイベント
        public event Action Disposed;
        //Disposeイベント発火
        protected void DoDisposed() { Disposed(); }
        public int value {
            get { return id; }
            protected set {id = value;}

        }
        protected Id(int id) {
            this.id=id;
        }
    }

    //id書き変え機能を持ったIdクラス
    class WriteId: Id,IDisposable
    {
        public void SetId(int id)
            =>this.id = id;

        //Disposeイベント発火
        public void Dispose()
            =>DoDisposed();

        public WriteId(int id) : base(id) { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXEX
{
    class Component<OwnerType>:Coroutine
        where OwnerType: GameObject
    {
        protected OwnerType owner;
        public void Attach(OwnerType _owner)
        {
            owner = _owner;
            owner.AddComponent(this);
        }

    }
}

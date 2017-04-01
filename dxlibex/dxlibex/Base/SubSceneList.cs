using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXEX
{
    class SubSceneList:ExclusiveList<List<Scene>,Scene>
    {
        public List<Scene> GetList { get { return list; } }

        protected override void Add(Scene item)
        {
            list.Add(item);
        }

        protected override void Remove(Scene item)
        {
            list.Remove(item);
        }
    }
}

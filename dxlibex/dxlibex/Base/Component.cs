﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXEX.Base
{
    //コンポーネントクラス
    //GameObjectを継承したクラスに取り付けて機能拡張する。
    //動的な性質なのでプログラムの柔軟性が上がるメリットがある
    //OwnerTypeを指定することで取り付け対象の型をある程度制限できる
    public class Component<OwnerType>:Coroutine
        where OwnerType: GameObject
    {
        //取り付け対象
        protected OwnerType owner;
        //取り付ける
        public void Attach(OwnerType _owner)
        {
            if (owner != null) throw new Exception("既にAttachされています");
            owner = _owner;
            owner.AddComponent(this);
        }

    }
}

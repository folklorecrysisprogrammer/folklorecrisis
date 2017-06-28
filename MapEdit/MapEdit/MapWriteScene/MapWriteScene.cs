using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System;

namespace MapEdit
{
    //マップを表示するシーン
    public class MapWriteScene : MapSceneBase
    {
        public event Action UpdateLocalPosEvent;

        //初期化                                 //描画先をmwpにする
        public MapWriteScene(Control control,int mapChipSize) : base(control)
        {  
            AddChild(new MapGrid(this,mapChipSize),1);
            LocalPos=new DXEX.Vect(0, 0);
            DXEX.DirectorForForm.AddSubScene(this);
        }

        //シーンの座標が更新されたら呼ばれる処理
        protected override void UpdateLocalPos()
        {
            UpdateLocalPosEvent?.Invoke();
        }

        protected override void Dispose(bool isFinalize)
        {
            DXEX.DirectorForForm.RemoveSubScene(this);
            base.Dispose(isFinalize);
        }
    }
}

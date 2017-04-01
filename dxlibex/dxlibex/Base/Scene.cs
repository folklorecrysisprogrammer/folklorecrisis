using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DxLibDLL;

namespace DXEX
{
    public class Scene : Node
    {

        public Control control = null;
        public Control GetControl { get { return control; } }

        //画面横サイズ
        private int windowX;
        public int WindowX
        {
            get
            {
                if (control == null)
                {
                    DX.GetWindowSize(out windowX, out windowY);
                    return windowX;
                }
                return control.ClientSize.Width;
            }
        }

        //画面縦サイズ
        private int windowY;
        public int WindowY
        {
            get
            {
                if (control == null)
                {
                    DX.GetWindowSize(out windowX, out windowY);
                    return windowY;
                }
                return control.ClientSize.Height;
            }
        }

        //初期化
        public Scene()
        {
            //windowの中心に座標をセット
            localPos.SetVect(WindowX / 2.0, WindowY / 2.0);
        }

        //描画先Controlを指定して初期化
        //(フォームと連携する場合は必ずこちらを使ってください)
        public Scene(Control control)
        {
            this.control=control;
            //Controlの中心に座標をセット
            localPos.SetVect(WindowX / 2.0, WindowY / 2.0);
            //Controlが破棄されたら、sceneを削除する
            this.control.Disposed += (object o, EventArgs e) => { Director.RemoveSubScene(this); };
        }

        //描画処理
        public sealed override void Draw()
        {
            base.Draw();
        }
    }
}

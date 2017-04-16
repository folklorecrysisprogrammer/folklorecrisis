using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace DXEX
{
    /*ゲームで使用するオブジェクトの基底クラス
     * 毎フレーム行う処理を追加したいならUpdate（）をオーバーライド
    */
    public partial class GameObject:Coroutine,IDisposable
    {
        //コンポーネントリスト
        List<Coroutine> components = new List<Coroutine>();

        //コンポーネント追加
       public void AddComponent(Coroutine component)
        {
            components.Add(component);
        }

        //タグ
        private int tag = 0;
        public virtual int Tag{ get { return tag; }set { tag = value; } }

        //ローカル座標
        protected Vect localPos;
        public Vect LocalPos
        {
            get { return localPos; }
            set { localPos = value; UpdateLocalPos(); }
        }
        public double LocalPosX
        {
            get { return localPos.x; }
            set { localPos.x = value; UpdateLocalPos(); }
        }
        public double LocalPosY
        {
            get { return localPos.y; }
            set { localPos.y = value; UpdateLocalPos(); }
        }

        //オフセット座標
        public Vect offsetPos;

        //X軸、Y軸
        private Vect xAxes=new Vect(1,0);
        public Vect XAxes { get { return xAxes; }set { xAxes = value; } }
        public Vect YAxes { get { return new Vect(-xAxes.y, xAxes.x); } }

        //基準点座標(0～1で指定。0.5で中心)
        public Vect anchor = new Vect(0, 0);

        //回転角度(度数法)
        protected double angle=0;
        public virtual double Angle
        {
            get { return angle; }
            set { angle = value; UpdateAngle(); }
        }

        //拡大率(1.0で等倍)
        protected Vect scale=new Vect(1.0,1.0);
        public Vect Scale
        {
            get { return scale; }
            set { scale = value; UpdateScale(); }
        }
        public double ScaleX
        {
            get { return scale.x; }
            set { scale.x = value; UpdateScale(); }
        }
        public double ScaleY
        {
            get { return scale.y; }
            set { scale.y = value; UpdateScale(); }
        }

        //不透明度（0～255）
        private byte opacity =255;
        public virtual byte Opacity { get { return opacity; } set { opacity = value; } }

        //反転描画のフラグ
        public int turnFlag=DX.FALSE;

        //描画するかどうかのフラグ
        private bool drawFlag=true;
        public bool DrawFlag { get { return drawFlag;} }

        //描画を有効化
        public virtual void DrawEnable() { drawFlag = true; }
        //描画を無効化
        public virtual void DrawDisable() { drawFlag = false; }

        /*一時停止について
         * コルーチン、コンポネントが一時停止対象
         * 描画は停止されない*/

        //一時停止フラグ
        private bool pauseFlag = false;
        //一時停止
        public virtual void Pause(){ pauseFlag = true; }
        //再開
        public virtual void Resume() { pauseFlag = false; }

        //毎ループ呼ばれる処理
        public virtual void LoopDo()
        {
            //コルーチン&コンポーネント実行
            if (pauseFlag == false && disposedFlag == false) {
                foreach(var component in components)
                {
                    component.UpdateDo();
                }
                UpdateDo();
            }
        }

        //Dispose()が呼ばれたかどうかのフラグ
        private bool disposedFlag = false;
        public bool DisposedFlag { get { return disposedFlag; } }

        //終了処理（公開部分）
        public  void Dispose()
        {
            Dispose(false);
            disposedFlag = true;
        }

        //終了処理（内部）、継承用
        protected virtual void Dispose(bool isFinalize)
        {
            DebugMessage.Mes("GameObject.Dispose():すでにDisposeされています", disposedFlag == true);
            DrawDisable();
            //デストラクタを呼ばないようにする
            if (!isFinalize)GC.SuppressFinalize(this);
        }

        //デストラクタ
        ~GameObject() { Dispose(true); }

        public virtual void Draw() { }
    }
}

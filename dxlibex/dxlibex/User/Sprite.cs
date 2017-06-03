using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using DXEX.Base;

namespace DXEX.User
{  
    //画面に画像を表示するオブジェクト
    public class Sprite:Node
    {
        //テクスチャークラス参照
        private ITexture texture=null;
        //グラフィックハンドル（テクスチャークラスから読み出す。ぶっちゃけいらないかも。直接テクスチャークラスから毎回取ればいいだけだし）
        private int gh;
        //テクスチャーの縦幅横幅
        private Vect rect;
        public Vect Rect{ get{ return rect; } }

        //グラフィックハンドルをセット
        public void SetTexture(ITexture iTexture)
        {
            texture = iTexture;
            gh = iTexture.Gh;
            int width,height;
            DX.GetGraphSize(gh, out width, out height);
            rect.x = width;
            rect.y = height;
            UpdateTexture();
        }
        public void SetTexture(string filePath)
        {
            texture = Director.TextureCache.GetTexture(filePath).GetITexture();
            gh = texture.Gh;
            int width, height;
            DX.GetGraphSize(gh, out width, out height);
            rect.x = width;
            rect.y = height;
            UpdateTexture();
        }

        //テクスチャーゲット
        public ITexture GetTexture()
        {
            return texture;
        }

        //テクスチャーをクリア
        public void ClearTexture()
        {
            texture = null;
            gh = -1;
            UpdateTexture();
        }

        //コンストラクタ-
        public Sprite(string filePath)
        {

            SetTexture(filePath);            
        }
        public Sprite(ITexture tex)
        {
            SetTexture(tex);
        }
        public Sprite(){}

        //描画処理
        public sealed override void Draw(){
            
            DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA,GlobalOpacity);
            DX.DrawRotaGraph3((int)(GlobalPos.x), (int)(GlobalPos.y),
                              (int)(rect.x*anchor.x), (int)(rect.y * anchor.y),
                              scale.x, scale.y,Utility.DegToRad(GlobalAngle), gh,DX.TRUE, turnFlag) ;
        }

        //終了処理
        protected override void Dispose(bool isFinalize)
        {
           // if (texture != null) texture.Dispose();
            gh = -1;
            base.Dispose(isFinalize);
        }

        //Textureが変更された時に呼ばれる処理ユーザーが好きにオーバーライドしてください
        protected virtual void UpdateTexture(){}
    }
}

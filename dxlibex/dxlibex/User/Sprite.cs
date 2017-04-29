using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace DXEX
{
   
    //画面に画像を表示するオブジェクト
    public class Sprite:Node
    {
        //テクスチャークラス参照
        private Texture texture=null;
        //グラフィックハンドル（テクスチャークラスから読み出す。ぶっちゃけいらないかも。直接テクスチャークラスから毎回取ればいいだけだし）
        private int gh;
        //テクスチャーの縦幅横幅
        private Size rect;
        public Size Rect{ get{ return rect; } }

        //グラフィックハンドルをセット
        public void SetTexture(Texture _texture)
        {
            texture = _texture;
            gh = _texture.Gh;
            int width,height;
            DX.GetGraphSize(gh, out width, out height);
            rect.Width = width;
            rect.Height = height;
            UpdateTexture();
        }
        public void SetTexture(string filePath)
        {
           // if (texture != null) texture.Dispose();
            texture = TextureCache.GetTexture(filePath);
            gh = texture.Gh;
            int width, height;
            DX.GetGraphSize(gh, out width, out height);
            rect.Width = width;
            rect.Height = height;
            UpdateTexture();
        }

        //テクスチャーゲット
        public Texture GetTexture()
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
        public Sprite(Texture tex)
        {
            SetTexture(tex);
        }
        public Sprite(){}

        //描画処理
        public sealed override void Draw(){
            
            DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA,GlobalOpacity);
            DX.DrawRotaGraph3((int)(GlobalPos.x), (int)(GlobalPos.y),
                              (int)(rect.Width*anchor.x), (int)(rect.Height * anchor.y),
                              scale.x, scale.y,Utility.DegToRad(GlobalAngle), gh,DX.TRUE, turnFlag) ;
            base.Draw();
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

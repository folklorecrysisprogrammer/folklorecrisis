using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using DXEX.Base;


/*
// BGM
// BGS(複数鳴るBGM)
// SE
// ME(SEと同じ扱い)
// VOICE
*/ 
namespace DXEX.User
{
    // BGMやSEを鳴らすオブジェクト
    public class SoundPlayer : Node
    {
        private Sound s = null; // サウンドクラス
        private int soundHandle = -1; // サウンドハンドル
    	private SOUNDTYPE soundType; // サウンドタイプ
        public enum PLAYTYPE
        {
            NORMAL = DX.DX_PLAYTYPE_NORMAL, // (処理が再生終了まで止まるので基本使わない)
            BACK = DX.DX_PLAYTYPE_BACK,
            LOOP = DX.DX_PLAYTYPE_LOOP,
        };
        public enum SOUNDTYPE
        {
            BGM, BGS, SE
        };

        public int PanVal { get; private set; }
        public int Playtype { get; set; }
        public float Volume { get; private set; }

        private float vVolume;
        private int remainFrame;

        //コンストラクタ(ファイルパスを受け取る)
        public SoundPlayer(string filePath, SOUNDTYPE st)
        {
            s = Director.SoundCache.GetSound(filePath);
            soundHandle = s.Gh;
            ChangePan(0); // パンの初期値は0
            ChangeVolume(255); // ボリュームは最大値

            soundType = st; // BGMか否か
            Playtype = (int)PLAYTYPE.BACK;
            if ( soundType != SOUNDTYPE.SE ) Playtype = (int)PLAYTYPE.LOOP;
        }

        // 所持サウンドハンドルの楽曲を再生(初めから再生)
        public void PlaySound()
        {
            if (soundType == SOUNDTYPE.BGM || soundType == SOUNDTYPE.BGS ) PlayBGM();
            else PlaySE();
        }
        private void PlayBGM()
        {
            if (DX.CheckSoundMem(soundHandle) == 0)
                DX.PlaySoundMem(soundHandle, (int)Playtype, DX.TRUE);
        }
        private void PlaySE()
        {
            DX.PlaySoundMem(soundHandle, (int)Playtype, DX.TRUE);
        }
        // 所持サウンドハンドルの楽曲を再生(続きから再生)
        public void ContinueSound()
        {
            if ( soundType != SOUNDTYPE.SE && DX.CheckSoundMem(soundHandle) == 0)
                DX.PlaySoundMem(soundHandle, (int)Playtype, DX.FALSE);
        }
        // 所持サウンドハンドルの楽曲を停止
        public void StopSound()
        {
            // ( StopSoundMemを使うと停止位置が保持される )
            if ( soundType != SOUNDTYPE.SE || DX.CheckSoundMem(soundHandle) == 1)
                DX.StopSoundMem(soundHandle);
        }
        // 現在、曲を再生中？
        public bool IsPlaySound() { return (DX.CheckSoundMem(soundHandle) == 1); }

        // パンの値を設定(-255～255)
        public void ChangePan(int panPal)
        {
            if (panPal > 255) panPal = 255;
            else if (panPal < -255) panPal = -255;
            PanVal = panPal;
            DX.ChangePanSoundMem(panPal, soundHandle);
        }

        // ボリュームの設定(0～255)
        public void ChangeVolume(float vol)
        {
            if (vol > 255) vol = 255;
            else if (vol < 0) vol = 0;
            Volume = vol;
            DX.ChangeVolumeSoundMem((int)vol, soundHandle);
        }

        // ループ開始位置を設定(mSec)
        public void SetLoopPosMillSec(int millSce) {
            DX.SetLoopPosSoundMem(millSce, soundHandle);
        }

        // 再生位置を取得(バイト単位で取得する)
        public int GetSoundCurrentPosition() {
            return DX.GetSoundCurrentPosition(soundHandle);
        }

        // フェードイン
        public void FadeinSound( int frame, int volume )
        {
            remainFrame = frame;
            vVolume = (float)volume / (float)frame;
            ChangeVolume(0);
            PlaySound();
        }

        // フェードアウト
        public void FadeoutSound( int frame )
        {
            remainFrame = frame;
            vVolume = -1 * Volume / frame;
        }

        // 更新処理
        public sealed override void Update()
        {
            if (remainFrame > 0)
            {
                Volume += vVolume;
                remainFrame--;
                ChangeVolume(Volume);
            }
        }

        //終了処理
        protected override void Dispose(bool isFinalize)
        {
            // 明示的に破棄するか否か
            // (そもそもサウンドのコピーは渡さない)
            // (呼ぶとソフト終了時にエラー吐く)
            // if (s != null) s.Dispose();

            base.Dispose(isFinalize);
        }
    }
}
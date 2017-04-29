using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DXEX.Base;
using DXEX.User;
using DxLibDLL;

class ShowSound : Node
{
    // サウンドファイルをソフトサウンドハンドルとして読み込み
    int SoundHandle;
    int SoftSoundHandle;
    float[] ParamList = new float[16384];
    int SamplePos;
    int i;
    Letter letter;


    public ShowSound() {
        // ソフトウェアサウンドで読み込み
        SoftSoundHandle = DX.LoadSoftSound("for_victory.mp3");

        // サウンドデータに変換
        SoundHandle = DX.LoadSoundMemFromSoftSound(SoftSoundHandle);
        DX.PlaySoundMem(SoundHandle, DX.DX_PLAYTYPE_LOOP);

        // テキストデータの生成
        letter = new Letter();
        letter.Color = DX.GetColor(127, 127, 127);
        letter.LocalPos = new Vect(400,400);
        AddChild(letter);
    }

    public override IEnumerator Update()
    {   
        // 現在の再生位置を取得
        SamplePos = DX.GetCurrentPositionSoundMem(SoundHandle);
        letter.Text = "" + SamplePos;

        // 現在の再生位置から 4096 サンプルを使用して周波数分布を得る
        DX.GetFFTVibrationSoftSound(SoftSoundHandle, -1, SamplePos, 16384, out ParamList[0], 16384);

        yield break;
    }


    public override void Draw()
    {
        int x = -1, j = 0;

        // 周波数分布を画面に描画する
        for (i = 0; i < 16384; i++)
        {
            if ((int)(Math.Log((double)i) * 10) != x)
            {
                j++;
                x = (int)(Math.Log((double)i) * 10);

                double param;

                // 関数から取得できる値を描画に適した値に調整
                param = Math.Pow(ParamList[i], 0.5) * 4.0;
                // 縦線を描画
                DX.DrawBox(j * 20, 600 - (int)(param * 600), j * 20 + 4, 600, DX.GetColor(255, 255, 0), DX.TRUE);
        }
    }
    }
}

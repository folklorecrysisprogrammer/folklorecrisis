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
    const int bitsize = 12; // 12で4096。これでだいたい0.1秒
    const int BufferLength = 1 << bitsize; // FFTで用いる
    // const int StackLength = (int)(0.1 * 44100.0); // X秒間分の波を書きこんでおく
    const int StackLength = BufferLength;

    int SoftSoundSampleNum;
    int SoftSoundHandle;
    int SoftSoundPlayerHandle;
    double[] ParamList = new double[BufferLength];
    double[] ParamList2 = new double[BufferLength];
    int[] Wave = new int[StackLength];
    int[] Wave2 = new int[StackLength];
    int[] ZeroWave = new int[StackLength];
    int[] WaveIm = new int[StackLength];
    int i;
    Letter letter;

    public ShowSound()
    {
        // ソフトウェアサウンドで読み込み
        SoftSoundHandle = DX.LoadSoftSound("ori_bt1.mp3");
        SoftSoundPlayerHandle = DX.MakeSoftSoundPlayer(SoftSoundHandle);
        SoftSoundSampleNum = DX.GetSoftSoundSampleNum(SoftSoundHandle);

        // テキストを表示 
        makeText();
        letter.Text = "" + SoftSoundSampleNum;

        // ソフトウェアサウンドの音声データを再生
        DX.StartSoftSoundPlayer(SoftSoundPlayerHandle);
    }

    public override IEnumerator IeUpdate()
    {
        // http://dxlib.o.oo7.jp/cgi/patiobbs/patio.cgi?mode=past&no=1800        
        int OutputSampleNum = 0;
        while (true)
        {
            // プレイヤーに再生されずにストックされている波形サンプルの数が
            // 0.1秒分以下になっていたら BGM.wav の全波形サンプルをプレイヤーに出力していない限りループの中に入る
            while (DX.GetStockDataLengthSoftSoundPlayer(SoftSoundPlayerHandle) < Math.Min(StackLength, 4410) && OutputSampleNum < SoftSoundSampleNum)
            {
                // サウンドデータハンドルを介してプレイヤーに出力していない波形サンプルを取得
                for (int i = 0; i < StackLength; i++)
                {
                    DX.ReadSoftSoundData(SoftSoundHandle, OutputSampleNum + i, out Wave[i], out Wave2[i]);
                }

                // ダミー / 0波を作る
                for( int i=0; i<Wave.Length; i++)
                    ZeroWave[i] = 0;

                // エンターキーを押している間だけ，音データを加工する
                if (KeyControl.GiveKey(DX.KEY_INPUT_SPACE) > 0)
                {
                    // 本当はここにフィルターの処理を入れる
                    for (int i = 0; i < StackLength; i++)
                    {
                        // 適当に左右バランスいじってみる
                        // Wave[i] = (int)((double)Wave[i] * 0.05);
                        // Wave2[i] = (int)((double)Wave2[i] * 0.95);

                        // 適当にsin波を放り込んでみる
                        // (波が不連続になっちゃってるんだろうねぇ…)
                        int fs = 44100; // サンプリング周波数
                        int freq = 440; // 周波数(ラー♪)
                        double d = 2 * Math.PI * freq * i  / fs;
                        Wave[i] = (int)(Math.Sin(d) * 32768);
                        Wave2[i] = (int)(Math.Sin(d) * 32768);
                        Wave2[i] = 0;
                    }
                }

                // Waveに対して(左側スピーカ)
                // FFTによりフーリエ変換を行う
                // bitsize : ビット数でサイズを指定。( BufferSizeは2の何乗であるか)
                // Wave2 : 虚数領域。(普通は0を設定)
                int[] a = Wave;
                FFT(Wave, ZeroWave, out ParamList, out ParamList2, bitsize);

                // 適当にフィルターを噛ませます
                // (適当なハイパスフィルタ)
                for (int i=0; i<ParamList.Length/2; i++)
                {
                    ParamList[i] /= 2;
                    ParamList2[i] /= 2;
                }

                // IFFTで逆フーリエ変換を行う
                IFFT(ParamList, ParamList2, out Wave, out WaveIm, bitsize, Wave.Length);
                int[] b = Wave;
                int[] c = a;


                // Wave2に対して(右側スピーカ)
                // FFTによりフーリエ変換を行う
                // bitsize : ビット数でサイズを指定。( BufferSizeは2の何乗であるか)
                // Wave2 : 虚数領域。(普通は0を設定)
                FFT(Wave2, ZeroWave, out ParamList, out ParamList2, bitsize);

                // 適当にフィルターを噛ませます
                // (適当なハイパスフィルタ)
                for (int i = 0; i < ParamList.Length / 2; i++)
                {
                    ParamList[i] = 0;
                    ParamList2[i] = 0;
                }

                // IFFTで逆フーリエ変換を行う
                IFFT(ParamList, ParamList2, out Wave2, out WaveIm, bitsize, Wave.Length);

                // 取得した波形サンプルをプレイヤーに出力
                for (int i = 0; i < StackLength; i++)
                {
                    DX.AddOneDataSoftSoundPlayer(SoftSoundPlayerHandle, Wave[i], Wave2[i]);
                }

                // プレイヤーに出力したサンプルの数をインクリメント
                OutputSampleNum += StackLength;
            }

            // まだプレイヤーの再生処理を開始していなかったら開始する
            if (DX.CheckStartSoftSoundPlayer(SoftSoundPlayerHandle) == DX.FALSE)
                DX.StartSoftSoundPlayer(SoftSoundPlayerHandle);

            // BGM.wav の全ての波形サンプルをプレイヤーに出力していて、
            // 且つプレイヤーが無音データを再生し始めていたら BGM.wav の再生が終了したということなのでループを抜ける
            if (OutputSampleNum == SoftSoundSampleNum && DX.CheckSoftSoundPlayerNoneData(SoftSoundPlayerHandle) == DX.TRUE)
                break;

            yield return 0;
        }

        yield break;
    }

    public override void Draw()
    {
        drawSpectrum();
        // drawWaveform();
    }

    // Xサンプルの平均を取ってる(飛ばし表示だけにすべきかも)
    private void drawWaveform()
    {
    }

    private void drawSpectrum()
    {
        // ( MAX 値いくつじゃい？ )
        // double[] data = ParamList;

        int x = -1, j = 0;

        // 周波数分布を画面に描画する
        for (i = 0; i < BufferLength; i++)
        {
            if ((int)(Math.Log((double)i) * 10) != x)
            {
                j++;
                x = (int)(Math.Log((double)i) * 10);

                double param;

                // 関数から取得できる値を描画に適した値に調整
                // param = Math.Pow(ParamList[i], 0.1) * 4.0; // DXLIBのやつ
                int normalize = 10000; // いくつだろうね.
                param = ParamList2[i] / normalize;
                param *= ParamList[i] / normalize; // (実数部と虚数部から大きさを求める…)
                param = Math.Abs(param);

                // 縦線を描画
                DX.DrawBox(j * 5, 400 - (int)(param * 300), j * 5 + 2, 400, DX.GetColor(255, 255, 0), DX.TRUE);
            }
        }
    }

// 4.3のfft.h(c言語)の内容を元に作成
public static void FFT(
    int[] inR,
    int[] inI,
    out double[] outputRe,
    out double[] outputIm,
    int length)
    {
        int i, j, k, n, m, r, stage, number_of_stage;
        double a_real, a_imag, b_real, b_imag, c_real, c_imag, real, imag;

        number_of_stage = length; // 呼び出し時の値
        int dataSize = 1 << number_of_stage; // データ数
        int N = inR.Length;

        outputRe = new double[N];
        outputIm = new double[N];

        // バタフライ計算
        for (stage = 1; stage <= number_of_stage; stage++)
        {
            for (i = 0; i < Math.Pow(2,stage - 1); i++)
            {
                for (j = 0; j < Math.Pow(2,number_of_stage - stage); j++)
                {
                    n = (int)Math.Pow(2,number_of_stage - stage + 1) * i + j;
                    m = (int)Math.Pow(2,number_of_stage - stage) + n;
                    r = (int)Math.Pow(2, stage - 1) * j;
                    a_real = inR[n];
                    a_imag = inI[n];
                    b_real = inR[m];
                    b_imag = inR[m];
                    c_real = Math.Cos((2.0 * Math.PI * r) / N);
                    c_imag = -Math.Sin((2.0 * Math.PI * r) / N);
                    if (stage < number_of_stage)
                    {
                        outputRe[n] = a_real + b_real;
                        outputIm[n] = a_imag + b_imag;
                        outputRe[m] = (a_real - b_real) * c_real - (a_imag - b_imag) * c_imag;
                        outputIm[m] = (a_imag - b_imag) * c_real + (a_real - b_real) * c_imag;
                    }
                    else
                    {
                        outputRe[n] = a_real + b_real;
                        outputIm[n] = a_imag + b_imag;
                        outputRe[m] = a_real - b_real;
                        outputIm[m] = a_imag - b_imag;
                    }
                }
            }
        }

        // インデックスの並び替えのためのテーブルの作成
        int[] index = new int[N];
        for (stage = 1; stage <= number_of_stage; stage++)
        {
            for (i = 0; i < Math.Pow(2,stage - 1); i++)
            {
                index[(int)Math.Pow(2, stage - 1) + i] = 
                    index[i] + (int)Math.Pow(2,number_of_stage - stage);
            }
        }

        // インデックスの並び替え
        for (k = 0; k < N; k++)
        {
            if (index[k] > k)
            {
                real = outputRe[index[k]];
                imag = outputIm[index[k]];
                outputRe[index[k]] = outputRe[k];
                outputIm[index[k]] = outputIm[k];
                outputRe[k] = real;
                outputIm[k] = imag;
            }
        }
    }


    public static void IFFT(
       double[] inR,
       double[] inI,
       out int[] outputRe,
       out int[] outputIm,
       int length,
       int waveLength)
    {
        int number_of_stage = length; // 呼び出し時の値
        int i, j, k, n, m, r, stage;
        double a_real, a_imag, b_real, b_imag, c_real, c_imag, real, imag;
        int N = waveLength; // 出力波のサイズ

        outputRe = new int[N];
        outputIm = new int[N];

        /* バタフライ計算 */
        for (stage = 1; stage <= number_of_stage; stage++)
        {
            for (i = 0; i < Math.Pow(2,stage - 1); i++)
            {
                for (j = 0; j < Math.Pow(2, number_of_stage - stage); j++)
                {
                    n = (int)Math.Pow(2, number_of_stage - stage + 1) * i + j;
                    m = (int)Math.Pow(2, number_of_stage - stage) + n;
                    r = (int)Math.Pow(2, stage - 1) * j;
                    a_real = inR[n];
                    a_imag = inI[n];
                    b_real = inR[m];
                    b_imag = inR[m];
                    c_real = Math.Cos((2.0 * Math.PI * r) / N);
                    c_imag = Math.Sin((2.0 * Math.PI * r) / N);
                    if (stage < number_of_stage)
                    {
                        outputRe[n] = (int)(a_real + b_real);
                        outputIm[n] = (int)(a_imag + b_imag);
                        outputRe[m] = (int)((a_real - b_real) * c_real - (a_imag - b_imag) * c_imag );
                        outputIm[m] = (int)((a_imag - b_imag) * c_real + (a_real - b_real) * c_imag );
                    }
                    else
                    {
                        outputRe[n] = (int)(a_real + b_real);
                        outputIm[n] = (int)(a_imag + b_imag);
                        outputRe[m] = (int)(a_real - b_real);
                        outputIm[m] = (int)(a_imag - b_imag);
                    }
                }
            }
        }

        /* インデックスの並び替えのためのテーブルの作成 */
        int[] index = new int[N];
        for (stage = 1; stage <= number_of_stage; stage++)
        {
            for (i = 0; i < Math.Pow(2,stage - 1); i++)
            {
                index[(int)Math.Pow(2, stage - 1) + i]
                    = index[i] + (int)Math.Pow(2, number_of_stage - stage);
            }
        }

        /* インデックスの並び替え */
        for (k = 0; k < N; k++)
        {
            if (index[k] > k)
            {
                real = outputRe[index[k]];
                imag = outputIm[index[k]];
                outputRe[index[k]] = outputRe[k];
                outputIm[index[k]] = outputIm[k];
                outputRe[k] = (int)real;
                outputIm[k] = (int)imag;
            }
        }

        // 計算結果をNで割る
        for (k = 0; k < N; k++)
        {
            // InR.Length とかで割っちゃうと，音が消滅する
            // ( 元のfft.hの N = 入力配列の要素数 )
            outputRe[k] /= 1;
            outputIm[k] /= 1;

            // 逆フーリエ変換後の虚数領域の扱いどうするんだろ？
            outputRe[k] += outputIm[k];
            outputRe[k] /= 2;
        }

        int[] temp = outputRe;
        int[] temp2 = outputIm;
    }


    // 終了時に呼ぶべき処理(メモ)
    private void End___()
    {
        // ソフトウエアサウンドハンドルを削除
        DX.DeleteSoftSound(SoftSoundHandle);

        // ソフトウエアサウンドプレイヤーハンドルを削除
        DX.DeleteSoftSoundPlayer(SoftSoundPlayerHandle);
    }



    private void makeText()
    {
        // テキストデータの生成
        letter = new Letter();
        letter.Color = DX.GetColor(127, 127, 127);
        letter.LocalPos = new Vect(400, 400);
        AddChild(letter);
    }
}
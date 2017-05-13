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
    int i;
    Letter letter;

    public ShowSound()
    {
        // ソフトウェアサウンドで読み込み
        SoftSoundHandle = DX.LoadSoftSound("for_victory.mp3");
        SoftSoundPlayerHandle = DX.MakeSoftSoundPlayer(SoftSoundHandle);
        SoftSoundSampleNum = DX.GetSoftSoundSampleNum(SoftSoundHandle);

        // テキストを表示 
        makeText();
        letter.Text = "" + SoftSoundSampleNum;

        // ソフトウェアサウンドの音声データを再生
        DX.StartSoftSoundPlayer(SoftSoundPlayerHandle);
    }

    public override IEnumerator Update()
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

                // エンターキーを押している間だけ，音データを加工する
                if (KeyControl.GiveKey(KeyCode.ENTER) > 0)
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

                        /*
                        freq = (int)(freq * 3 / 2); // 周波数(3倍音の半オクターブ下)
                        d = 2 * Math.PI * freq * i / fs;
                        Wave[i] += (int)(Math.Sin(d) * 32768);
                        Wave2[i] += (int)(Math.Sin(d) * 32768);

                        freq = (int)(freq / 3 * 2); // 周波数(1オクターブ上)
                        d = 2 * Math.PI * freq * i / fs;
                        Wave[i] += (int)(Math.Sin(d) * 32768);
                        Wave2[i] += (int)(Math.Sin(d) * 32768);
                        */
                    }
                }

                // FFTによりフーリエ変換を行う
                // bitsize : ビット数でサイズを指定。( BufferSizeは2の何乗であるか)
                FFT(Wave, Wave2, out ParamList, out ParamList2, bitsize);

                // DXLIBのやつ(動かなくなった)
                // 現在の再生位置から 4096 サンプルを使用して周波数分布を得る
                // DX.GetFFTVibrationSoftSound(SoftSoundHandle, -1, OutputSampleNum, BufferLength, out ParamList[0], BufferLength);

                // 取得した波形サンプルをプレイヤーに出力
                for (int i = 0; i < StackLength; i++)
                {
                    DX.AddOneDataSoftSoundPlayer(SoftSoundPlayerHandle, Wave[i], Wave2[i]);
                }

                // プレイヤーに出力したサンプルの数をインクリメント
                OutputSampleNum += StackLength;
            }

            // 現在の再生位置を取得(ソフトサウンドじゃ使えない)
            // SamplePos = DX.GetCurrentPositionSoundMem(SoundHandle);
            // letter.Text = "" + SamplePos;

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
        /*
        int x = -1;
        int ave = 50;
        for( i =0; i < Channel1.Length/ave; i++)
        {
            // 縦線を描画

            
            //ReadSoftSoundData は8bit量子化のサウンドの場合は -128～127 の値が、
            //16bit量子化のサウンドの場合が - 32768～32767 の値が Channel1, Channel2 に代入されます
            float param = 0;
            for (int j = 0; j < ave; j++)
            {
                param += Channel1[i*ave+j];
            }
            param /= ave;
            param /= 32768; // 16bit量子化サウンドを正規化              
            DX.DrawBox( i , 250 - (int)(param * 200), i+1, 250, DX.GetColor(255, 255, 0), DX.TRUE);
        }
        */
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
                int normalize = 1000000; // いくつだろうね.
                param = ParamList2[i] / normalize;
                param *= ParamList[i] / normalize; // (実数部と虚数部から大きさを求める…)
                param = Math.Abs(param);

                // 縦線を描画
                DX.DrawBox(j * 5, 400 - (int)(param * 300), j * 5 + 2, 400, DX.GetColor(255, 255, 0), DX.TRUE);
            }
        }
    }

    private void makeText()
    {
        // テキストデータの生成
        letter = new Letter();
        letter.Color = DX.GetColor(127, 127, 127);
        letter.LocalPos = new Vect(400, 400);
        AddChild(letter);
    }


    // フーリエ変換
    // (拾い物)
    // URL: https://algorithm.joho.info/signal/csharp-fast-fourier-transform/
    public static void FFT(
    int[] inR,
    int[] inI,
    out double[] outputRe,
    out double[] outputIm,
    int bitSize)
    {        
        int dataSize = 1 << bitSize;
        int[] reverseBitArray = BitScrollArray(dataSize);

        outputRe = new double[dataSize];
        outputIm = new double[dataSize];

        // 0埋したサイズを調整した波形データを生成する
        // ( 0埋方法 ) 日本語でおっけー。窓関数使ってないので影響ないと思われ。
        // http://wiener.seigyo.numazu-ct.ac.jp/CreativeDesign/souzou2005/group2/document/Report_PackData.pdf
        int sizeInput = inR.Length;
        int[] inputRe = new int[dataSize];
        int[] inputIm = new int[dataSize];
        for( int i= dataSize-1; i>=0; i--)
        {
            if((sizeInput - 1 - i) < 0)
            {
                inputRe[i] = 0;
                inputIm[i] = 0;
                continue;
            }
            inputRe[i] = inR[sizeInput - 1 - i];
            inputIm[i] = inI[sizeInput - 1 - i];
            int temp = inR[sizeInput - 1 - i];
        }
        int abcdemart = inputRe.Length;

        // バタフライ演算のための置き換え
        for (int i = 0; i < dataSize; i++)
        {
            outputRe[i] = inputRe[reverseBitArray[i]];
            outputIm[i] = inputIm[reverseBitArray[i]];
        }

        // バタフライ演算
        for (int stage = 1; stage <= bitSize; stage++)
        {
            int butterflyDistance = 1 << stage;
            int numType = butterflyDistance >> 1;
            int butterflySize = butterflyDistance >> 1;

            double wRe = 1.0;
            double wIm = 0.0;
            double uRe =
                Math.Cos(Math.PI / butterflySize);
            double uIm =
                -Math.Sin(Math.PI / butterflySize);

            for (int type = 0; type < numType; type++)
            {
                for (int j = type; j < dataSize; j += butterflyDistance)
                {
                    int jp = j + butterflySize;
                    double tempRe =
                        outputRe[jp] * wRe - outputIm[jp] * wIm;
                    double tempIm =
                        outputRe[jp] * wIm + outputIm[jp] * wRe;
                    outputRe[jp] = outputRe[j] - tempRe;
                    outputIm[jp] = outputIm[j] - tempIm;
                    outputRe[j] += tempRe;
                    outputIm[j] += tempIm;
                }
                double tempWRe = wRe * uRe - wIm * uIm;
                double tempWIm = wRe * uIm + wIm * uRe;
                wRe = tempWRe;
                wIm = tempWIm;
            }
        }
    }

    // ビットを左右反転した配列を返す
    private static int[] BitScrollArray(int arraySize)
    {
        int[] reBitArray = new int[arraySize];
        int arraySizeHarf = arraySize >> 1;

        reBitArray[0] = 0;
        for (int i = 1; i < arraySize; i <<= 1)
        {
            for (int j = 0; j < i; j++)
                reBitArray[j + i] = reBitArray[j] + arraySizeHarf;
            arraySizeHarf >>= 1;
        }
        return reBitArray;
    }

    // 終了時に呼ぶべき処理(メモ)
    private void End___()
    {
        // ソフトウエアサウンドハンドルを削除
        DX.DeleteSoftSound(SoftSoundHandle);

        // ソフトウエアサウンドプレイヤーハンドルを削除
        DX.DeleteSoftSoundPlayer(SoftSoundPlayerHandle);
    }
}
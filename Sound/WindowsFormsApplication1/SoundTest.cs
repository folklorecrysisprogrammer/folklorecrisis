using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using DXEX.Base;
using DXEX.User;


class SoundTest : Node
{
    SoundPlayer m,m2,m3;
    Letter l,l2;

    public SoundTest(string str, string str2, string str3)
    {
        int fadeFrame = 600;
        int volume = 127;

        m = new SoundPlayer(str,SoundPlayer.SOUNDTYPE.BGM);
        m.FadeinSound(fadeFrame, volume);
        AddChild(m);

        m2 = new SoundPlayer(str2, SoundPlayer.SOUNDTYPE.BGS);
        m2.ChangeVolume(volume);
        m2.PlaySound();
        m2.FadeoutSound(fadeFrame);
        AddChild(m2);

        m3 = new SoundPlayer(str3, SoundPlayer.SOUNDTYPE.SE);
        AddChild(m3);

        l = new Letter();
        l.LocalPos = new Vect(70, 70);
        AddChild(l);

        l2 = new Letter();
        l2.LocalPos = new Vect(70, 110);
        l2.Text = "0= 一時停止\n 1= パン左へ\n2 = パン右へ\n3 = ボリューム下げる\n4 = ボリューム上げる";
        AddChild(l2);
    }

    public override IEnumerator IeUpdate()
    {
        while (true)
        {
            if (KeyControl.GiveKey(DX.KEY_INPUT_0) == 1)
            {
                if( m.IsPlaySound() )  m.StopSound();
                else m.ContinueSound();
            }
            if (KeyControl.GiveKey(DX.KEY_INPUT_1) >= 1)
            {
                int pan = m.PanVal;
                m.ChangePan(pan-3);
            }
            if (KeyControl.GiveKey(DX.KEY_INPUT_2) >= 1)
            {
                int pan = m.PanVal;
                m.ChangePan(pan+3);
            }
            if (KeyControl.GiveKey(DX.KEY_INPUT_3) >= 1)
            {
                float vol = m.Volume;
                m.ChangeVolume(vol - 3);
            }
            if (KeyControl.GiveKey(DX.KEY_INPUT_4) >= 1)
            {
                float vol = m.Volume;
                m.ChangeVolume(vol + 3);
            }
            if (KeyControl.GiveKey(DX.KEY_INPUT_5) >= 1)
            {
                m3.PlaySound();
            }
            yield return 0;
        }
    }

    public override void Draw()
    {
        int pan = m.PanVal;
        int vol = (int)m.Volume;
        l.Text = "(bgm1)パン= " + pan + "   ボリューム = " + vol + "\n";

        int pan2 = m2.PanVal;
        int vol2 = (int)m2.Volume;
        l.Text += "(bgm2)パン= " + pan2 + "   ボリューム = " + vol2;
    }
}

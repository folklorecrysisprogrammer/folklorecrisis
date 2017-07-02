using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapEdit
{
    //プルヌ制御
    class Purunu
    {
        private Timer timer = new Timer();
        private bool runFlag = false;
        //プルヌとして表示するPictureBoxを指定
        public Purunu(PictureBox chara)
        {
            var myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            chara.Image = new Bitmap(myAssembly.GetManifestResourceStream("MapEdit.Resources.プルヌ.png"));
            chara.MouseEnter += (o, e) =>
            {
                runFlag = true;
            };
            timer.Interval = 1;
            timer.Start();
            timer.Tick += (o, e) =>
            {
                if (runFlag)
                {
                    chara.Location = new Point(chara.Location.X + 3, chara.Location.Y);
                }
            };
        }
    }
}

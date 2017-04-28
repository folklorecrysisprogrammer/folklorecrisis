using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing;

namespace MapEdit
{
    //プロジェクトを保存するのに必要なリソースをまとめたクラス
    public class ProjectInfo
    {
        //マップシート
        public Bitmap BitmapSheet {get; }
        public int LastId { get; }
        public StringBuilder MapDataText { get; }
        public ProjectInfo(Bitmap bitmapSheet,int lastId,StringBuilder mapDataText)
        {
            this.BitmapSheet = bitmapSheet;
            this.LastId = lastId;
            this.MapDataText = mapDataText;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;

namespace MapEdit
{
    //プロジェクトデータを保存,上書き,開く機能をするクラス
    public class ProjectManager
    {
        private readonly MapEditForm meForm;

        //開いているプロジェクトへのパス
        private string currentProjectPath;

        public ProjectManager(MapEditForm meForm)
        {
            this.meForm = meForm;
        }

        //プロジェクトをロードする（引数のパスはプロジェクト名を含むとこまで）
        public bool LoadProject(string path)
        {
            if (Directory.Exists(path) == false) return false;
            Bitmap bitmap = (Bitmap)Image.FromFile(path + @"MapChip.png");
            StreamReader sr=new StreamReader(
                    path + @"\MapChip.txt",
                    Encoding.GetEncoding("shift_jis"));
            int lastId=int.Parse(sr.ReadLine());
            meForm.mcrm.LoadBitmapSheet(lastId, bitmap);
            return true;
        }

        public bool SaveNewProject(string path,string projectName)
        {
            currentProjectPath = path + @"\" + projectName;
            if (Directory.Exists(path) == false) return false;
            Directory.CreateDirectory(currentProjectPath);
            var bitmap = meForm.mcrm.GetBitmapSheet();
            if (bitmap != null)
            {
                // 画像生成
                bitmap.Save(currentProjectPath + @"\MapChip.png", ImageFormat.Png);
                // テキストファイル生成
                StreamWriter sw = new StreamWriter(
                    currentProjectPath + @"\MapChip.txt",
                    false,
                    Encoding.GetEncoding("shift_jis"));
                sw.Write(""+meForm.mcrm.LastID());
                sw.Close();
            }
            return true;
        }

    }
}

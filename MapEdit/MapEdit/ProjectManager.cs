using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing.Imaging;

namespace MapEdit
{
    //プロジェクトデータを保存,上書き,開く機能をするクラス
    public class ProjectManager
    {
        private readonly MapEditForm meForm;

        //開いているプロジェクトへのパス
        private string currentProjectPath="";

        public ProjectManager(MapEditForm meForm)
        {
            this.meForm = meForm;
        }

        public bool SaveNewProject(string path,string projectName)
        {
            //データを保存する場所を設定
            currentProjectPath = path + @"\" + projectName;
            //ディレクトリが存在しないならfalseで抜ける
            if (Directory.Exists(path) == false) return false;
            //プロジェクトフォルダを作成する
            Directory.CreateDirectory(currentProjectPath);
            var bitmap = meForm.mcrm.GetBitmapSheet();
            if (bitmap != null)
            {
                bitmap.Save(currentProjectPath + @"\MapChip.png", ImageFormat.Png);
            }
            //78461616646988

            return true;
        }

    }
}

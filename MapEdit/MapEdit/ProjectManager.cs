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
        //開いているプロジェクトのパス
        private string currentProjectPath;

        public ProjectManager(MapEditForm meForm)
        {
            this.meForm = meForm;
            currentProjectPath = "";
        }

        //プロジェクトをロードする（引数のパスはプロジェクト名を含むとこまで）
        public bool LoadProject(string path)
        {
            if (Directory.Exists(path) == false) return false;
            if (File.Exists(path + @"\MapChip.png") == false) return false;
            if (File.Exists(path + @"\MapChip.txt") == false) return false;
            currentProjectPath = path;
            //MapChip.txt読み込み
            StreamReader sr=new StreamReader(
                    path + @"\MapChip.txt",
                    Encoding.GetEncoding("shift_jis")
                );
            int lastId=int.Parse(sr.ReadLine());
            sr.Close();
            //MapData.txt読み込み
            sr= new StreamReader(
                    path + @"\MapData.txt",
                    Encoding.GetEncoding("shift_jis"));
            var mapDataFromText = new MapDataFromText(sr);
            sr.Close();
            meForm.LoadProject(mapDataFromText,lastId,path);
            meForm.sif.SelectMapChipScene.resetMapChip();
            return true;
        }

        //プロジェクトを新しく保存する
        public bool SaveNewProject(string path,string projectName)
        {
            currentProjectPath = path+ @"\"+projectName;
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
                // マップの中身を書き出す
                StringBuilder mapDataText = meForm.mapEdit.GetMapDataText();
                // テキストファイル生成(マップデータ)
                sw = new System.IO.StreamWriter(
                currentProjectPath + @"\MapData.txt",
                false,
                System.Text.Encoding.GetEncoding("shift_jis"));
                sw.Write(mapDataText.ToString());
                sw.Close();
            }
            return true;
        }
        //プロジェクトの上書き
        public bool OverwriteProject()
        {
            if (currentProjectPath == "") return false;
            if (Directory.Exists(currentProjectPath) == false) return false;
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
                sw.Write("" + meForm.mcrm.LastID());
                sw.Close();
                // マップの中身を書き出す
                StringBuilder mapDataText = meForm.mapEdit.GetMapDataText();
                // テキストファイル生成(マップデータ)
                sw = new System.IO.StreamWriter(
                currentProjectPath + @"\MapData.txt",
                false,
                System.Text.Encoding.GetEncoding("shift_jis"));
                sw.Write(mapDataText.ToString());
                sw.Close();
            }
            return true;
        }

    }
}

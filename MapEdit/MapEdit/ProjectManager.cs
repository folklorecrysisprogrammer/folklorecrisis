﻿using System;
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
    //若干関数が肥大化してはいるが、それ以外は特に問題はない
    public class ProjectManager
    {
      //  private readonly MapEditForm meForm;
        //開いているプロジェクトのパス
        private string currentProjectPath="";

        //プロジェクトをロードする（引数のパスはプロジェクト名を含むとこまで）
        public MapInfoFromText LoadProject(string path)
        {
            if (Directory.Exists(path) == false) throw new Exception("notExists");
            if (File.Exists(path + @"\MapChip.png") == false) throw new Exception("notExists");
            if (File.Exists(path + @"\MapChip.txt") == false) throw new Exception("notExists");
            currentProjectPath = path;
            //MapChip.txt読み込み
            StreamReader sr = new StreamReader(
                    path + @"\MapChip.txt",
                    Encoding.GetEncoding("shift_jis")
                );
            int lastId = int.Parse(sr.ReadLine());
            sr.Close();
            //MapData.txt読み込み
            using (sr = new StreamReader(
                   path + @"\MapData.txt",
                   Encoding.GetEncoding("shift_jis"))
                   )
            {

                return new MapInfoFromText(sr, lastId);
            }
        }

        //プロジェクトを新しく保存する
        public bool SaveNewProject(string path,string projectName,ProjectInfo pinfo)
        {
            currentProjectPath = path+ @"\"+projectName;
            if (Directory.Exists(path) == false) return false;
            Directory.CreateDirectory(currentProjectPath);
            WriteTxtFile(pinfo);
            return true;
        }
        //プロジェクトの上書き
        public bool OverwriteProject(ProjectInfo pinfo)
        {
            if (currentProjectPath == "") return false;
            if (Directory.Exists(currentProjectPath) == false) return false;
            Directory.CreateDirectory(currentProjectPath);
            WriteTxtFile(pinfo);
            return true;
        }

        //txtを書く
        private void WriteTxtFile(ProjectInfo pinfo) {
            var bitmap = pinfo.BitmapSheet;
            if (bitmap != null)
            {
                // 画像生成
                bitmap.Save(currentProjectPath + @"\MapChip.png", ImageFormat.Png);
                // テキストファイル生成
                StreamWriter sw = new StreamWriter(
                    currentProjectPath + @"\MapChip.txt",
                    false,
                    Encoding.GetEncoding("shift_jis"));
                sw.Write("" + pinfo.LastId);
                sw.Close();
                // マップの中身を書き出す
                StringBuilder mapDataText = pinfo.MapDataText;
                // テキストファイル生成(マップデータ)
                sw = new System.IO.StreamWriter(
                currentProjectPath + @"\MapData.txt",
                false,
                System.Text.Encoding.GetEncoding("shift_jis"));
                sw.Write(mapDataText.ToString());
                sw.Close();
            }
        }

    }
}

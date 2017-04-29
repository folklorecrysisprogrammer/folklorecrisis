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
            if (Directory.Exists(path + @"\MapChip.png") == false) return false;
            if (Directory.Exists(path + @"\MapChip.txt") == false) return false;
            Bitmap bitmap = (Bitmap)Image.FromFile(path + @"\MapChip.png");
            StreamReader sr=new StreamReader(
                    path + @"\MapChip.txt",
                    Encoding.GetEncoding("shift_jis"));
            int lastId=int.Parse(sr.ReadLine());
            sr.Close();
            meForm.mcrm.LoadBitmapSheet(lastId, bitmap);
            meForm.sif.MapPalletScene.LoadProject(path + @"\MapChip.png", bitmap.Height / meForm.MapChipSize);
            return true;
        }

        //プロジェクトを新しく保存する
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
                // マップの中身を書き出す
                string mapData = "";
                MapData md = meForm.mws.GetMapData();
                for (int y = 0; y < md.MapSize.Height; y++)
                {
                    for (int x = 0; x < md.MapSize.Width; x++)
                    {
                        MapOneMass mom = md[x, y];
                        MapChip[] ChipId = mom.mapChips;
                        for (int i = 0; i < ChipId.Length; i++)
                        {
                            int Id = ChipId[i].Id;
                            int Angle = (int)(ChipId[i].Angle / 90.0);
                            int turnFlag = ChipId[i].turnFlag;
                            mapData += Id + "," + Angle + "," + turnFlag + ",";
                        }
                    }
                    mapData += Environment.NewLine;
                }
                // テキストファイル生成(マップデータ)
                sw = new System.IO.StreamWriter(
                currentProjectPath + @"\MapData.txt",
                false,
                System.Text.Encoding.GetEncoding("shift_jis"));
                sw.Write(mapData);
                sw.Close();
            }
            return true;
        }

    }
}

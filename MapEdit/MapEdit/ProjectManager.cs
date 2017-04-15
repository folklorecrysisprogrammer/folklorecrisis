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
                    Encoding.GetEncoding("shift_jis"));
            int lastId=int.Parse(sr.ReadLine());
            sr.Close();
            //MapData.txt読み込み
            sr= new StreamReader(
                    path + @"\MapData.txt",
                    Encoding.GetEncoding("shift_jis"));
            var mapDataFromText = new MapDataFromText(sr);
            sr.Close();
            meForm.LoadProject(mapDataFromText.MapChipSize,mapDataFromText.MapSize);
            meForm.mcrm.LoadBitmapSheet(lastId, path + @"\MapChip.png");
            meForm.sif.MapPalletScene.LoadProject();
            meForm.mws.GetMapData().LoadProject(mapDataFromText);
            meForm.sif.SelectMapChipScene.resetMapChip();
            return true;
        }

        //プロジェクトを新しく保存する
        public bool SaveNewProject(string path)
        {
            currentProjectPath = path;
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
                string mapDataText = "";
                MapData md = meForm.mws.GetMapData();
                mapDataText = meForm.MapChipSize + "," + md.MapSize.Width + "," + md.MapSize.Height;
                for (int y = 0; y < md.MapSize.Height; y++)
                {
                    mapDataText += Environment.NewLine;
                    for (int x = 0; x < md.MapSize.Width; x++)
                    {
                        MapOneMass mom = md[x, y];
                        MapChip[] ChipId = mom.mapChips;
                        for (int i = 0; i < ChipId.Length; i++)
                        {
                            int Id = ChipId[i].Id;
                            int Angle = (int)(ChipId[i].Angle / 90.0);
                            int turnFlag = ChipId[i].turnFlag;
                            mapDataText += Id + "," + Angle + "," + turnFlag + ",";
                        }
                    }
                    
                }
                // テキストファイル生成(マップデータ)
                sw = new System.IO.StreamWriter(
                currentProjectPath + @"\MapData.txt",
                false,
                System.Text.Encoding.GetEncoding("shift_jis"));
                sw.Write(mapDataText);
                sw.Close();
            }
            return true;
        }
        //プロジェクトの上書き
        public bool OverwriteProject()
        {
            if (currentProjectPath == "") return false;
            return SaveNewProject(currentProjectPath);
        }

    }
}

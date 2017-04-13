using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEdit
{
    //作業データクラス
    public class ProjectData
    {
        //プロジェクトのあるパス
        public string Path { get;}
        //プロジェクトの名前
        public string ProjectName { get; }
        //初期化
        public ProjectData(string path,string projectName)
        {
            Path = path;
            ProjectName = projectName;
        }
    }
}

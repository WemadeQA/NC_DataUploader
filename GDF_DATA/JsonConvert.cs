using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GDF_DATA
{
    class JsonConvert
    {
        public Dictionary<string, string> _dicChangedName = new Dictionary<string, string>();
        Stack<string> _sourcePath = new Stack<string>();
        Stack<string> _destPath = new Stack<string>();
        public JsonConvert()
        {
            string[] columnTexts = Properties.Resources.JsonConvert.Split('\n');

            foreach (var col in columnTexts)
            {
                string[] splitstr = col.Split(',');

                if (!_dicChangedName.ContainsKey(splitstr[0]))
                    _dicChangedName.Add(splitstr[0].Replace("\r", ""), splitstr[1].Replace("\r", ""));
            }
        }

        public void SetJsonFoler(string jsonPath)
        {
            var di = new DirectoryInfo(jsonPath);
            if(di.Exists == false)
            {
                return;
            }
            var dirs = Directory.GetDirectories(jsonPath);
      

            foreach (var file in di.GetDirectories())
            {
                foreach (var name in _dicChangedName)
                {
                    if (name.Key.Contains(file.Name))
                    {
                        string filePath = jsonPath + "\\" + file.Name;
                        string folderPath = jsonPath + "\\" + name.Value;
                        // 폴더 위치 변경
                        //System.IO.Directory.Move(filePath, movedPath + "\\" + file.Name);
                        // 폴더 이름 변경
                        System.IO.Directory.Move(jsonPath + "\\" + file.Name, jsonPath + "\\" + name.Value);

                        _sourcePath.Push(jsonPath + "\\" + file.Name);
                        _destPath.Push(jsonPath + "\\" + name.Value);
                    }
                }
            }
            
            //deleteFoler(di.Parent.FullName + "\\" + "CUSTOMIZING");
        }

        public void UndoFolder()
        {
            foreach(string source in _sourcePath)
            {
                string dest = _destPath.Pop();
                System.IO.Directory.Move(dest, source);
            }
        }

        private void deleteFoler(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);

            if (di.Exists)
            {
                di.Delete(true);
            }
        }

        private void deleteFoler(DirectoryInfo di)
        {
            if (di.Exists)
            {
                di.Delete(true);
            }
        }

        private void createFolder(string name, string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);

            if (di.Exists == false)
            {
                di.Create();
            }
        }
    }
}

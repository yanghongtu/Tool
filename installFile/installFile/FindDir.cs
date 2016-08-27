using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using IWshRuntimeLibrary;

namespace installFile
{
    class FindDir
    {
        const string startupPath =@"C:\Users\Administrator\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup";
        public string FindDirPath()
        {
            DirectoryInfo di = new DirectoryInfo(startupPath);
            FileInfo[] fis = di.GetFiles();
            foreach (var item in fis)
            {
                if (item.Name.Contains(".lnk"))
                {
                    //发现是 快捷键的 就进来
                    //判断是不是对的地址
                    string dirPath;
                    if (IsDirPathByLnkFullName(item.FullName,out dirPath))
                    {
                        return dirPath;
                    }
                }
            }
            return "";
        }
        private bool IsDirPathByLnkFullName(string fileFullPath,out string FilePath)
        {
            FilePath = "";
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(fileFullPath);
            string fileName="FillBillSystem.exe";
            if(shortcut.TargetPath.Contains(fileName))
            {
                FilePath = shortcut.TargetPath.Substring(0, shortcut.TargetPath.Length - fileName.Length);
                return true;
            }
            return false;
        }
    }
}

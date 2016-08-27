using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace installFile
{
    class CopyFile
    {
        /// <summary>
        /// 剪切 fromDir 文件夹里面的单据配置文件夹 到 toDir文件夹里面
        /// </summary>
        /// <param name="fromDir">从复制过来的文件夹</param>
        /// <param name="toDir">复制到的文件夹</param>
        public void CopyFullDir(string fromDir,string toDir)
        {
            DirectoryInfo di = new DirectoryInfo(fromDir);
            DirectoryInfo[] dis = di.GetDirectories();
            foreach (var item in dis)
            {
                //要复制的文件夹名称
                string dirName=item.Name;
                item.MoveTo(toDir + "\\" + dirName);
            }
        }
        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="src"></param>
        /// <param name="des"></param>
        public void CopyDIY(string src, string des)
        {
            //复制除了 config文件夹 以外的 数据
            this.CopyDirectory(src, des,"config");
            //假如存在config文件夹 复制 config文件夹里面的数据
            if (Directory.Exists(src+"\\config"))
            {
                //复制config 文件夹里面的数据
                this.CopyConfigData(src + "\\config", des + "\\config");
            }
        }
        /// <summary>
        /// 复制config 里面的数据
        /// </summary>
        /// <param name="src_config"></param>
        /// <param name="des_config"></param>
        private void CopyConfigData(string src_config,string des_config)
        {
            DirectoryInfo src_di = new DirectoryInfo(src_config);
            DirectoryInfo des_di = new DirectoryInfo(des_config);
            DirectoryInfo[] src_dis = src_di.GetDirectories();
            DirectoryInfo[] des_dis = des_di.GetDirectories();

            foreach (var item in src_dis)
            {
                Form2 f = new Form2(item, des_dis);
                f.ShowDialog();
            }
        }
        
        /// <summary>
        /// 拷贝文件夹下面的全部东西到另外一个里面
        /// </summary>
        /// <param name="srcdir"></param>
        /// <param name="desdir"></param>
        public void CopyDirectory(string srcdir, string desdir, string exceptdir)
        {
            DirectoryInfo di = new DirectoryInfo(srcdir);
            
            DirectoryInfo[] dis = di.GetDirectories();
            FileInfo[] fis = di.GetFiles();
            //如果是文件夹  就看 是不是 要创建的  要是有的 就不创建要是没有的 就创建
            foreach (var dir in dis)
            {
                //假如排除的文件夹名称不等于空就排除对应的文件夹
                if (exceptdir!=""&&dir.Name==exceptdir)
                {
                    continue;
                }
                string tempDirName = desdir + "\\" + dir.Name;
                if (!Directory.Exists(tempDirName))
                {
                    Directory.CreateDirectory(tempDirName);
                }
                else
                {
                    CopyDirectory(dir.FullName, tempDirName,"");
                }
            }
            //如果已经存在文件就把文件覆盖掉
            foreach (var file in fis)
            {
                string tempFileName = desdir + "\\" + file.Name;
                File.Copy(file.FullName, tempFileName,true);
            }
        }//function end
    }
}

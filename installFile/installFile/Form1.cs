using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace installFile
{
    public partial class Form1 : Form
    {
        private string toDir = "";
        private string fromDir = "a";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FindDir fd = new FindDir();
            string dirPath = fd.FindDirPath();
            if (dirPath != "")
            {
                MessageBox.Show("找到文件夹:"+dirPath);
                this.label1.Text = dirPath;
                this.toDir = dirPath;
                this.button2.Enabled = true;
            }
            else
            {
                MessageBox.Show("不能找到文件夹!请联系有关人员");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Button b=sender as Button;
            string text = b.Text;
            b.Enabled = false;
            b.Text = "正在拷贝中...";
            //
            CopyFile cf = new CopyFile();
            cf.CopyDIY(Environment.CurrentDirectory+"\\"+ fromDir, toDir);
            b.Text = text;
            MessageBox.Show("拷贝完成");
        }
    }
}

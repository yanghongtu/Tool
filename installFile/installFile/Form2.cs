using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace installFile
{
    public partial class Form2 : Form
    {
        /// <summary>
        /// 给config文件夹 文件选择 新增还是  覆盖的方法
        /// </summary>
        /// <param name="src_path">来源位置</param>
        /// <param name="des_path">目标位置</param>
        /// <param name="count">总共单据数</param>
        public Form2(DirectoryInfo src_di,DirectoryInfo[] des_dis)
        {
            InitializeComponent();
            this._src_di = src_di;
            this._des_dis = des_dis;
            //设置页面 业务名称
            this.label2.Text = this._src_di.Name;
        }
        private DirectoryInfo _src_di;
        private DirectoryInfo[] _des_dis;
        /// <summary>
        /// 新增业务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            b.Enabled = false;
            string path = this._des_dis[0].Parent.FullName;
            DirectoryInfo index_di = new DirectoryInfo(path);
            string des_di_name = (index_di.GetDirectories().Length + 1) + "-" + (this._src_di.Name.Contains("-") ? this._src_di.Name.Split('-')[1] : this._src_di.Name);
            //
            string des_path=_des_dis[0].Parent.FullName+"\\"+des_di_name;
            if (!Directory.Exists(des_path))
            {
                Directory.CreateDirectory(des_path);
            }
            CopyFile cf=new CopyFile();
            cf.CopyDirectory(_src_di.FullName,des_path , "");
            this.Close();
        }
        /// <summary>
        /// 替换业务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            //获取当前的 文件夹目录
            DirectoryInfo[] dis = new DirectoryInfo(this._des_dis[0].Parent.FullName).GetDirectories();
            //列出 全部的单据 
            int n = this._des_dis.Length;
            int h=90;
            Point[] ps = new Point[n];
            for (int i = 0; i < n; i++)
            {
                ps[i] = new Point(0, h * i);
            }
            for (int j = 0; j < n; j++)
            {
                this.AddButton(dis[j],ps[j]);
            }
        }
        private void AddButton(DirectoryInfo des,Point p)
        {
            Button bu = new Button();
            bu.Width = 500;
            bu.Height = 80;
            bu.Text = des.Name;
            bu.Location = p;
            bu.Tag = des;
            bu.Font = new Font("黑体", 20);
            bu.Click += bu_Click;
            this.panel1.Controls.Add(bu);
        }

        void bu_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            b.Enabled = false;
            DirectoryInfo di = b.Tag as DirectoryInfo;
            string index = di.Name.Split('-')[0];
            if (di.Exists)
            {
                di.Delete(true);
            }
            else
            {
                MessageBox.Show("该文件夹 不存在!!");
            }
            //copy file to purpose directory
            string purpose_name=index +"-"+ (this._src_di.Name.Contains("-")?this._src_di.Name.Split('-')[1]:this._src_di.Name);
            string purpose_path = this._des_dis[0].Parent.FullName + "\\" + purpose_name;
            if (!Directory.Exists(purpose_path))
            {
                Directory.CreateDirectory(purpose_path);
            }
            CopyFile cf = new CopyFile();
            cf.CopyDirectory(this._src_di.FullName, purpose_path,"");
            this.Close();
        }
    }
}

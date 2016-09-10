using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using XinJiangFBMSet.ServiceLayer;

namespace XinJiangFBMSet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 设置按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Button bu = sender as Button;
            bu.Enabled = false;
            //开启网络适配器
            WorkClass wc = new WorkClass();

            if (wc.OpenAdapter())
            { MessageBox.Show("开启  本地连接 成功"); }
            else
            {
                MessageBox.Show("开启本地连接 失败");
            }
            //复制文件
            if (wc.CopyFile())
            {
                MessageBox.Show("复制文件成功");
            }
            else
            {
                MessageBox.Show("复制文件  失败");
            }
            //设置ini文件
            if (wc.SetIniFile(this.tb_morendiqu.Text.Trim(), this.tb_morenshi.Text.Trim(), this.tb_morenxian.Text.Trim(), this.tb_shihuoxian.Text.Trim(), this.tb_yinhangmingcheng.Text.Trim(),this.tb_ip.Text.Trim()))
            {
                MessageBox.Show("设置ini文件成功");
            }
            else
            {
                MessageBox.Show("设置ini文件失败");
            }
            //设置ip地址
            if (wc.SetIP(this.tb_ip.Text.Trim(), this.tb_netsub.Text.Trim(), this.tb_gateway.Text.Trim()))
            {
                MessageBox.Show("设置ip地址成功");
            }
            else
            {
                MessageBox.Show("设置ip地址失败");
            }
            
            //设置身份证联网核查 url
            wc.SetIDCardV(this.tb_IDCardV_zhanghao.Text.Trim());
            
            bu.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //读取银行属性
            WorkClass wc = new WorkClass();
            string morendiqu;
            string morenshi;
            string morenxian;
            string shihuoxian;
            string yinhangmingcheng;
            wc.ShowRegion(out morendiqu, out morenshi, out morenxian, out shihuoxian, out yinhangmingcheng);
            this.tb_morendiqu.Text = morendiqu;
            this.tb_morenshi.Text = morenshi;
            this.tb_morenxian.Text = morenxian;
            this.tb_shihuoxian.Text = shihuoxian;
            this.tb_yinhangmingcheng.Text = yinhangmingcheng;

            //读取联网核查的账号
            this.tb_IDCardV_zhanghao.Text = wc.ReadIDCardV();
        }

        private void bt_viewAdapterEnable_Click(object sender, EventArgs e)
        {
            WorkClass wc = new WorkClass();
            if (wc.ShowNetStatus()) { MessageBox.Show("网络正常"); }
            else { MessageBox.Show("网络不通"); }
        }
    }
}

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

        }
    }
}

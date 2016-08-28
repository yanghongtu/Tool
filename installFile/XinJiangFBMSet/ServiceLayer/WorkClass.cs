using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace XinJiangFBMSet.ServiceLayer
{
    public class WorkClass
    {
        /// <summary>
        /// 复制文件过去
        /// </summary>
        /// <returns></returns>
        public bool CopyFile()
        {
            FileTool.FileControl fc = new FileTool.FileControl();
            Hashtable ht = fc.FindDirPath(FileTool.FileControl.win7startupPath, new string[] { "FillBillSystem.exe", "DataService.exe" });
            if (ht["FillBillSystem.exe"].ToString().Trim()==""||ht["DataService.exe"].ToString().Trim()=="")
            {
                MessageBox.Show("没有获取到文件夹位置");
                return false;
            }
            string path = Environment.CurrentDirectory;
            string dir_s_f=path+"\\a\\fillbill";
            string dir_d_f = ht["FillBillSystem.exe"].ToString();
            bool result = false;
            
            //复制fillbill文件夹下面的文件
            fc.CopyDirectory(dir_s_f, dir_d_f, "");
            //复制service文件下面的文件

            string dir_s_s = path + "\\a\\service";
            string dir_d_s = ht["DataService.exe"].ToString();

            fc.CopyDirectory(dir_s_s, dir_d_s, "");
            result = true;
            return result;
        }
        /// <summary>
        /// 修改ini文件内容
        /// </summary>
        /// <param name="morendiqu">默认地区</param>
        /// <param name="morenshi">默认市</param>
        /// <param name="morenxian">默认县</param>
        /// <param name="shihuoxian">市或县</param>
        /// <param name="yinhangmingcheng">银行名称</param>
        /// <returns>是否执行成功</returns>
        public bool SetIniFile(string morendiqu,string morenshi,string morenxian,string shihuoxian,string yinhangmingcheng,string ip)
        {
            bool result = false;
            FileTool.FileControl fc = new FileTool.FileControl();
            Hashtable ht = fc.FindDirPath(FileTool.FileControl.win7startupPath, new string[] { "FillBillSystem.exe", "DataService.exe" });
            
            if (ht["FillBillSystem.exe"].ToString().Trim()==""||ht["DataService.exe"].ToString().Trim()=="")
            {
                MessageBox.Show("没有获取到文件夹位置");
                return false;
            }
            string fillsetpath=ht["FillBillSystem.exe"].ToString()+"\\set.ini";
            FileTool.IniFileControl ifc_fill = new FileTool.IniFileControl(fillsetpath);
            if(!ifc_fill.WriteIniField("填单机参数", "是否屏蔽ctrlaltdel", "否"))return false;
            if(!ifc_fill.WriteIniField("填单机参数", "默认地区", morendiqu))return false;
            if(!ifc_fill.WriteIniField("填单机参数", "默认市", morenshi))return false;
            if(!ifc_fill.WriteIniField("填单机参数", "默认县",morenxian))return false;
            if(!ifc_fill.WriteIniField("填单机参数", "市或县", shihuoxian))return false;
            if(!ifc_fill.WriteIniField("填单机参数", "银行名称", yinhangmingcheng))return false;
            if (!ifc_fill.WriteIniField("填单机参数", "无接口连接地址", ip)) return false;

            string servicesetpath = ht["DataService.exe"].ToString() + "\\set.ini";
            FileTool.IniFileControl ifc_service = new FileTool.IniFileControl(servicesetpath);
            if (!ifc_service.WriteIniField("系统设置", "数据地址", ip)) return false;


            string kaihu = ht["FillBillSystem.exe"].ToString() + "\\config\\1-开户\\本人业务模块.txt";
            FileTool.IniFileControl ifc_kaihu = new FileTool.IniFileControl(kaihu);
            if(!ifc_kaihu.WriteIniField("业务", "输入14必填","是"))return false;


            string wukazhe = ht["FillBillSystem.exe"].ToString() + "\\config\\5-无卡折\\配置文件.txt";
            FileTool.IniFileControl ifc_wukazhe = new FileTool.IniFileControl(wukazhe);
            if(!ifc_wukazhe.WriteIniField("业务", "是否打印填单号","否"))return false;

            result = true;
            return result;
        }
        /// <summary>
        /// 查看默认地区 默认市默认县 市或县 银行名称
        /// </summary>
        /// <param name="morendiqu"></param>
        /// <param name="morenshi"></param>
        /// <param name="morenxian"></param>
        /// <param name="shihuoxian"></param>
        /// <param name="yinhangmingcheng"></param>
        public void ShowRegion(out string morendiqu,out string morenshi,out string morenxian,out string shihuoxian,out string yinhangmingcheng)
        {
            FileTool.FileControl fc = new FileTool.FileControl();
            Hashtable ht = fc.FindDirPath(FileTool.FileControl.win7startupPath, new string[] { "FillBillSystem.exe", "DataService.exe" });
            string fillsetpath = ht["FillBillSystem.exe"].ToString() + "\\set.ini";
            FileTool.IniFileControl ifc_fill = new FileTool.IniFileControl(fillsetpath);
            morendiqu = ifc_fill.ReadIniField("填单机参数", "默认地区");
            morenshi = ifc_fill.ReadIniField("填单机参数", "默认市");
            morenxian = ifc_fill.ReadIniField("填单机参数", "默认县");
            shihuoxian = ifc_fill.ReadIniField("填单机参数", "市或县");
            yinhangmingcheng = ifc_fill.ReadIniField("填单机参数", "银行名称");
        }
        /// <summary>
        /// 查看网络 是否通
        /// </summary>
        /// <returns></returns>
        public bool ShowNetStatus()
        {
            bool result = false;
            using (ToolHelper.IPClass ipc = new ToolHelper.IPClass())
            {
                result = ipc.IsEnableAdapter("本地连接");
            }
            return result;
        }
        /// <summary>
        /// 开启本地连接  网络适配器
        /// </summary>
        /// <returns></returns>
        public bool OpenAdapter()
        {
            bool result = false;
            using (ToolHelper.IPClass ipc=new ToolHelper.IPClass())
            {
                result = ipc.EnableOrDisableAdapter("本地连接", true);
            }
            return result;
        }
        /// <summary>
        /// 设置IP地址
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="netsub"></param>
        /// <param name="gateway"></param>
        /// <returns></returns>
        public bool SetIP(string ip, string netsub, string gateway)
        {

            if (ip.Contains("?")||gateway.Contains("?"))
            {
                return false;
            }
            if (!this.validateIPAddress(ip)||!this.validateIPAddress(netsub)||!this.validateIPAddress(gateway))
            {
                return false;
            }
            bool result = false;
            using (ToolHelper.IPClass ipc=new ToolHelper.IPClass())
            {
                result = ipc.ModifyIPAddress("本地连接", ip, netsub, gateway);
            }
            return result;
        }
        /// <summary>
        /// 验证ip地址 是否有效
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        private bool validateIPAddress(string ip)
        {
            string[] ips = ip.Split('.');
            if (ips.Length!=4)
            {
                return false;
            }
            for (int i = 0; i < ips.Length; i++)
            {
                try
                {
                    int ips_v = Convert.ToInt32(ips[i]);
                    if (ips_v >= 0 && ips_v < 256)
                    {
                        ;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch { return false; }
            }
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Finder.services
{
    public class WorkClass
    {
        public string ShowIP()
        {
            ToolHelper.IPClass ipc = new ToolHelper.IPClass();
            ToolHelper.NetClass nt = new ToolHelper.NetClass();
            IPAddress[] ipas = Dns.GetHostAddresses(Dns.GetHostName());
            IPAddress ipa = ipas[0];
            ipa.ToString();
        }
    }
}

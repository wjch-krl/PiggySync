using PiggySyncWin.WinUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace PiggySyncWin.WinUI.Infrastructure.Concrete
{
    public class PiggyRemoteHostSync : PiggyRemoteHost
    {
        public PiggyRemoteHostSync(IPAddress ip, string name, bool isClient)
            : base(ip, name)
        {
            this.IsClient = isClient;
        }

        public PiggyRemoteHostSync(PiggyRemoteHost host, bool isClient,byte[] msg)
            : base(host.Ip, host.Name)
        {
            this.IsClient = isClient;
            this.Msg = msg;
        }

        public bool IsClient { get; set; }

        public byte[] Msg { get; set; }
    }
}

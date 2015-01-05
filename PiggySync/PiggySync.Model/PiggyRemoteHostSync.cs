using System.Net;
using PiggySync.Common;

namespace PiggySync.Model
{
    public class PiggyRemoteHostSync : PiggyRemoteHost
    {
        public PiggyRemoteHostSync(IIPAddress ip, string name, bool isClient)
            : base(ip, name)
        {
            IsClient = isClient;
        }

        public PiggyRemoteHostSync(PiggyRemoteHost host, bool isClient, byte[] msg)
            : base(host.Ip, host.Name)
        {
            IsClient = isClient;
            Msg = msg;
        }

        public bool IsClient { get; set; }

        public byte[] Msg { get; set; }
    }
}
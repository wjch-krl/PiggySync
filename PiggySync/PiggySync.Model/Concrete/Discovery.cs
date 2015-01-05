using System;
using System.Linq;
using System.Net;
using System.Text;
using PiggySync.Common;
using PiggySync.Domain.Concrete;
using PiggySync.Model.Abstract;

namespace PiggySync.Model.Concrete
{
    public class Discovery : UdpPacket
    {
        private static readonly byte[] name =
            Encoding.UTF8.GetBytes(XmlSettingsRepository.Instance.Settings.ComputerName);

        public Discovery()
            : base(240)
        {
        }

        public static byte[] Name
        {
            get { return name; }
        }

        public override byte[] GetPacket()
        {
            byte[] ip = PiggyRemoteHost.Me.Ip.GetAddressBytes();
            var msg = new byte[ip.Length + name.Length + 1];
            msg[0] = Code;
            ip.CopyTo(msg, 1); //TODO concat ??
            name.CopyTo(msg, ip.Length + 1);
            return msg;
        }

        public static PiggyRemoteHost GetHostData(byte[] data)
        {
            var ip = new byte[4];
            Array.Copy(data, 1, ip, 0, 4);

            string name = Encoding.UTF8.GetString(data, 5, data.Count() - 5);
            return new PiggyRemoteHost(TypeResolver.IpHelper.Create(ip), name);
        }
    }
}
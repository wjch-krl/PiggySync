using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using PiggySyncWin.WinUI.Models;
using System.Net.NetworkInformation;

namespace PiggySyncWin.WinUI.Models
{
    class Discovery : UDPPacket
    {
        public Discovery()
            : base(240)
        {
        }

        static byte[] name = System.Text.Encoding.UTF8.GetBytes(PiggySyncWin.Domain.Concrete.XmlSettingsRepository.Instance.Settings.ComputerName);
        public static byte[] Name
        {
            get { return name; }
        }

        public override byte[] GetPacket()
        {
            byte[] ip = SyncManager.Me.Ip.GetAddressBytes();
            byte[] msg = new byte[ip.Length + name.Length + 1];
            msg[0] = code;
            ip.CopyTo(msg, 1); //TODO concat ??
            name.CopyTo(msg, ip.Length+1);
            return msg;
        }

        public static PiggyRemoteHost GetHOstData(byte[] data)
        {
            byte[] ip = new byte[4];
            Array.Copy(data, 1, ip, 0, 4);

            string name = System.Text.Encoding.UTF8.GetString(data, 5, data.Count() - 5);
            return new PiggyRemoteHost(new IPAddress(ip), name);       
        }

       

    }

}

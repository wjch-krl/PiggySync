using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace PiggySync.Common
{
    public class IPUtils
    {
        static IPAddress localBroadCastAdress = GetBroadcastIP();
        public static IPAddress LocalBroadCastAdress
        {
            get { return localBroadCastAdress; }
        }

        public static IPAddress LocalIPAddress()
        {
            IPHostEntry host;
            IPAddress localIP = IPAddress.Any;
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork && ip != IPAddress.Any)
                {
                    localIP = ip;
                    break;
                }
            }
            return localIP;
        }

        public static IPAddress GetBroadcastIP()
        {
			return IPAddress.Broadcast;

            IPAddress maskIP = GetHostMask();
            IPAddress hostIP = LocalIPAddress();

            if (maskIP == null || hostIP == null)
                return null;

            byte[] complementedMaskBytes = new byte[4];
            byte[] broadcastIPBytes = new byte[4];

            for (int i = 0; i < 4; i++)
            {
                complementedMaskBytes[i] = (byte)~(maskIP.GetAddressBytes().ElementAt(i));
                broadcastIPBytes[i] = (byte)((hostIP.GetAddressBytes().ElementAt(i)) | complementedMaskBytes[i]);
            }

            return new IPAddress(broadcastIPBytes);

        }


        private static IPAddress GetHostMask()
        {
            NetworkInterface[] Interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface Interface in Interfaces)
            {
                IPAddress hostIP = LocalIPAddress();
                UnicastIPAddressInformationCollection UnicastIPInfoCol = Interface.GetIPProperties().UnicastAddresses;
                foreach (UnicastIPAddressInformation UnicatIPInfo in UnicastIPInfoCol)
                {
                    if (UnicatIPInfo.Address.ToString() == hostIP.ToString())
                    {
                        return UnicatIPInfo.IPv4Mask;
                    }
                }
            }

            return null;
        }
    }
}

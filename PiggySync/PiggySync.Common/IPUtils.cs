using System;
using System.Diagnostics;

namespace PiggySync.Common
{
    public class IPUtils
    {
        static IPUtils()
        {
            LocalBroadCastAdress = GetBroadcastIP();
        }

        public static IIPAddress LocalBroadCastAdress { get; private set; }

        public static IIPAddress LocalIPAddress()
        {
            IIPAddress host;
            IIPAddress localIP = TypeResolver.IpHelper.LocalIp;
            return localIP;
        }

        public static IIPAddress GetBroadcastIP()
        {
            return TypeResolver.IpHelper.Broadcast;
            //IIPAddress maskIP = GetHostMask();
            //IIPAddress hostIP = LocalIPAddress();

            //if (maskIP == null || hostIP == null)
            //    return null;

            //var complementedMaskBytes = new byte[4];
            //var broadcastIPBytes = new byte[4];
            //var maskBytes = maskIP.GetAddressBytes();
            //var hostBytes = hostIP.GetAddressBytes();
            //for (int i = 0; i < 4; i++)
            //{
            //    complementedMaskBytes[i] = (byte) ~(maskBytes[i]);
            //    broadcastIPBytes[i] = (byte) (hostBytes[i] | complementedMaskBytes[i]);
            //}

            //return new TypeResolver.IpHelper.Create(broadcastIPBytes);
        }


        //private static IIPAddress GetHostMask()
        //{
        //    try
        //    {
        //        INetworkInterface[] Interfaces = INetworkInterface.GetAllNetworkInterfaces();
        //        foreach (var Interface in Interfaces)
        //        {
        //            IIPAddress hostIP = LocalIPAddress();
        //            UnicastIPAddressInformationCollection UnicastIPInfoCol =
        //                Interface.GetIPProperties().UnicastAddresses;
        //            foreach (var UnicatIPInfo in UnicastIPInfoCol)
        //            {
        //                if (UnicatIPInfo.Address.ToString() == hostIP.ToString())
        //                {
        //                    return UnicatIPInfo.IPv4Mask;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.WriteLine("Get Host Mask: {0]", e);
        //    }
        //    return IPAddress.Parse("255.255.255.0");
        //}
    }
}
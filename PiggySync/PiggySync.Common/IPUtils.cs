using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System;
using System.Diagnostics;

namespace PiggySync.Common
{
	public class IPUtils
	{
		public static IPAddress LocalBroadCastAdress
		{
			get;
			private set;
		}

		static IPUtils ()
		{
			LocalBroadCastAdress = GetBroadcastIP ();
		}

		public static IPAddress LocalIPAddress ()
		{
			IPHostEntry host;
			IPAddress localIP = IPAddress.Any;
			try
			{
				host = Dns.GetHostEntry (Dns.GetHostName ());
				foreach (var ip in host.AddressList)
				{
					if (ip.AddressFamily == AddressFamily.InterNetwork && ip != IPAddress.Any)
					{
						localIP = ip;
						break;
					}
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine ("Create Local IP: {0]", e);
			}
			return localIP;

		}

		public static IPAddress GetBroadcastIP ()
		{
			return IPAddress.Broadcast;
			IPAddress maskIP = GetHostMask ();
			IPAddress hostIP = LocalIPAddress ();

			if (maskIP == null || hostIP == null)
				return null;

			var complementedMaskBytes = new byte[4];
			var broadcastIPBytes = new byte[4];
			var maskBytes = maskIP.GetAddressBytes ();
			var hostBytes = hostIP.GetAddressBytes ();
			for (int i = 0; i < 4; i++)
			{
				complementedMaskBytes [i] = (byte)~(maskBytes [i]);
				broadcastIPBytes [i] = (byte)(hostBytes [i] | complementedMaskBytes [i]);
			}

			return new IPAddress (broadcastIPBytes);
		}


		private static IPAddress GetHostMask ()
		{
			try
			{
				NetworkInterface[] Interfaces = NetworkInterface.GetAllNetworkInterfaces ();
				foreach (var Interface in Interfaces)
				{
					IPAddress hostIP = LocalIPAddress ();
					UnicastIPAddressInformationCollection UnicastIPInfoCol = Interface.GetIPProperties ().UnicastAddresses;
					foreach (var UnicatIPInfo in UnicastIPInfoCol)
					{
						if (UnicatIPInfo.Address.ToString () == hostIP.ToString ())
						{
							return UnicatIPInfo.IPv4Mask;
						}
					}
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine ("Get Host Mask: {0]", e);
			}
			return IPAddress.Parse ("255.255.255.0");
		}
	}
}
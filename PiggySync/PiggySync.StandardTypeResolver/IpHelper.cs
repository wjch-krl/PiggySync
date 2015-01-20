using System;
using PiggySync.Common;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace PiggySync.StandardTypeResolver
{
	public class IpHelper : IIPHelper
	{
		public IIPAddress Create (byte[] adrBytes)
		{
			return new StandardIPAddress (new IPAddress (adrBytes));
		}

		public IIPAddress Any//TODO
		{
			get
			{
				return new StandardIPAddress (IPAddress.Any);
			}
		}

		public IIPAddress Broadcast
		{
			get
			{
				return new StandardIPAddress (IPAddress.Broadcast);
			}
		}

		private IPAddress localIp = LocalIPAddress ();

		public IIPAddress LocalIp
		{
			get
			{
				return new StandardIPAddress (localIp);
			}
		}

		private static IPAddress LocalIPAddress ()
		{
			if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
			{
				return null;
			}
			IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
			return host
				.AddressList
				.LastOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
		}
	}
}


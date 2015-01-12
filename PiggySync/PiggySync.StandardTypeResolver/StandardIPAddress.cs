using System;
using PiggySync.Common;
using System.Net;

namespace PiggySync.StandardTypeResolver
{
	public class StandardIPAddress : IIPAddress
	{
		public byte[] GetAddressBytes ()
		{
			return IpAdress.GetAddressBytes ();
		}

		public bool AddressFamilyIsInterNetwork
		{
			get
			{
				return IpAdress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork;
			}
		}

		public IPAddress IpAdress { get ; private set; }

		public StandardIPAddress (IPAddress ip)
		{
			IpAdress = ip;
		}

		public override string ToString ()
		{
			return IpAdress.ToString ();
		}
	}
}


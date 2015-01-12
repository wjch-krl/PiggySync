using System;
using PiggySync.Common;
using System.Net.Sockets;

namespace PiggySync.StandardTypeResolver
{
	public class StandardUdpClient : IUdpClient 
	{
		public byte[] Receive (ref IIPEndPoint source)
		{
			var ip = ((StandardIpEndPoint)source).IPEndPoint;
			return UdpClient.Receive (ref ip);
		}

		public void Send (byte[] msg, int p, IIPEndPoint destination)
		{
			UdpClient.Send (msg,p,((StandardIpEndPoint)destination).IPEndPoint);
		}

		public bool EnableBroadcast
		{
			get
			{
				return UdpClient.EnableBroadcast;
			}
			set
			{
				UdpClient.EnableBroadcast = value;
			}
		}

		public UdpClient UdpClient { get; private set; }

		public StandardUdpClient ()
		{
			UdpClient = new UdpClient ();
		}

		public StandardUdpClient(int p)
		{
			UdpClient = new UdpClient (p);
		}
	}
}									
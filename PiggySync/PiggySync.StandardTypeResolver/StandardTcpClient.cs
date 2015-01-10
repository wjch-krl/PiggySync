using System;
using PiggySync.Common;
using System.Net.Sockets;

namespace PiggySync.StandardTypeResolver
{
	public class StandardTcpClient : ITcpClient
	{
		public INetworkStream GetStream ()
		{
			return new StandardNetworkStream (TcpClient.GetStream ());
		}

		public void Connect (IIPEndPoint host)
		{
			TcpClient.Connect (((StandardIpEndPoint)host).IPEndPoint);
		}

		public void Dispose ()
		{
			TcpClient.Close ();
		}

		internal TcpClient TcpClient { get; private set; }

		public StandardTcpClient (TcpClient client)
		{
			TcpClient = client;
		}

		public StandardTcpClient (StandardIpEndPoint iIPEndPoint)
		{
			TcpClient = new TcpClient (iIPEndPoint.IPEndPoint);
		}
	}
}


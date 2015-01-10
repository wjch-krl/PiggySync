using System;
using PiggySync.Common;
using System.Net.Sockets;
using PiggySync.Core.Concrete;

namespace PiggySync.StandardTypeResolver
{
	public class StandardTcpListener:ITcpListener
	{
		public void Start ()
		{
			TcpListener.Start ();
		}

		public ITcpClient AcceptTcpClient ()
		{
			return new StandardTcpClient(TcpListener.AcceptTcpClient ());
		}

		public void Stop ()
		{
			TcpListener.Stop ();
		}
			
		public TcpListener TcpListener { get; private set; }

		public StandardTcpListener (StandardIpEndPoint iIPEndPoint)
		{
			TcpListener = new TcpListener (iIPEndPoint.IPEndPoint);
		}
	}
}


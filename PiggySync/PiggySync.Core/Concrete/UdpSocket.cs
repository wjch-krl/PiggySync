using System;
using System.Net;

namespace PiggySync.Core.Concrete
{
	class UdpSocket
	{
		public bool EnableBroadcast {
			get;
			set;
		}

		public UdpSocket (int i)
		{
			throw new NotImplementedException ();
		}

		public UdpSocket ()
		{
			throw new NotImplementedException ();
		}

		public byte[] Receive (ref IPEndPoint source)
		{
			throw new NotImplementedException ();
		}

		public void Send (byte[] msg, int length, IPEndPoint hostAddr)
		{
			throw new NotImplementedException ();
		}
	}


}

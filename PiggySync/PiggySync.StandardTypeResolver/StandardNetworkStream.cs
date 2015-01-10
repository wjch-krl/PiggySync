using System;
using PiggySync.Common;
using System.Net.Sockets;

namespace PiggySync.StandardTypeResolver
{
	public class StandardNetworkStream : INetworkStream
	{
		internal NetworkStream NetworkStream { get; private set; }

		public StandardNetworkStream (NetworkStream stream)
		{
			NetworkStream = stream;
		}

		public int Read (byte[] msg, int p1, int p2)
		{
			return NetworkStream.Read (msg,p1,p2);
		}

		public void Write (byte[] msg, int p1, int p2)
		{
			NetworkStream.Write (msg, p1, p2);
		}

		public void Dispose ()
		{
			NetworkStream.Dispose ();
		}
	}
}


using System;
using System.Net;
using System.Net.Sockets;

namespace PiggySync.Core.Concrete
{
	public class TcpSocket : IDisposable
    {
		Socket socket;
        
		public TcpSocket(IPEndPoint iPEndPoint)
        {
			this.socket = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			socket.Bind (iPEndPoint);
        }

		public TcpSocket(Socket socket)
		{
			this.socket = socket;
		}

        public void Connect(IPEndPoint host)
        {
			socket.Connect (host);
        }

        public void Close()
        {
			socket.Close ();
		}

        public NetworkStream GetStream()
        {
			return new NetworkStream (socket);
		}

		public void Dispose ()
		{
			if (socket.Connected)
			{
				socket.Close ();
			}
			socket.Dispose ();
		}
    }
}
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace PiggySync.Core.Concrete
{
	public class TcpSocketListner : IDisposable
    {
		Socket socket;
		bool run;
        public TcpSocketListner(IPEndPoint iPEndPoint)
        {
			socket = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			socket.Bind (iPEndPoint);
        }

		ConcurrentQueue<Socket> sockets;

        public void Start()
        {
			run = true;
			Task.Factory.StartNew (() =>
			{
				do
				{
					try
					{
						socket.Listen (5);
						sockets.Enqueue (socket.Accept ());
					}
					catch (Exception)
					{
						Thread.Sleep (100);
					}
				} while(run);
			});
        }

        public TcpSocket AcceptTcpClient()
        {
			Socket x;
			do
			{
				Thread.Sleep (100);
			} while(!sockets.TryDequeue (out x));
			return new TcpSocket (x);
        }

        public void Stop()
        {
			run = false;
        }
			
		public void Dispose ()
		{
			foreach (var element in sockets)
			{
				element.Dispose ();
			}
		}

    }
}
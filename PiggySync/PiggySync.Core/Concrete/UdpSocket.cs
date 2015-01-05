namespace PiggySync.Core.Concrete
{
	internal class UdpSocket
	{
//		Socket socket;
//
//		public UdpSocket (int port)
//		{
//			socket = new Socket (AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
//			socket.Bind (new IPEndPoint (0, port));
//		}
//
//		public UdpSocket ()
//		{
//			socket = new Socket (AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
//		}
//
//		public bool EnableBroadcast
//		{
//			get { return socket.EnableBroadcast; }
//			set { socket.EnableBroadcast = value; }
//		}
//
//		public byte[] Receive (ref IPEndPoint source)
//		{
//			int buffSize = 256;
//			var buffer = new byte[buffSize];
//			do
//			{
//				var endPoint = (source as EndPoint);
//				try
//				{
//					var recLen = socket.ReceiveFrom (buffer, ref endPoint);
//					return buffer.SubArray (0, recLen);
//				}
//				catch (Exception)
//				{
//				}
//				buffSize = buffSize * 2;
//				buffer = new byte[buffSize];
//			} while (buffer.Length < 10000);
//			throw new SocketException ();
//		}
//
//		public void Send (byte[] msg, int length, IPEndPoint hostAddr)
//		{
//			if (EnableBroadcast)
//			{
//				socket.SendTo (msg, length, SocketFlags.Broadcast, hostAddr);
//			}
//			else
//			{
//				socket.SendTo (msg, length, SocketFlags.None, hostAddr);
//			}
//		}
	}
}
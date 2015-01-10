using System;
using PiggySync.Common;
using System.Net.Sockets;

namespace PiggySync.StandardTypeResolver
{
	public class Resolver : ITypeResolver
	{
		public IIPEndPoint IPEndPoint (IIPAddress iPAddress, int port)
		{
			return new StandardIpEndPoint (iPAddress, port);
		}

		public IUdpClient UdpClient ()
		{
			return new StandardUdpClient ();
		}

		public IUdpClient UdpClient (int p)
		{
			return new StandardUdpClient (p);
		}

		public ITcpListener TcpListener (IIPEndPoint iIPEndPoint)
		{
			return new StandardTcpListener ((StandardIpEndPoint)iIPEndPoint);
		}

		public ITcpClient TcpClient (IIPEndPoint iIPEndPoint)
		{
			return new StandardTcpClient ((StandardIpEndPoint)iIPEndPoint);
		}

		public ISslStream SslStream (INetworkStream stream)
		{
			return new StandardSslStream (stream);
		}

		public IFileInfo FileInfo (string path)
		{
			return new StandarFileInfo (path);
		}

		IIPHelper ipHelper = new PiggySync.StandardTypeResolver.IpHelper ();
		public IIPHelper IpHelper
		{
			get
			{
				return  ipHelper;
			}
		}

		DirectoryHelper dirHelper = new DirectoryHelper ();
		public IDirectoryHelper EnviromentHelper
		{
			get
			{
				return dirHelper;
			}
		}

		ThreadHelper thrdHelper = new ThreadHelper();
		public IThreadHelper ThreadHelper
		{
			get
			{
				return thrdHelper;
			}
		}

		public IDirectoryHelper DirectoryHelper
		{
			get
			{
				return dirHelper;
			}
		}
	}
}


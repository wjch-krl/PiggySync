using System;

namespace PiggySync.Common
{
	public interface ITypeResolver
	{
		IIPHelper IpHelper { get;}
		IDirectoryHelper EnviromentHelper { get;}
		IThreadHelper ThreadHelper { get;}
		IDirectoryHelper DirectoryHelper { get; }

		IIPEndPoint IPEndPoint (IIPAddress iPAddress, int port);

		IUdpClient UdpClient();

		IUdpClient UdpClient (int p);

		ITcpListener TcpListener (Common.IIPEndPoint iIPEndPoint);

		ITcpClient TcpClient (IIPEndPoint iIPEndPoint);

		ISslStream SslStream (INetworkStream stream);

		IFileInfo FileInfo (string path);

		Imd5 Md5 ();

        IFileWather FileWather { get; set; }
	}
}


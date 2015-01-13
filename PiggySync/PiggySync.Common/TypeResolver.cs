using System;
using PiggySync.Common.Abstract;
using System.Threading.Tasks;
using System.IO;

namespace PiggySync.Common
{
	public class TypeResolver
	{
		public static ITypeResolver Factory { private get; set; }

		public static IIPHelper IpHelper
		{
			get { return Factory.IpHelper; }
		}

		public static IDirectoryHelper EnviromentHelper
		{
			get { return Factory.DirectoryHelper; }
		}

		public static IThreadHelper ThreadHelper
		{
			get { return Factory.ThreadHelper; }
		}

		public static IDirectoryHelper DirectoryHelper
		{
			get { return Factory.DirectoryHelper; }
		}

		public static IIPEndPoint IPEndPoint (IIPAddress iPAddress, int port)
		{
			return Factory.IPEndPoint (iPAddress, port);
		}

		public static IUdpClient UdpClient ()
		{
			return Factory.UdpClient ();
		}

		public static IUdpClient UdpClient (int p)
		{
			return Factory.UdpClient (p);
		}

		public static ITcpListener TcpListener (Common.IIPEndPoint iIPEndPoint)
		{
			return Factory.TcpListener (iIPEndPoint);
		}

		public static ITcpClient TcpClient (IIPEndPoint iIPEndPoint)
		{
			return Factory.TcpClient (iIPEndPoint);
		}

		public static ISslStream SslStream (INetworkStream stream)
		{
			return Factory.SslStream (stream);
		}

		public static IFileInfo FileInfo (string path)
		{
			return Factory.FileInfo (path);
		}

		public static Imd5 Md5 ()
		{
			return Factory.Md5 ();
		}
	}
}

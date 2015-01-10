using System;
using PiggySync.Common;
using System.Net.Security;

namespace PiggySync.StandardTypeResolver
{
	public class StandardSslStream : ISslStream
	{
		internal SslStream SslStream { get; private set; }

		public StandardSslStream (INetworkStream stream)
		{
			SslStream = new SslStream (((StandardNetworkStream)stream).NetworkStream);
		}

//		public void AuthenticateAsServer (X509Certificate serverCert, bool clientAuth, SslProtocols protocols, bool serverAuth)
//		{
//			SslStream.AuthenticateAsServer (serverCert, clientAuth, System.Security.Authentication.SslProtocols.Default, serverAuth);
//		}

		public void Write (byte[] msg, int i, int length)
		{
			SslStream.Write (msg, i, length);
		}

		public int Read (byte[] msg, int i, int length)
		{
			return SslStream.Read (msg, i, length);
		}

//		public void AuthenticateAsClient (X509Certificate serverCert)
//		{
//			//SslStream.AuthenticateAsClient (serverCert);
//		}

		public void Dispose ()
		{
			SslStream.Dispose ();
		}
	}
}


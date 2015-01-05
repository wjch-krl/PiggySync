using System;
using Org.BouncyCastle.X509;

namespace PiggySync.Common
{
    public interface ISslStream : IDisposable
    {
        void AuthenticateAsServer(X509Certificate serverCert, bool clientAuth, SslProtocols protocols, bool serverAuth);
        void Write(byte[] msg, int i, int length);
        int Read(byte[] msg, int i, int length);
        void AuthenticateAsClient(X509Certificate serverCert);
    }
}
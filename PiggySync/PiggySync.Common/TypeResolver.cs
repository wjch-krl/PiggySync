using System;
using PiggySync.Common.Abstract;

namespace PiggySync.Common
{
    public class TypeResolver
    {
        public static IIPHelper IpHelper { get {  throw  new NotImplementedException();} }
        public static IDirectoryHelper EnviromentHelper { get; private set; }
        public static IThreadHelper ThreadHelper { get {throw new NotImplementedException();}}
        public static IDirectoryHelper DirectoryHelper { get; set; }

        public static IIPEndPoint IPEndPoint(IIPAddress iPAddress, int port)
        {
            throw new NotImplementedException();
        }

        public static IUdpClient UdpClient()
        {
            throw new NotImplementedException();
        }

        public static IUdpClient UdpClient(int p)
        {
            throw new NotImplementedException();
        }

        public static ITcpListener TcpListener(Common.IIPEndPoint iIPEndPoint)
        {
            throw new NotImplementedException();
        }

        public static ITcpClient TcpClient(IIPEndPoint iIPEndPoint)
        {
            throw new NotImplementedException();
        }

        public static ISslStream SslStream(INetworkStream getStream)
        {
            throw new NotImplementedException();
        }
    }
}

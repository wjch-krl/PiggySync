using System;
using System.Net;

namespace PiggySync.Core.Concrete
{
    internal class UdpSocket
    {
        public UdpSocket(int i)
        {
            throw new NotImplementedException();
        }

        public UdpSocket()
        {
            throw new NotImplementedException();
        }

        public bool EnableBroadcast { get; set; }

        public byte[] Receive(ref IPEndPoint source)
        {
            throw new NotImplementedException();
        }

        public void Send(byte[] msg, int length, IPEndPoint hostAddr)
        {
            throw new NotImplementedException();
        }
    }
}
using System;

namespace PiggySync.Common
{
    public interface ITcpClient : IDisposable
    {
        INetworkStream GetStream();

        void Connect(IIPEndPoint host);
    }
}
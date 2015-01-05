using System;

namespace PiggySync.Common
{
    public interface INetworkStream : IDisposable
    {
        int Read(byte[] msg, int p1, int p2);

        void Write(byte[] msg, int p1, int p2);
    }
}

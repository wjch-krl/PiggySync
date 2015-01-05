namespace PiggySync.Common
{
    public interface IUdpClient
    {
        byte[] Receive(ref IIPEndPoint source);

        bool EnableBroadcast { get; set; }

        void Send(byte[] msg, int p, IIPEndPoint destination);
    }
}
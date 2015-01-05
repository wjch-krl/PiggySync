namespace PiggySync.Common
{
    public interface ITcpListener
    {
        void Start();
        ITcpClient AcceptTcpClient();
        void Stop();
    }
}
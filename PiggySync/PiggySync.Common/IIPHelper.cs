namespace PiggySync.Common
{
    public interface IIPHelper
    {
        IIPAddress Create(byte[] adrBytes);
        IIPAddress Any { get; }
        IIPAddress Broadcast { get; }
        IIPAddress LocalIp { get; }
    }
}
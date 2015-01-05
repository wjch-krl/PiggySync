namespace PiggySync.Common
{
    public interface IIPHelper
    {
        IIPAddress Create(byte[] adrBytes);
        IIPAddress Any { get; set; }
        IIPAddress Broadcast { get; set; }
        IIPAddress LocalIp { get; set; }
    }
}
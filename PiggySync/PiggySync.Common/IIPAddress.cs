namespace PiggySync.Common
{
    public interface IIPAddress
    {
        byte[] GetAddressBytes();
        bool AddressFamilyIsInterNetwork { get; set; }
    }
}

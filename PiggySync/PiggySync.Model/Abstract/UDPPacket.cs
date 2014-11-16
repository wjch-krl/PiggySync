namespace PiggySync.Model.Abstract
{
    public abstract class UdpPacket
    {
        public UdpPacket(byte code)
        {
            Code = code;
        }

        internal byte Code { get; set; }

        public abstract byte[] GetPacket();
    }
}
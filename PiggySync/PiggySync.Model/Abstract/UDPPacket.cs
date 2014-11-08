namespace PiggySync.Model.Abstract
{
    public abstract class UdpPacket 
    {
		internal byte Code { get; set; }

        public UdpPacket(byte code)
        {
            this.Code = code;
        }

        public abstract byte[] GetPacket();

    }

}

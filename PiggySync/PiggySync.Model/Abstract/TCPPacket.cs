namespace PiggySync.Model.Abstract
{
    public abstract class TCPPacket
    {
        internal byte Code;

        public TCPPacket(byte code)
        {
            this.Code = code;
        }

        public abstract byte[] GetPacket();
    }
}

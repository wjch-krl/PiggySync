namespace PiggySync.Model.Abstract
{
    public abstract class TCPPacket
    {
        internal byte Code;

        public TCPPacket(byte code)
        {
            Code = code;
        }

        public abstract byte[] GetPacket();
    }
}
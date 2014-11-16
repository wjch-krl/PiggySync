using PiggySync.Model.Abstract;

namespace PiggySync.Model.Concrete
{
    public class NoRequestPacket : TCPPacket
    {
        public const byte NoRequestPacketCode = 160;

        public NoRequestPacket()
            : base(NoRequestPacketCode)
        {
        }

        public override byte[] GetPacket()
        {
            return new[] {NoRequestPacketCode};
        }
    }
}
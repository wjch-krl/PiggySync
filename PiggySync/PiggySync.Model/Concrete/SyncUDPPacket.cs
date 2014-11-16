using System;
using System.Linq;
using PiggySync.Model.Abstract;

namespace PiggySync.Model.Concrete
{
    public class SyncUDPPacket : UdpPacket
    {
        private readonly UInt32 seqNumber;

        protected SyncUDPPacket(UInt32 seqNumber, byte code)
            : base(code)
        {
            this.seqNumber = seqNumber;
        }

        public override byte[] GetPacket()
        {
            byte[] packet = {Code};
            packet = packet.Concat(BitConverter.GetBytes(seqNumber)).ToArray();
            packet = packet.Concat(Discovery.Name).ToArray();
            return packet;
        }
    }
}
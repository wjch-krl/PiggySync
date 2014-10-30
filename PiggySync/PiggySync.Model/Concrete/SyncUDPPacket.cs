using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggySyncWin.WinUI.Models
{
    public class SyncUDPPacket : UDPPacket
    {
        private UInt32 seqNumber;
        protected SyncUDPPacket(UInt32 seqNumber,byte code)
            : base(code)
        {
            this.seqNumber = seqNumber;
        }

        public override byte[] GetPacket()
        {
            byte[] packet = new byte[] { Code };
            packet = packet.Concat(BitConverter.GetBytes(seqNumber)).ToArray();
            packet = packet.Concat(Discovery.Name).ToArray();
            return packet;
        }
    }
}

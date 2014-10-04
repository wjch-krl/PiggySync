using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggySyncWin.WinUI.Models
{
    public class SyncUDPPacket : UDPPacket
    {
        private uint seqNumber;
        protected SyncUDPPacket(uint seqNumber,byte code)
            : base(code)
        {
            this.seqNumber = seqNumber;
        }

        public override byte[] GetPacket()
        {
            byte[] packet = new byte[] { code };
            packet = packet.Concat(BitConverter.GetBytes(seqNumber)).ToArray();
            packet = packet.Concat(Discovery.Name).ToArray();
            return packet;
        }
    }
}

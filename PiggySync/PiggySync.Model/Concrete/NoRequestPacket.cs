using PiggySyncWin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggySyncWin.WinUI.Models
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
			return new byte[] { NoRequestPacketCode };
        }

    }
}

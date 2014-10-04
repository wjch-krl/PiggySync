using PiggySyncWin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggySyncWin.WinUI.Models
{
    class NoRequestPacket : TCPPacket
    {
        public const byte Code = 160;

        public NoRequestPacket()
            : base(Code)
        {

        }

        public override byte[] GetPacket()
        {
            return new byte[] { code };
        }

    }
}

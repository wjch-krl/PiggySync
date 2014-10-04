using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggySyncWin.Models
{
    public abstract class TCPPacket
    {
        internal byte code;

        public TCPPacket(byte code)
        {
            this.code = code;
        }

        public abstract byte[] GetPacket();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggySyncWin.WinUI.Models
{
    public abstract class UDPPacket 
    {
        internal byte code;

        public UDPPacket(byte code)
        {
            this.code = code;
        }

        public abstract byte[] GetPacket();

    }

}

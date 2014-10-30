using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggySyncWin.WinUI.Models
{
    public abstract class UDPPacket 
    {
		internal byte Code { get; set; }

        public UDPPacket(byte code)
        {
            this.Code = code;
        }

        public abstract byte[] GetPacket();

    }

}

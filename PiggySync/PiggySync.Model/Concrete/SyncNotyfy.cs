using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggySyncWin.WinUI.Models
{
    public class SyncNotyfy : SyncUDPPacket
    {
        public SyncNotyfy(UInt32 seqNumber)
            : base(seqNumber, 170)
        {
        }
    }
}

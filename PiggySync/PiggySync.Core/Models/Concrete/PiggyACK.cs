using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggySyncWin.WinUI.Models
{
    class PiggyACK : SyncUDPPacket
    {
        public PiggyACK(UInt32 seqNumber)
            : base(seqNumber, 0)
        {
        }
    }
}

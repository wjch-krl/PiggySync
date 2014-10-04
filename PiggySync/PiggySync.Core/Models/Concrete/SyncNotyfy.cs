using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggySyncWin.WinUI.Models
{
    public class SyncNotyfy : SyncUDPPacket
    {
        public SyncNotyfy(uint seqNumber)
            : base(seqNumber, 170)
        {
        }
    }
}

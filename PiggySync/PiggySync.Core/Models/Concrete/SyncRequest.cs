using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggySyncWin.WinUI.Models.Concrete
{
    class SyncRequest : SyncUDPPacket
    {
        public SyncRequest(uint seqNumber)
            : base(seqNumber, 255)
        {

        }
    }
}

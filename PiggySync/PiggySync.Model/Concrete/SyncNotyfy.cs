using System;

namespace PiggySync.Model.Concrete
{
    public class SyncNotyfy : SyncUDPPacket
    {
        public SyncNotyfy(UInt32 seqNumber)
            : base(seqNumber, 170)
        {
        }
    }
}
using System;

namespace PiggySync.Model.Concrete
{
    public class SyncRequest : SyncUDPPacket
    {
        public SyncRequest(UInt32 seqNumber)
            : base(seqNumber, 255)
        {
        }
    }
}
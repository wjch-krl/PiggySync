using System;

namespace PiggySync.Model.Concrete
{
    public class PiggyACK : SyncUDPPacket
    {
        public PiggyACK(UInt32 seqNumber)
            : base(seqNumber, 0)
        {
        }
    }
}
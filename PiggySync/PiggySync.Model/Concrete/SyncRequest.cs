﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggySyncWin.WinUI.Models.Concrete
{
	public class SyncRequest : SyncUDPPacket
    {
        public SyncRequest(UInt32 seqNumber)
            : base(seqNumber, 255)
        {

        }
    }
}
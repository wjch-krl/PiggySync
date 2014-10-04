using PiggySyncWin.WinUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PiggySyncWin.WinUI.Infrastructure
{
    public class PiggyRemoteHostHistoryEntry : PiggyRemoteHost
    {
        public PiggyRemoteHostHistoryEntry(PiggyRemoteHost x) : base(x.Ip,x.Name)
        {
        }

        public DateTime LastSync
        {
            get;
            set;
        }
    }
}

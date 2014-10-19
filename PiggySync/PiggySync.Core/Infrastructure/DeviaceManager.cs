using PiggySyncWin.WinUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PiggySyncWin.WinUI.Infrastructure
{
    public class DeviaceHistoryManager
    {
        static List<PiggyRemoteHostHistoryEntry> allHosts;

        public static List<PiggyRemoteHostHistoryEntry> AllHosts
        {
            get { return allHosts; }
            set { allHosts = value; }
        }

        public static void Initialize(){
            allHosts = new List<PiggyRemoteHostHistoryEntry>();
        }

        public static void AddHost(PiggyRemoteHost host)
        {
            foreach (var x in allHosts)
            {
                if (x == host)
                {
                    x.LastSync = DateTime.Now;
                    return;
                }
            }
            var newHost = new PiggyRemoteHostHistoryEntry(host);
            newHost.LastSync = DateTime.Now;
            allHosts.Add(newHost);
        }
    }
}
using System;
using System.Collections.Generic;
using PiggySync.Model;

namespace PiggySync.Core
{
    public static class DeviaceHistoryManager
    {
        static DeviaceHistoryManager()
        {
            AllHosts = DatabaseManager.Instance.GetHistoricalDeviaces();
        }

        public static List<PiggyRemoteHostHistoryEntry> AllHosts { get; set; }

        public static void AddHost(PiggyRemoteHost host)
        {
            foreach (var x in AllHosts)
            {
                if (x == host)
                {
                    x.LastSync = DateTime.Now;
                    DatabaseManager.Instance.UpdateHistoricalDeviace(x);
                    return;
                }
            }
            var newHost = new PiggyRemoteHostHistoryEntry(host);
            newHost.LastSync = DateTime.Now;
            DatabaseManager.Instance.SaveHistoricalDeviace(newHost);

            AllHosts.Add(newHost);
        }
    }
}
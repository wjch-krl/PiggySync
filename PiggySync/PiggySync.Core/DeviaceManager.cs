﻿using System;
using System.Collections.Generic;
using PiggySync.Model;
using PiggySyncWin.WinUI.Infrastructure;
using PiggySyncWin.WinUI.Models;

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
            foreach (PiggyRemoteHostHistoryEntry x in AllHosts)
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
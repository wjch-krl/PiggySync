using PiggySyncWin.WinUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PiggySync.DatabaseManager;

namespace PiggySyncWin.WinUI.Infrastructure
{
	public static class DeviaceHistoryManager
	{
		public static List<PiggyRemoteHostHistoryEntry> AllHosts
		{
			get;
			set;
		}

		static DeviaceHistoryManager ()
		{
			AllHosts = DatabaseManager.Instance.GetHistoricalDeviaces ();
		}

		public static void AddHost (PiggyRemoteHost host)
		{
			foreach (var x in AllHosts)
			{
				if (x == host)
				{
					x.LastSync = DateTime.Now;
					DatabaseManager.Instance.UpdateHistoricalDeviace (x);
					return;
				}
			}
			var newHost = new PiggyRemoteHostHistoryEntry (host);
			newHost.LastSync = DateTime.Now;
			DatabaseManager.Instance.SaveHistoricalDeviace (newHost);

			AllHosts.Add (newHost);
		}
	}
}
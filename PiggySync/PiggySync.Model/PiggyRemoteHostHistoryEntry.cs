using PiggySyncWin.WinUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

namespace PiggySyncWin.WinUI.Infrastructure
{
	public class PiggyRemoteHostHistoryEntry : PiggyRemoteHost
	{
		public PiggyRemoteHostHistoryEntry (PiggyRemoteHost x) : base (x.Ip, x.Name)
		{
		}

		public PiggyRemoteHostHistoryEntry():base(null,null)
		{

		}

		[PrimaryKey, AutoIncrement]
		public int Id
		{
			get;
			set;
		}

		public DateTime LastSync
		{
			get;
			set;
		}
	}
}

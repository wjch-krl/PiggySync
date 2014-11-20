using System;
using System.Collections.Generic;

namespace PiggySync.GuiShared
{
	public interface IMainView
	{
		/// <summary>
		/// Sets hosts list.
		/// </summary>
		/// <value>Conntected hosts.</value>
		double ProgresLevel { get; set; }

		/// <summary>
		/// Gets or sets the sync status.
		/// </summary>
		/// <value>The sync status.</value>
		SyncStatus SyncStatus { get; set; }
	}
}


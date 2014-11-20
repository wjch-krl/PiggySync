using System;
using System.Collections.Generic;
using PiggySync.Model;
using PiggySync.Core;

namespace PiggySync.GuiShared
{
	public interface IHostView
	{
		/// <summary>
		/// Sets hosts list.
		/// </summary>
		/// <value>Conntected hosts.</value>
		IEnumerable<string> Hosts{ set; }
	}
}


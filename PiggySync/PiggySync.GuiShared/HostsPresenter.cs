using System;
using System.Collections.Generic;
using PiggySync.Model;
using PiggySync.Core;
using System.Linq;

namespace PiggySync.GuiShared
{
	public class HostsPresenter : IHostWather
	{
		IHostView view;
		public HostsPresenter (IHostView view)
		{
			this.view = view;
		}

		public void RefreshHostsList (IEnumerable<PiggyRemoteHost> hosts)
		{
			view.Hosts = hosts.Select ( x => x.GetShortName ());
		}	
	}
}


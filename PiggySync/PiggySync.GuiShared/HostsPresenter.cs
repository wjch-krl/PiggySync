using System;
using System.Collections.Generic;
using PiggySync.Model;
using PiggySync.Core;
using System.Linq;
using PiggySync.Domain;
using PiggySync.Domain.Concrete;

namespace PiggySync.GuiShared
{
	public class HostsPresenter : IHostWather
	{
		IHostView view;
		public HostsPresenter (IHostView view)
		{
			this.view = view;
			view.Hosts = DeviaceHistoryManager.AllHosts;
		}

		public void RefreshHostsList (IEnumerable<PiggyRemoteHost> hosts)
		{
			view.Hosts = hosts.Concat (DeviaceHistoryManager.AllHosts);
		}	
	}
}


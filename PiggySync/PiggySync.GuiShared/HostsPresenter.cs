using System.Collections.Generic;
using System.Linq;
using PiggySync.Core;
using PiggySync.Model;

namespace PiggySync.GuiShared
{
    public class HostsPresenter : IHostWather
    {
        private readonly IHostView view;

        public HostsPresenter(IHostView view)
        {
            this.view = view;
            view.Hosts = DeviaceHistoryManager.AllHosts;
        }

        public void RefreshHostsList(IEnumerable<PiggyRemoteHost> hosts)
        {
            view.Hosts = hosts.Concat(DeviaceHistoryManager.AllHosts);
        }
    }
}
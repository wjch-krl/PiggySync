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
            SyncManager.HostObserver = this;
            view.Hosts = DeviaceHistoryManager.AllHosts;
        }

        public void RefreshHostsList(IEnumerable<PiggyRemoteHost> hosts)
        {
            view.Hosts = hosts.Concat(DeviaceHistoryManager.AllHosts);
        }

        public void UnSucribe()
        {
            if (SyncManager.HostObserver == this)
            {
                SyncManager.HostObserver = null;
            }
        }
    }
}
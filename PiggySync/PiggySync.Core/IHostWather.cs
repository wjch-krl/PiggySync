using System.Collections.Generic;
using PiggySync.Model;

namespace PiggySync.Core
{
    public interface IHostWather
    {
        void RefreshHostsList(IEnumerable<PiggyRemoteHost> hosts);
    }
}
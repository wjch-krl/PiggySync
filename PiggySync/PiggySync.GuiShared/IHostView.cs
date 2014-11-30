using System.Collections.Generic;
using PiggySync.Model;

namespace PiggySync.GuiShared
{
    public interface IHostView
    {
        /// <summary>
        ///     Sets hosts list.
        /// </summary>
        /// <value>Conntected hosts.</value>
        IEnumerable<PiggyRemoteHost> Hosts { set; }
    }
}
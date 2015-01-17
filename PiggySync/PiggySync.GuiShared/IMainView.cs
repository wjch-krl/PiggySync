using PiggySync.Common;
using PiggySync.Model;

namespace PiggySync.GuiShared
{
    public interface IMainView
    {
        /// <summary>
        ///     Sets hosts list.
        /// </summary>
        /// <value>Conntected hosts.</value>
        double ProgresLevel { get; set; }

        /// <summary>
        ///     Gets or sets the sync status.
        /// </summary>
        /// <value>The sync status.</value>
        SyncStatus SyncStatus { set; }

		/// <summary>
		/// Sets a value indicating whether this <see cref="PiggySync.GuiShared.IMainView"/> progres enabled.
		/// </summary>
		/// <value><c>true</c> if progres enabled; otherwise, <c>false</c>.</value>
		bool ProgresEnabled { set; }
    }
}
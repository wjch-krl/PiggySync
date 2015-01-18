using System.Collections.Generic;
using PiggySync.Domain;

namespace PiggySync.GuiShared
{
    public interface ISettingsView
    {
        /// <summary>
        ///     Gets or sets the sync root path.
        /// </summary>
        /// <value>The sync root path.</value>
        string SyncRootPath { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="PiggySync.GtkGui.IMainView" /> use tcp.
        /// </summary>
        /// <value><c>true</c> if use tcp; otherwise, <c>false</c>.</value>
        bool UseTcp { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="PiggySync.GtkGui.IMainView" /> use encryption.
        /// </summary>
        /// <value><c>true</c> if use encryption; otherwise, <c>false</c>.</value>
        bool UseEncryption { get; set; }

        /// <summary>
        ///     Gets or sets the name of the computer.
        /// </summary>
        /// <value>The name of the computer.</value>
        string ComputerName { get; set; }

        /// <summary>
        ///     Gets or sets the text files.
        /// </summary>
        /// <value>The text files.</value>
        IEnumerable<TextFile> TextFiles { get; set; }

		/// <summary>
		/// Gets or sets the banned files.
		/// </summary>
		/// <value>The banned files.</value>
        IEnumerable<string> BannedFiles { get; set; }

		/// <summary>
		/// Gets or sets the keep deleted info.
		/// </summary>
		/// <value>The keep deleted info.</value>
        int KeepDeletedInfo { get; set; }
    }
}
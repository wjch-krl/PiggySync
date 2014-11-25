using System;
using System.Collections.Generic;

namespace PiggySync.Domain
{
    [Serializable]
    public class Settings
    {
        /// <summary>
        ///     Gets or sets the sync root path.
        /// </summary>
        /// <value>The sync root path.</value>
        public string SyncRootPath { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="PiggySync.GtkGui.IMainView" /> auto sync.
        /// </summary>
        /// <value><c>true</c> if auto sync; otherwise, <c>false</c>.</value>
        public bool AutoSync { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="PiggySync.GtkGui.IMainView" /> use tcp.
        /// </summary>
        /// <value><c>true</c> if use tcp; otherwise, <c>false</c>.</value>
        public bool UseTcp { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="PiggySync.GtkGui.IMainView" /> use encryption.
        /// </summary>
        /// <value><c>true</c> if use encryption; otherwise, <c>false</c>.</value>
        public bool UseEncryption { get; set; }

        /// <summary>
        ///     Gets or sets the name of the computer.
        /// </summary>
        /// <value>The name of the computer.</value>
        public string ComputerName { get; set; }

        /// <summary>
        ///     Gets or sets the text files.
        /// </summary>
        /// <value>The text files.</value>
		public List<TextFile> TextFiles { get; set; }
    }
}
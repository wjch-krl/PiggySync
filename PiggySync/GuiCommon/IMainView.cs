using System;
using System.Collections.Generic;
using PiggySyncWin.Domain;
using PiggySyncWin.Domain.Concrete;

namespace PiggySync.GuiCommon
{
	public interface IMainView
	{
		/// <summary>
		/// Gets or sets the sync root path.
		/// </summary>
		/// <value>The sync root path.</value>
		string SyncRootPath  { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="PiggySync.GtkGui.IMainView"/> auto sync.
		/// </summary>
		/// <value><c>true</c> if auto sync; otherwise, <c>false</c>.</value>
		bool AutoSync { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="PiggySync.GtkGui.IMainView"/> use tcp.
		/// </summary>
		/// <value><c>true</c> if use tcp; otherwise, <c>false</c>.</value>
		bool UseTcp { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="PiggySync.GtkGui.IMainView"/> use encryption.
		/// </summary>
		/// <value><c>true</c> if use encryption; otherwise, <c>false</c>.</value>
		bool UseEncryption { get; set; }
		/// <summary>
		/// Gets or sets the name of the computer.
		/// </summary>
		/// <value>The name of the computer.</value>
		string ComputerName{ get; set; }
		/// <summary>
		/// Gets or sets the text files.
		/// </summary>
		/// <value>The text files.</value>
		IEnumerable<TextFile> TextFiles{ get; set; }
		/// <summary>
		/// Sets hosts list.
		/// </summary>
		/// <value>Conntected hosts.</value>
		IEnumerable<string> Hosts{ set; }
		/// <summary>
		/// Gets or sets the progres level.(0.0 started 1.0 finished)
		/// </summary>
		/// <value>The progres level.</value>
		double ProgresLevel { get; set; }
		/// <summary>
		/// Indicated end of an action.
		/// </summary>
		void ActionFinished ();
		/// <summary>
		/// Indicated start of an action (View should be inactive).
		/// </summary>
		void ActionStart ();
		/// <summary>
		/// Indicated end of an action.
		/// </summary>
		/// <param name="dialogText">Text of dialog that will be displayed.</param>
		void ActionFinished (string dialogText);

	}

}


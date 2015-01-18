
using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using PiggySync.GuiShared;
using System.IO;
using PiggySync.Domain;

namespace PiggySync.MonoMacGui
{
	public partial class SettingsWindowController : MonoMac.AppKit.NSWindowController, ISettingsView
	{
		#region Constructors

		// Called when created from unmanaged code
		public SettingsWindowController (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public SettingsWindowController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		// Call to load from the XIB/NIB file
		public SettingsWindowController () : base ("SettingsWindow")
		{
			Initialize ();
		}
		
		// Shared initialization code
		void Initialize ()
		{
		}

		#endregion

		SettingsPresenter presenter;

		public override void WindowDidLoad ()
		{
			base.WindowDidLoad ();
			this.presenter = new SettingsPresenter (this);
			CancelButton.Activated += (s, e) => this.Close ();
			SaveButton.Activated += (s, e) => presenter.SaveSettings ();
		//	NewFileButton.Activated += (sender, e) => TextFilesGrid.; 
		}

		//strongly typed window accessor
		public new SettingsWindow Window
		{
			get
			{
				return (SettingsWindow)base.Window;
			}
		}
			
		public string SyncRootPath
		{
			get
			{
				return SyncRootPathTextField.StringValue;
			}
			set
			{
				SyncRootPathTextField.StringValue = value;
			}
		}

		public bool UseTcp
		{
			get
			{
				return UseTcpChek.State == NSCellStateValue.On;
			}
			set
			{
				UseTcpChek.State = value ? NSCellStateValue.On : NSCellStateValue.Off;
			}
		}

		public string ComputerName
		{
			get
			{
				return ComputerNameTextBox.StringValue;
			}
			set
			{
				ComputerNameTextBox.StringValue = value;
			}
		}

		public IEnumerable<PiggySync.Domain.TextFile> TextFiles
		{
			get
			{
				return TextFilesTextBox.Value.Split
					(new char[0],StringSplitOptions.RemoveEmptyEntries).
						Select (x=>new TextFile{ Extension = x, });
			}
			set
			{
				TextFilesTextBox.Value = string.Join ("\n", value.Select (x => x.Extension));
			}
		}

		public bool UseEncryption
		{
			get
			{
				return EnableEncryptionCheck.State == NSCellStateValue.On;
			}
			set
			{
				EnableEncryptionCheck.State = value ? NSCellStateValue.On : NSCellStateValue.Off;
			}
		}

		public IEnumerable<string> BannedFiles
		{
			get
			{
				return ExcludedFilesBox.Value.Split (new char[0], StringSplitOptions.RemoveEmptyEntries);
			}
			set
			{
				ExcludedFilesBox.Value = string.Join ("\n", value);
			}
		}

		public int KeepDeletedInfo
		{
			get
			{
				return KeepDeletedNumberBox.IntValue;
			}
			set
			{
				KeepDeletedNumberBox.IntValue = value;
			}
		}
	}
}



using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using PiggySync.GuiShared;

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
			this.presenter = new SettingsPresenter (this);
		}

		#endregion

		SettingsPresenter presenter;

		public override void WindowDidLoad ()
		{
			base.WindowDidLoad ();
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
				throw new NotImplementedException ();
			}
			set
			{
				throw new NotImplementedException ();
			}
		}

		public bool AutoSync
		{
			get
			{
				throw new NotImplementedException ();
			}
			set
			{
				throw new NotImplementedException ();
			}
		}

		public bool UseTcp
		{
			get
			{
				throw new NotImplementedException ();
			}
			set
			{
				throw new NotImplementedException ();
			}
		}

		public bool UseEncryption
		{
			get
			{
				throw new NotImplementedException ();
			}
			set
			{
				throw new NotImplementedException ();
			}
		}

		public string ComputerName
		{
			get
			{
				throw new NotImplementedException ();
			}
			set
			{
				throw new NotImplementedException ();
			}
		}

		public IEnumerable<PiggySync.Domain.TextFile> TextFiles
		{
			get
			{
				throw new NotImplementedException ();
			}
			set
			{
				throw new NotImplementedException ();
			}
		}
	}
}


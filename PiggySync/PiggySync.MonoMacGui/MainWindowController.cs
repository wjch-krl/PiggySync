using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using PiggySync.GuiShared;
using PiggySync.MonoMacGui;
using PiggySync.Domain;

namespace PiggySync.MacApp
{
	public partial class MainWindowController : MonoMac.AppKit.NSWindowController , IMainView
	{
		#region Constructors

		// Called when created from unmanaged code
		public MainWindowController (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public MainWindowController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		// Call to load from the XIB/NIB file
		public MainWindowController () : base ("MainWindow")
		{
			Initialize ();
		}
		
		// Shared initialization code
		void Initialize ()
		{
		//	tabController.DrawsBackground = false;
		}

		#endregion

		public override void WindowDidLoad ()
		{
			base.WindowDidLoad ();
			this.SettingsButton.Activated += (sender, e) => {
				new SettingsWindowController().ShowWindow(this);
			};
		}

		//strongly typed window accessor
		public new MainWindow Window {
			get {
				return (MainWindow)base.Window;
			}
		}

		public void ActionFinished ()
		{
			throw new NotImplementedException ();
		}

		public void ActionStart ()
		{
			throw new NotImplementedException ();
		}

		public void ActionFinished (string dialogText)
		{
			throw new NotImplementedException ();
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

		public IEnumerable<TextFile> TextFiles
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

		public IEnumerable<string> Hosts
		{
			set
			{
				throw new NotImplementedException ();
			}
		}

		public double ProgresLevel
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


using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using PiggySync.GuiShared;
using PiggySync.MonoMacGui;
using PiggySync.Domain;
using System.IO;
using System.Net;
using PiggySync.Model;
using PiggySync.Common;

namespace PiggySync.MacApp
{
	public partial class MainWindowController : MonoMac.AppKit.NSWindowController , IMainView
	{
		MainPresenter presenter;

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
			this.SettingsButton.Activated += (sender, e) =>
			{
				new SettingsWindowController ().ShowWindow (this);
			};
			this.DevicesButton.Activated += (sender, e) =>
			{
				//	var p = "/Users/wojciechkrol/Documents/Praca inż/PiggySync/PiggySync.MonoMacGui/bin/Debug";
				//	new DiffWindowController (Path.Combine (p,"!LocalV~MainWindow.txt"),Path.Combine (p,"!RemoteV~MainWindow.txt"),Path.Combine (p,"MainWindow.txt")).ShowWindow(this);
				new HostsWindowController ().ShowWindow (this);
			};
			this.StatusImage.Activated += (sender, e) =>
			{
				new LogWindowController ().ShowWindow (this);
			};
			this.presenter = new MainPresenter (this);
		}

		//strongly typed window accessor
		public new MainWindow Window
		{
			get
			{
				return (MainWindow)base.Window;
			}
		}

		public double ProgresLevel
		{
			get
			{
				return SyncProgressBar.DoubleValue;
			}
			set
			{
				InvokeOnMainThread (() =>
					SyncProgressBar.DoubleValue = value
				);
			}
		}

		public SyncStatus SyncStatus
		{
			set
			{
				InvokeOnMainThread (() =>
					SyncStatusLabel.StringValue = string.Format ("Sync Status:\n{0}", value.ToString ()));
			}
		}

		public bool ProgresEnabled
		{
			set
			{
				InvokeOnMainThread (() =>
					SyncProgressBar.Hidden = !value
				);
			}
		}
	}
}


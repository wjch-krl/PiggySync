using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using PiggySync.GuiShared;
using PiggySync.Model;

namespace PiggySync.MonoMacGui
{
	public partial class HostsWindowController : MonoMac.AppKit.NSWindowController, IHostView
	{
		#region Constructors

		// Called when created from unmanaged code
		public HostsWindowController (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public HostsWindowController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		// Call to load from the XIB/NIB file
		public HostsWindowController () : base ("HostsWindow")
		{
			Initialize ();
		}
		
		// Shared initialization code
		void Initialize ()
		{
		}

		public override void WindowDidLoad ()
		{
			base.WindowDidLoad ();
			new HostsPresenter (this);
		}

		#endregion

		//strongly typed window accessor
		public new HostsWindow Window
		{
			get
			{
				return (HostsWindow)base.Window;
			}
		}


		#region IHostView implementation
		public IEnumerable<PiggyRemoteHost> Hosts
		{
			set
			{
				HostsTableView.DataSource = new HostTableDataSource(value);
			}
		}
		#endregion
	}
}


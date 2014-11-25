// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoMac.Foundation;
using System.CodeDom.Compiler;

namespace PiggySync.MonoMacGui
{
	[Register ("HostsWindowController")]
	partial class HostsWindowController
	{
		[Outlet]
		MonoMac.AppKit.NSTableView HostsTableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (HostsTableView != null) {
				HostsTableView.Dispose ();
				HostsTableView = null;
			}
		}
	}

	[Register ("HostsWindow")]
	partial class HostsWindow
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}

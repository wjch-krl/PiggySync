// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoMac.Foundation;
using System.CodeDom.Compiler;

namespace PiggySync.MacApp
{
	[Register ("MainWindowController")]
	partial class MainWindowController
	{
		[Outlet]
		MonoMac.AppKit.NSButton DevicesButton { get; set; }

		[Outlet]
		MonoMac.AppKit.NSPopover popover { get; set; }

		[Outlet]
		MonoMac.AppKit.NSViewController popoverController { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton SettingsButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (popover != null) {
				popover.Dispose ();
				popover = null;
			}

			if (popoverController != null) {
				popoverController.Dispose ();
				popoverController = null;
			}

			if (SettingsButton != null) {
				SettingsButton.Dispose ();
				SettingsButton = null;
			}

			if (DevicesButton != null) {
				DevicesButton.Dispose ();
				DevicesButton = null;
			}
		}
	}

	[Register ("MainWindow")]
	partial class MainWindow
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}

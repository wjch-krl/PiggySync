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
		MonoMac.AppKit.NSButton SettingsButton { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton StatusImage { get; set; }

		[Outlet]
		MonoMac.AppKit.NSProgressIndicator SyncProgressBar { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField SyncStatusLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (DevicesButton != null) {
				DevicesButton.Dispose ();
				DevicesButton = null;
			}

			if (SettingsButton != null) {
				SettingsButton.Dispose ();
				SettingsButton = null;
			}

			if (SyncProgressBar != null) {
				SyncProgressBar.Dispose ();
				SyncProgressBar = null;
			}

			if (SyncStatusLabel != null) {
				SyncStatusLabel.Dispose ();
				SyncStatusLabel = null;
			}

			if (StatusImage != null) {
				StatusImage.Dispose ();
				StatusImage = null;
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

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
	[Register ("SettingsWindowController")]
	partial class SettingsWindowController
	{
		[Outlet]
		MonoMac.AppKit.NSButton AutoSyncCheck { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton CancelButton { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton DeleteFileButton { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton EnableEncryptionCheck { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton NewFileButton { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton SaveButton { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField SyncRootPathTextField { get; set; }

		[Outlet]
		MonoMac.AppKit.NSScrollView TextFilesGrid { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton UseTcpChek { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (EnableEncryptionCheck != null) {
				EnableEncryptionCheck.Dispose ();
				EnableEncryptionCheck = null;
			}

			if (UseTcpChek != null) {
				UseTcpChek.Dispose ();
				UseTcpChek = null;
			}

			if (AutoSyncCheck != null) {
				AutoSyncCheck.Dispose ();
				AutoSyncCheck = null;
			}

			if (SyncRootPathTextField != null) {
				SyncRootPathTextField.Dispose ();
				SyncRootPathTextField = null;
			}

			if (TextFilesGrid != null) {
				TextFilesGrid.Dispose ();
				TextFilesGrid = null;
			}

			if (DeleteFileButton != null) {
				DeleteFileButton.Dispose ();
				DeleteFileButton = null;
			}

			if (NewFileButton != null) {
				NewFileButton.Dispose ();
				NewFileButton = null;
			}

			if (CancelButton != null) {
				CancelButton.Dispose ();
				CancelButton = null;
			}

			if (SaveButton != null) {
				SaveButton.Dispose ();
				SaveButton = null;
			}
		}
	}

	[Register ("SettingsWindow")]
	partial class SettingsWindow
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}

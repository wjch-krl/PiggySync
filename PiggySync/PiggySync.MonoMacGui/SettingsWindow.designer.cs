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
		MonoMac.AppKit.NSButton CancelButton { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField ComputerNameLabel { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField ComputerNameTextBox { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton EnableEncryptionCheck { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextView ExcludedFilesBox { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField KeepDeletedNumberBox { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton SaveButton { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField SyncRootPathTextField { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextView TextFilesTextBox { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton UseTcpChek { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CancelButton != null) {
				CancelButton.Dispose ();
				CancelButton = null;
			}

			if (ComputerNameLabel != null) {
				ComputerNameLabel.Dispose ();
				ComputerNameLabel = null;
			}

			if (ComputerNameTextBox != null) {
				ComputerNameTextBox.Dispose ();
				ComputerNameTextBox = null;
			}

			if (EnableEncryptionCheck != null) {
				EnableEncryptionCheck.Dispose ();
				EnableEncryptionCheck = null;
			}

			if (ExcludedFilesBox != null) {
				ExcludedFilesBox.Dispose ();
				ExcludedFilesBox = null;
			}

			if (KeepDeletedNumberBox != null) {
				KeepDeletedNumberBox.Dispose ();
				KeepDeletedNumberBox = null;
			}

			if (SaveButton != null) {
				SaveButton.Dispose ();
				SaveButton = null;
			}

			if (SyncRootPathTextField != null) {
				SyncRootPathTextField.Dispose ();
				SyncRootPathTextField = null;
			}

			if (TextFilesTextBox != null) {
				TextFilesTextBox.Dispose ();
				TextFilesTextBox = null;
			}

			if (UseTcpChek != null) {
				UseTcpChek.Dispose ();
				UseTcpChek = null;
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

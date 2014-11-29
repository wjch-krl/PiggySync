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
	[Register ("DiffWindowController")]
	partial class DiffWindowController
	{
		[Outlet]
		MonoMac.AppKit.NSTextField LocalDateLabel { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextView LocalTextView { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField MergedLabel { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextView MergedTextView { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField RemoteDateLabel { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextView RemoteTextView { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton SaveButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (LocalDateLabel != null) {
				LocalDateLabel.Dispose ();
				LocalDateLabel = null;
			}

			if (LocalTextView != null) {
				LocalTextView.Dispose ();
				LocalTextView = null;
			}

			if (MergedLabel != null) {
				MergedLabel.Dispose ();
				MergedLabel = null;
			}

			if (MergedTextView != null) {
				MergedTextView.Dispose ();
				MergedTextView = null;
			}

			if (RemoteDateLabel != null) {
				RemoteDateLabel.Dispose ();
				RemoteDateLabel = null;
			}

			if (RemoteTextView != null) {
				RemoteTextView.Dispose ();
				RemoteTextView = null;
			}

			if (SaveButton != null) {
				SaveButton.Dispose ();
				SaveButton = null;
			}
		}
	}

	[Register ("DiffWindow")]
	partial class DiffWindow
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}

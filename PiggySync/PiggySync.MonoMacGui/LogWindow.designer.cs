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
	[Register ("LogWindowController")]
	partial class LogWindowController
	{
		[Outlet]
		MonoMac.AppKit.NSTextView LogTextField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (LogTextField != null) {
				LogTextField.Dispose ();
				LogTextField = null;
			}
		}
	}

	[Register ("LogWindow")]
	partial class LogWindow
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}

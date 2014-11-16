// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace IosGui
{
	[Register ("FirstViewController")]
	partial class FirstViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIImageView ImageStatus { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIProgressView ProgressBar { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton StatusImageButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel StatusLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton SyncNowButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ImageStatus != null) {
				ImageStatus.Dispose ();
				ImageStatus = null;
			}

			if (ProgressBar != null) {
				ProgressBar.Dispose ();
				ProgressBar = null;
			}

			if (StatusLabel != null) {
				StatusLabel.Dispose ();
				StatusLabel = null;
			}

			if (SyncNowButton != null) {
				SyncNowButton.Dispose ();
				SyncNowButton = null;
			}

			if (StatusImageButton != null) {
				StatusImageButton.Dispose ();
				StatusImageButton = null;
			}
		}
	}
}

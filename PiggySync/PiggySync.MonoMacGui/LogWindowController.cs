using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using PiggySync.GuiShared;

namespace PiggySync.MonoMacGui
{
	public partial class LogWindowController : MonoMac.AppKit.NSWindowController, ILogView
	{
		#region Constructors

		// Called when created from unmanaged code
		public LogWindowController (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public LogWindowController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		// Call to load from the XIB/NIB file
		public LogWindowController () : base ("LogWindow")
		{
			Initialize ();
		}
		
		// Shared initialization code
		void Initialize ()
		{
		}

		#endregion

		//strongly typed window accessor
		public new LogWindow Window
		{
			get
			{
				return (LogWindow)base.Window;
			}
		}
	}
}


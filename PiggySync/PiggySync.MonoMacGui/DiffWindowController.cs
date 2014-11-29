
using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using PiggySync.GuiShared;

namespace PiggySync.MonoMacGui
{
	public partial class DiffWindowController : MonoMac.AppKit.NSWindowController, IDiffView
	{
		#region Constructors
		DiffPresenter presenter;
		string fileAPath;
		string fileBPath;
		string resultPath;

		// Called when created from unmanaged code
		public DiffWindowController (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public DiffWindowController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		// Call to load from the XIB/NIB file
		public DiffWindowController (string fileA,string fileB,string resultFile) : base ("DiffWindow")
		{
			Initialize ();
			this.fileAPath = fileA;
			this.fileBPath = fileB;
			this.resultPath = resultFile;
		}
		
		// Shared initialization code
		void Initialize ()
		{
		}

		public override void WindowDidLoad ()
		{
			base.WindowDidLoad ();
			this.presenter = new DiffPresenter (this,fileAPath,fileBPath,resultPath);
			this.SaveButton.Activated += (sender, e) => presenter.Save ();
		}

		#endregion

		//strongly typed window accessor
		public new DiffWindow Window
		{
			get
			{
				return (DiffWindow)base.Window;
			}
		}
			
		public string SourceFile
		{
			set
			{
				this.LocalTextView.Value = value;
			}
		}

		public string ChangedFile
		{
			set
			{
				this.RemoteTextView.Value = value;
			}
		}

		public string ResultFile
		{
			get
			{
				return this.MergedTextView.Value;
			}
			set
			{
				this.MergedTextView.Value = value;
			}
		}
	}
}


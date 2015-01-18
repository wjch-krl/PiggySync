using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using PiggySync.GuiShared;
using Android.Support.V7.App;

namespace PiggySync.AndroidApp
{
	[Activity (Label = "PiggySync.AndroidApp", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : ActionBarActivity, IMainView
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.mainlayout);
		}
			
		public double ProgresLevel
		{
			get
			{
				throw new NotImplementedException ();
			}
			set
			{
				throw new NotImplementedException ();
			}
		}

		public PiggySync.Model.SyncStatus SyncStatus
		{
			set
			{
				throw new NotImplementedException ();
			}
		}

		public bool ProgresEnabled
		{
			set
			{
				throw new NotImplementedException ();

				//var progresbar =
			}
		}

	}
}



using System;
using Android.Support.V4.App;
using Android.Views;
using Android.OS;
using PiggySync.GuiShared;
using Android.Widget;
using PiggySync.Common;

namespace PiggySync.AndroidApp
{
	public class MainFragment: Fragment, IMainView
	{
		private MainPresenter presenter;
		private ProgressBar progresBar;
		private TextView statusLabel;
		private ImageView statusImage;

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate (Resource.Layout.mainlayout, null);
			this.progresBar = view.FindViewById<ProgressBar> (Resource.Id.progressBar1);
			this.statusLabel = view.FindViewById<TextView> (Resource.Id.statusTextView);
			this.statusImage = view.FindViewById<ImageView> (Resource.Id.statusImageView);
			statusImage.Click += (sender, e) =>
			{
				System.Diagnostics.Debug.WriteLine ("dddD");
			};
			this.presenter = new MainPresenter (this);
			return view;
		}

		public double ProgresLevel
		{
			get { return progresBar.Progress / 100.0; }
			set { progresBar.Progress = Convert.ToInt32 (value * 100); }
		}

		public SyncStatus SyncStatus
		{ 
			set { statusLabel.Text = String.Format ("Sync status:\n{0}", value); } 
		}

		public bool ProgresEnabled
		{
			set { progresBar.Visibility = !value ? ViewStates.Invisible : ViewStates.Visible; } 
		}
	}
}


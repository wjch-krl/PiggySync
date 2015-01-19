using System;
using Android.Support.V4.App;
using Android.Views;
using Android.OS;

namespace PiggySync.AndroidApp
{
	public class MainFragment: Fragment, IMainView
	{
		private MainPresenter presenter;
		private ProgressBar progresBar;
		
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Resource.Layout.mainlayout, null);
			this.presenter = new MainPresenter(this);
			this.progresBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
			return view;
		}
		
		public double ProgresLevel 
		{
			get { return progresBar.Progress / 100.0; }
			set { progresBar.Progress = Convert.ToIn32(value * 100);   }
		}

        public SyncStatus SyncStatus 
		{ 
			set { throw new NotImplementedException(); } 
		}

		public bool ProgresEnabled 
		{
			set { throw new NotImplementedException(); } 
		}
	}
}


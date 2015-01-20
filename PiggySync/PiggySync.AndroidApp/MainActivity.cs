using Android.App;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Util;
using PiggySync.GuiShared;
using PiggySync.Common;
using System;
using PiggySync.StandardTypeResolver;
using PiggySync.Model;
using SQLite.Net.Platform.XamarinAndroid;
using Android.Content.PM;

namespace PiggySync.AndroidApp
{
	[Android.App.Activity (Label = "PiggySync.AndroidApp", Theme = "@style/Theme.AppCompat", MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Portrait)]
	public class MainActivity : ActionBarActivity, Android.Support.V7.App.ActionBar.ITabListener
	{
		Android.Support.V4.App.Fragment[] _fragments;

		protected override void OnCreate (Bundle bundle)
		{
			TypeResolver.Factory = new Resolver ();
			DatabaseManager.Init (new SQLitePlatformAndroid());
			base.OnCreate (bundle);
			SupportActionBar.NavigationMode = Android.Support.V7.App.ActionBar.NavigationModeTabs;
			SetContentView(Resource.Layout.Main);

			_fragments = new Android.Support.V4.App.Fragment[]
			{
				new MainFragment(),
				new SettingsFragment(),
				new DevicesFragment(),
			};

			AddTabToActionBar (" Main ");
			AddTabToActionBar (" Settings ");
			AddTabToActionBar (" Devices ");
		}

		public void OnTabReselected(Android.Support.V7.App.ActionBar.Tab tab, Android.Support.V4.App.FragmentTransaction ft)
		{
			System.Diagnostics.Debug.WriteLine ( "The tab {0} was re-selected.", (object)tab.Text);
		}

		public void OnTabSelected(Android.Support.V7.App.ActionBar.Tab tab, Android.Support.V4.App.FragmentTransaction ft)
		{
			System.Diagnostics.Debug.WriteLine ( "The tab {0} has been selected.", (object)tab.Text);
			var frag = _fragments[tab.Position];
			ft.Replace(Resource.Id.frameLayout1, frag);
		}

		public void OnTabUnselected(Android.Support.V7.App.ActionBar.Tab tab, Android.Support.V4.App.FragmentTransaction ft)
		{
			// perform any extra work associated with saving fragment state here.
			System.Diagnostics.Debug.WriteLine ("The tab {0} as been unselected.", (object)tab.Text);
		}

		void AddTabToActionBar(string tabName)
		{
			Android.Support.V7.App.ActionBar.Tab tab = SupportActionBar.NewTab()
				.SetText(tabName)
				.SetTabListener(this);
			SupportActionBar.AddTab(tab);
		}
	}
}



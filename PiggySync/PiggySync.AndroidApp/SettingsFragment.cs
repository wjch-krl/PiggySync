using System;
using Android.Support.V4.App;
using Android.Views;
using Android.OS;

namespace PiggySync.AndroidApp
{
	public class SettingsFragment: Fragment
	{
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Resource.Layout.settingslayout, null);
			return view;
		}
	}
}


using System;
using Android.Support.V4.App;
using Android.Views;
using Android.OS;
using PiggySync.GuiShared;
using Android.Widget;
using Android.Provider;
using Android.App;
using System.Linq;
using PiggySync.Domain;

namespace PiggySync.AndroidApp
{
	public class SettingsFragment: Android.Support.V4.App.Fragment, ISettingsView
	{
		private EditText syncPathTextBox;
		private CheckBox enamleTcp;
		private CheckBox encrypt;
		private EditText keepDeleted;
		private EditText bannedFiles;
		private EditText textFiles;
		private SettingsPresenter presenter;
		private EditText devName;

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate (Resource.Layout.settingslayout, null);
			syncPathTextBox = view.FindViewById<EditText> (Resource.Id.syncRootPathEdit);
			enamleTcp = view.FindViewById<CheckBox> (Resource.Id.useTcp);
			encrypt = view.FindViewById<CheckBox> (Resource.Id.useEncyption);
			keepDeleted = view.FindViewById<EditText> (Resource.Id.deletedStore);
			bannedFiles = view.FindViewById<EditText> (Resource.Id.bannedFilesEdit);
			textFiles = view.FindViewById<EditText> (Resource.Id.textFilesEdit);
			devName = view.FindViewById<EditText> (Resource.Id.deviceName);
			var saveButton = view.FindViewById<Button> (Resource.Id.saveButton);
			saveButton.Click+= SaveSettings;
			this.presenter = new SettingsPresenter (this);
			return view;
		}

		void SaveSettings (object sender, EventArgs e)
		{
			if (this.presenter.SaveSettings ())
			{
				new AlertDialog.Builder (Activity).SetTitle ("OK").SetMessage ("Settings saved").Show ();
			}
			else
			{
				new AlertDialog.Builder (Activity).SetTitle ("Error").SetMessage ("An error occured while saving Settings").Show ();
			}
		}

		public string SyncRootPath
		{
			get
			{
				return syncPathTextBox.Text;
			}
			set
			{
				syncPathTextBox.Text = value;
			}
		}

		public bool UseTcp
		{
			get
			{
				return enamleTcp.Checked;
			}
			set
			{
				enamleTcp.Checked = value;
			}
		}

		public bool UseEncryption
		{
			get
			{
				return encrypt.Checked;
			}
			set
			{
				encrypt.Checked = value;
			}
		}

		public string ComputerName
		{
			get
			{
				return devName.Text;
			}
			set
			{
				devName.Text = value;
			}
		}

		public System.Collections.Generic.IEnumerable<PiggySync.Domain.TextFile> TextFiles
		{
			get
			{
				return textFiles.Text.Split (new char[0], StringSplitOptions.RemoveEmptyEntries).
					Select (x => new TextFile (){ Extension = x });
			}
			set
			{
				textFiles.Text = string.Join ("\n", value.Select (x => x.Extension));
			}
		}

		public System.Collections.Generic.IEnumerable<string> BannedFiles
		{
			get
			{
				return bannedFiles.Text.Split (new char[0], StringSplitOptions.RemoveEmptyEntries);
			}
			set
			{
				bannedFiles.Text = string.Join ("\n", value);
			}
		}

		public int KeepDeletedInfo
		{
			get
			{
				return int.Parse (keepDeleted.Text);
			}
			set
			{
				keepDeleted.Text = value.ToString ();
			}
		}
	}
}


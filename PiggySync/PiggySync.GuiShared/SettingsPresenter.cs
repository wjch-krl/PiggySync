using PiggySync.Core;
using PiggySync.DesktopFileWather;
using System.Collections.Generic;
using PiggySync.Domain.Concrete;
using PiggySync.Model;
using System.Linq;
using PiggySync.Domain;

namespace PiggySync.GuiShared
{
	public class SettingsPresenter
	{
		ISettingsView settingsView;
		static SyncManager main;

		public SettingsPresenter (ISettingsView view)
		{
			this.settingsView = view;
			ReloadSettings ();
		}

		public void ReloadSettings ()
		{
			var settings = XmlSettingsRepository.Instance.Settings;
			settingsView.TextFiles = settings.TextFiles;
			settingsView.SyncRootPath = settings.SyncRootPath;
			settingsView.AutoSync = settings.AutoSync;
			settingsView.ComputerName = settings.ComputerName;
			settingsView.UseEncryption = settings.UseEncryption;
			settingsView.UseTcp = settings.UseTcp;
			settingsView.TextFiles = settings.TextFiles;
		}

		public bool SaveSettings ()
		{
			var settings = XmlSettingsRepository.Instance.Settings;
			settings.SyncRootPath = settingsView.SyncRootPath;
			settings.AutoSync = settingsView.AutoSync;
			settings.ComputerName = settingsView.ComputerName;
			settings.UseEncryption = settingsView.UseEncryption;
			settings.UseTcp = settingsView.UseTcp;
			settings.TextFiles = settingsView.TextFiles.ToList ();
			return XmlSettingsRepository.Instance.SaveSettings ();
		}
	}
}


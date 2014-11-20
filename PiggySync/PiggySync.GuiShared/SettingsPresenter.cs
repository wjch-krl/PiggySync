using PiggySync.Core;
using PiggySync.DesktopFileWather;
using System.Collections.Generic;
using PiggySync.Domain.Concrete;
using PiggySync.Model;
using System.Linq;

namespace PiggySync.GuiShared
{
	public class SettingsPresenter
	{
		ISettingsView mainView;
		static SyncManager main;

		static SettingsPresenter()
		{
			main = new SyncManager();
			FileWatcher.Initialize(main);
			main.Run();
		}

		public SettingsPresenter (ISettingsView mainView)
		{
			this.mainView = mainView;
		}

		public void ReloadSettings()
		{
			var settings = XmlSettingsRepository.Instance.Settings;
			mainView.SyncRootPath = settings.SyncRootPath;
			mainView.AutoSync = settings.AutoSync;
			mainView.ComputerName = settings.ComputerName;
			mainView.UseEncryption = settings.UseEncryption;
			mainView.UseTcp = settings.UseTcp;
			mainView.TextFiles = settings.TextFiles;
		}

		public bool SaveSettings ()
		{
			var settings = XmlSettingsRepository.Instance.Settings;
			settings.SyncRootPath = mainView.SyncRootPath;
			settings.AutoSync = mainView.AutoSync;
			settings.ComputerName = mainView.ComputerName;
			settings.UseEncryption = mainView.UseEncryption;
			settings.UseTcp = mainView.UseTcp;
			settings.TextFiles = mainView.TextFiles;
			return XmlSettingsRepository.Instance.SaveSettings ();
		}

		public void Synchronize()
		{
			if (!main.IsSynchronizing)
			{
				main.ForceSync ();
			}
		}
	}
}


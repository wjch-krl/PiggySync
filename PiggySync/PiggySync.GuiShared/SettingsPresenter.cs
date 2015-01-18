using System.Collections.Generic;
using System.Linq;
using PiggySync.Core;
using PiggySync.Domain.Concrete;

namespace PiggySync.GuiShared
{
    public class SettingsPresenter
    {
        private readonly ISettingsView settingsView;

        public SettingsPresenter(ISettingsView view)
        {
            settingsView = view;
            ReloadSettings();
        }

        public void ReloadSettings()
        {
            var settings = XmlSettingsRepository.Instance.Settings;
            settingsView.TextFiles = settings.TextFiles;
            settingsView.SyncRootPath = settings.SyncRootPath;
            settingsView.ComputerName = settings.ComputerName;
            settingsView.BannedFiles = settings.BannedFiles;
            settingsView.UseEncryption = settings.UseEncryption;
            settingsView.UseTcp = settings.UseTcp;
            settingsView.KeepDeletedInfo = settings.DeletedFileStore;
            settingsView.TextFiles = settings.TextFiles;
        }

        public bool SaveSettings()
        {
            var settings = XmlSettingsRepository.Instance.Settings;
            settings.SyncRootPath = settingsView.SyncRootPath;
            settings.ComputerName = settingsView.ComputerName;
            settings.UseEncryption = settingsView.UseEncryption;
            settings.BannedFiles = new HashSet<string>(settingsView.BannedFiles);
            settings.UseTcp = settingsView.UseTcp;
            settings.DeletedFileStore = settingsView.KeepDeletedInfo;
            settings.TextFiles = settingsView.TextFiles.ToList();
            return XmlSettingsRepository.Instance.SaveSettings();
        }
    }
}
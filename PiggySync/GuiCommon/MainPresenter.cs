using System.Collections.Generic;
using System.Linq;
using PiggySync.Core;
using PiggySync.DesktopFileWather;
using PiggySync.Domain.Concrete;
using PiggySync.Model;

namespace PiggySync.GuiCommon
{
    public class MainPresenter : IHostWather
    {
        private static readonly SyncManager main;
        private readonly IMainView mainView;

        static MainPresenter()
        {
            main = new SyncManager();
            FileWatcher.Initialize(main);
            main.Run();
        }

        public MainPresenter(IMainView mainView)
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

        public void RefreshHostsList(IEnumerable<PiggyRemoteHost> hosts)
        {
            mainView.Hosts = hosts.Select(x => x.GetShortName());
        }

        public void SaveSettings()
        {
            mainView.ActionStart();
            bool succesfull = true;
            var settings = XmlSettingsRepository.Instance.Settings;
            settings.SyncRootPath = mainView.SyncRootPath;
            settings.AutoSync = mainView.AutoSync;
            settings.ComputerName = mainView.ComputerName;
            settings.UseEncryption = mainView.UseEncryption;
            settings.UseTcp = mainView.UseTcp;
            settings.TextFiles = mainView.TextFiles;
            if (XmlSettingsRepository.Instance.SaveSettings())
            {
                mainView.ActionFinished(ResourceWorker.SaveSuccesfull);
            }
            else
            {
                mainView.ActionFinished(ResourceWorker.SaveFail);
            }
        }

        public void Synchronize()
        {
            if (!main.IsSynchronizing)
            {
                main.ForceSync();
            }
        }
    }
}
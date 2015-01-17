using PiggySync.Core;
//using PiggySync.DesktopFileWather;

namespace PiggySync.GuiShared
{
    public class MainPresenter : IStatusWather
    {
        private static readonly SyncManager main;
        private readonly IMainView mainView;

        static MainPresenter()
        {
            main = new SyncManager();
           // FileWatcher.Initialize(main);
            main.Run();
			main.Observers.Add (this);
        }

        public MainPresenter(IMainView mainView)
        {
            this.mainView = mainView;
			this.mainView.SyncStatus = SyncStatus.UpToDate;
        }
			
    }
}
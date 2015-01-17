using PiggySync.Core;
//using PiggySync.DesktopFileWather;
using PiggySync.Model;

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
        }

        public MainPresenter(IMainView mainView)
        {
            this.mainView = mainView;
			this.mainView.SyncStatus = SyncStatus.UpToDate;
			main.Observers.Add (this);

        }
			
		public SyncStatus Status
		{
			set
			{
				mainView.SyncStatus = value;
			}
		}
		public double Progress
		{
			set
			{
				mainView.ProgresLevel = value;
			}
		}
    }
}
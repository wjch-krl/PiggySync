using System;
using PiggySync.Core;
using PiggySync.Domain;
using PiggySync.Domain.Concrete;
using PiggySync.DesktopFileWather;

namespace PiggySync.GuiShared
{
	public class MainPresenter
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

		public void Synchronize()
		{
			if (!main.IsSynchronizing)
			{
				main.ForceSync();
			}
		}
	}
}


using PiggySyncWin.Domain.Concrete;
using PiggySyncWin.WinUI.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PiggySyncWin.Domain.Concrete;

namespace PiggySyncWin.WinUI.Infrastructure
{
	public class FileWatcher : IFileWather
	{
		private static PiggyConnect dckConnect;
		private static XmlSettingsRepository repo;
		private static FileSystemWatcher watcher;

		static void watcher_Created(object sender, FileSystemEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine(e.Name + " has created. Sending notyfy...");//TODO save modyfied file packet
			string newElementPath = e.FullPath.Replace(XmlSettingsRepository.Instance.Settings.CurrentDirectory+"\\", "");
			System.Diagnostics.Debug.WriteLine(newElementPath + " Adding");
			//  if (e.ChangeType == WatcherChangeTypes.Created)
			try
			{
				//     dckConnect.NotyfyAllHost();
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex);
			}
		}

		static void addFileToRootFolder()
		{

		}

		static void fileChanged (string path)
		{
			System.Diagnostics.Debug.WriteLine (path + " has changed. Sending notyfy...");
			try {
				//     dckConnect.NotyfyAllHost();
			}
			catch (Exception ex) {
				System.Diagnostics.Debug.WriteLine (ex);
			}
		}

		static void watcher_Changed(object sender, FileSystemEventArgs e)
		{
			fileChanged (e.FullPath);
		}

		static void watcher_Deleted (object sender, FileSystemEventArgs e)
		{
			fileDeleted (e.FullPath); 
		}

		public static void Initialize(PiggyConnect main)
		{
			dckConnect = main;
			repo = XmlSettingsRepository.Instance;

			if (watcher == null)
			{
				watcher = new FileSystemWatcher(repo.Settings.CurrentDirectory);
				watcher.IncludeSubdirectories = true;

				watcher.Changed += watcher_Changed;
				watcher.Created += watcher_Created;
				watcher.Deleted += watcher_Deleted;

				watcher.EnableRaisingEvents = true;
			}
		}

		public static void RefreshMonitoredDirectory(string dir)
		{
			watcher.EnableRaisingEvents = false;
			watcher.Path = dir;
			watcher.EnableRaisingEvents = true;
		}

		static void fileDeleted (string fullPath)
		{
			throw new NotImplementedException ();
		}
	}
}

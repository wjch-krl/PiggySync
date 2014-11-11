﻿using PiggySync.Core;
using PiggySync.Domain.Abstract;
using PiggySync.Domain.Concrete;
using PiggySyncWin.WinUI.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PiggySync.Model;
using PiggySyncWin.Core;

namespace PiggySyncWin.WinUI.Infrastructure
{
	public class FileWatcher : IFileWather
	{
		private static SyncManager syncM;
		private static XmlSettingsRepository repo;
		private static FileSystemWatcher watcher;

		static void watcher_Created (object sender, FileSystemEventArgs e)
		{
			fileCreated (e.FullPath);
		}

		static void fileCreated (string path)
		{
			System.Diagnostics.Debug.WriteLine (path + " has created. Sending notyfy...");//TODO save modyfied file packet
			FileManager.RefreshPath (path);
			string newElementPath = path.Replace (XmlSettingsRepository.Instance.Settings.SyncRootPath + "\\", String.Empty);
			System.Diagnostics.Debug.WriteLine (newElementPath + " Adding");
			try
			{
				syncM.NotyfyAllHost ();
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine (ex);
			}
		}

		static void fileChanged (string path)
		{
			System.Diagnostics.Debug.WriteLine (path + " has changed. Sending notyfy...");
			FileManager.RefreshPath (path);
			try
			{
				syncM.NotyfyAllHost ();
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine (ex);
			}
		}

		static void watcher_Changed (object sender, FileSystemEventArgs e)
		{
			fileChanged (e.FullPath);
		}

		static void watcher_Deleted (object sender, FileSystemEventArgs e)
		{
			FileDeleted (e.FullPath); 
		}

		public static void Initialize (SyncManager main)
		{
			syncM = main;
			repo = XmlSettingsRepository.Instance;

			if (watcher == null)
			{
				watcher = new FileSystemWatcher (repo.Settings.SyncRootPath);
				watcher.IncludeSubdirectories = true;

				watcher.Changed += watcher_Changed;
				watcher.Created += watcher_Created;
				watcher.Deleted += watcher_Deleted;
				watcher.Renamed += wather_Renamed;
				watcher.EnableRaisingEvents = true;
			}
		}

		public static void RefreshMonitoredDirectory (string dir)
		{
			watcher.EnableRaisingEvents = false;
			watcher.Path = dir;
			watcher.EnableRaisingEvents = true;
		}

		static void FileDeleted (string fullPath)
		{
			var fileInf = new FileInf (fullPath) 
			{
				IsDeleted = true,
			};
			FileManager.RefreshPath (fullPath);
			DatabaseManager.Instance.SaveDeletedFile (fileInf);
			try
			{
				syncM.NotyfyAllHost ();
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine (ex);
			}
		}

		static void wather_Renamed (object sender, RenamedEventArgs e)
		{
			FileRenamed (e.OldFullPath, e.FullPath);
		}

		static void FileRenamed (string oldFullPath, string fullPath)
		{
			throw new NotImplementedException ();
		}
	}
}

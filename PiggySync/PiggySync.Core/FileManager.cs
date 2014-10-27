using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Threading;
using PiggySyncWin.Domain.Concrete;
using PiggySyncWin.Domain;
using System.IO;
using PiggySyncWin.WinUI.Infrastructure;
using PiggySync.Core;
using PiggySync.DatabaseManager;
using PiggySync.Model;
using PiggySyncWin.WinUI.Models.Concrete;

namespace PiggySyncWin.WinUI.Models
{
	public class FileManager
	{
		static SyncInfoPacket rootFolder = null;
		static FileManager fileManager;

		private FileManager ()
		{
			FileManager.rootFolder = new SyncInfoPacket ();
		}

		public static void Initialize ()
		{

			if (FileManager.fileManager == null)
			{
				FileManager.fileManager = new FileManager ();
				CreateRootFolder ();
			}
		}

		public static SyncInfoPacket RootFolder
		{
			get { return rootFolder; }
			set { rootFolder = value; }
		}

		public static SyncInfoPacket RootFolderCopy
		{
			get
			{
				lock (rootFolder)
				{
					return rootFolder.Copy ();
				}
			}
		}

		public static void CreateRootFolder ()
		{
			FileManager.rootFolder = new SyncInfoPacket ();
			CreateRootFolder (FileManager.rootFolder);
			DatabaseManager.Instance.SaveFiles (rootFolder);
		}

		private static void CreateRootFolder (SyncInfoPacket root) //TODO delete packets 
		{
			string path = XmlSettingsRepository.Instance.Settings.SyncPath;
			var dbFiles = DatabaseManager.Instance.GetAllFiles ();
			CreateRootFolder (root, path, dbFiles);
		}

		private static void CreateRootFolder (SyncInfoPacket root, string path, HashSet<FileInf> dbFiles) //TODO delete packets 
		{
			if (root is FolderInfoPacket)
			{
				path += @"\" + (root as FolderInfoPacket).FolderName;
			}
			GetFiles (root, path, dbFiles);
			GetDirectories (root, path, dbFiles);
			FileManager.rootFolder.ElelmentsCount = FileManager.rootFolder.GetFileCount ();
		}


		static void GetDirectories (SyncInfoPacket root, string path, HashSet<FileInf> deletedFiles)
		{
			string[] folders = System.IO.Directory.GetDirectories (path);
			FolderInfoPacket folder;
			foreach (var x in folders)
			{
				System.Diagnostics.Debug.WriteLine ("Added dir: " + x);
				folder = new FolderInfoPacket (x.Substring (path.Length + 1));
				//simple path 
				CreateRootFolder (folder, path, deletedFiles);
				root.Folders.Add (folder);
			}
		}

		static void GetFiles (SyncInfoPacket root, string path, HashSet<FileInf> dbFiles)
		{
			string[] localFileNames = System.IO.Directory.GetFiles (path);
			var addedFiles = new List<FileInf> ();
			foreach (var x in localFileNames)
			{
				System.Diagnostics.Debug.WriteLine ("Added file: " + x.Substring (path.Length + 1));
				var fileInf = new FileInf (x) {
					Path = path,
				};
				root.Files.Add (new FileInfoPacket (fileInf));
				addedFiles.Add (fileInf);
			}
			foreach (var element in dbFiles.Where (x => x.Path == path && x.IsDeleted))
			{
				System.Diagnostics.Debug.WriteLine ("Added deleted file from db: {0}", element.FileName);
				root.DeletedFiles.Add (new FileDeletePacket (element));
			}
			foreach (var element in dbFiles.Where (x => x.Path == path && !x.IsDeleted))
			{
				FileInf file = null;
				if ((file = addedFiles.FirstOrDefault (x => x.FileName == element.FileName)) == null)
				{
					System.Diagnostics.Debug.WriteLine ("Added new deleted file: {0}", element.FileName);
					element.IsDeleted = true;
					DatabaseManager.Instance.SaveDeletedFile (element);
					root.DeletedFiles.Add (new FileDeletePacket (element));
				}
				else
				{
					file.Id = element.Id;
				}
			}
		}

		public static void RefreshRootFolder ()
		{
			CreateRootFolder ();
		}

		public static void RefreshPath (string path)
		{
			if (!Directory.Exists (path))
			{
				path = Path.GetDirectoryName (path);
			}
			path = path.Replace (XmlSettingsRepository.Instance.Settings.SyncPath, String.Empty);
			path.Replace ('/','\\');
			SyncInfoPacket folder = rootFolder;
			foreach (var segment in path.Split ('\\'))
			{
				folder = folder.Folders.First (element => element.FolderName == segment);
			}
			var dbFiles = DatabaseManager.Instance.GetAllFiles ();
			GetFiles (folder, path, dbFiles);
			DatabaseManager.Instance.SaveFiles (folder);
		}
	}
}

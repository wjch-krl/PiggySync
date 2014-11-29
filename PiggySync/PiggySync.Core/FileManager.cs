using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PiggySync.Model;
using PiggySync.Model.Concrete;
using PiggySync.Domain;
using PiggySync.Domain.Concrete;

namespace PiggySync.Core
{
	public class FileManager
	{
		static SyncInfoPacket rootFolder = null;
		static FileManager fileManager;

		private FileManager ()
		{
			FileManager.rootFolder = new SyncInfoPacket ();
		}

		static FileManager ()
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
			string path = XmlSettingsRepository.Instance.Settings.SyncRootPath;
			var dbFiles = DatabaseManager.Instance.GetAllFiles ();
			CreateRootFolder (root, path, dbFiles);
			AddOtherDeletedFiles (dbFiles, root);
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
				try
				{
					folder = new FolderInfoPacket (x.Substring (path.Length + 1));
					//simple path 
					CreateRootFolder (folder, path, deletedFiles);
					root.Folders.Add (folder);
					System.Diagnostics.Debug.WriteLine ("Added dir: {0}", x);
				}
				catch (IOException ex)
				{
					System.Diagnostics.Debug.WriteLine ("Cannot add dir: {0}", ex);
				}

			}
		}

		static void GetFiles (SyncInfoPacket root, string path, HashSet<FileInf> dbFiles)
		{
			string[] localFileNames = System.IO.Directory.GetFiles (path);
			var addedFiles = new List<FileInf> ();
			foreach (var x in localFileNames)
			{
				try
				{
					var fileInf = new FileInf (x) {
						Path = path,
					};
					if (XmlSettingsRepository.Instance.Settings.BannedFiles.Contains (fileInf.FileName))
					{
						continue;
					}
					root.Files.Add (new FileInfoPacket (fileInf));
					addedFiles.Add (fileInf);
					System.Diagnostics.Debug.WriteLine ("Added file: {0}", x.Substring (path.Length + 1));

				}
				catch (IOException e)
				{
					System.Diagnostics.Debug.WriteLine ("Cannot add file: {0}", e);

				}
			}
			var folderFiles = new List<FileInf> (dbFiles.Where (x => x.Path == path));
			foreach (var element in folderFiles)
			{
				FileInf file = null;
				if ((file = addedFiles.FirstOrDefault (x => x.FileName == element.FileName && !element.IsDeleted)) == null)
				{
					System.Diagnostics.Debug.WriteLine ("Added new deleted file: " + element.FileName);
					element.IsDeleted = true;
					DatabaseManager.Instance.SaveDeletedFile (element);
					root.DeletedFiles.Add (new FileDeletePacket (element));
				}
				else
				{
					file.IsDeleted = false;
					file.Id = element.Id;
				}
				dbFiles.Remove (element);
			}
		}

		public static void RefreshRootFolder ()
		{
			CreateRootFolder ();
		}

		static void AddOtherDeletedFiles (HashSet<FileInf> dbFiles, SyncInfoPacket root)
		{
			foreach (var file in dbFiles)
			{
				file.IsDeleted = true;
				var folder = file.Path.Replace (XmlSettingsRepository.Instance.Settings.SyncRootPath, String.Empty);
				folder = folder.Replace ('\\', '/');
				var foldersTree = folder.Split ('/');
				string curr = XmlSettingsRepository.Instance.Settings.SyncRootPath;
				foreach (var element in foldersTree)
				{
					curr = Path.Combine (element, curr);
					if (!Directory.Exists (curr))
					{
						var deletedFolder = new FolderInfoPacket (curr.Replace (XmlSettingsRepository.Instance.Settings.SyncRootPath, String.Empty), 144);
						root.Folders.Add (deletedFolder);//TODO
						deletedFolder.DeletedFiles.Add (new FileDeletePacket (file));
						break;
					}
				}
				rootFolder.DeletedFiles.Add (new FileDeletePacket (file));
				//TODO Create deleted folder packet
			}
		}

		public static void RefreshPath (string path)
		{
			if (!Directory.Exists (path))
			{
				path = Path.GetDirectoryName (path);
			}
			path = path.Replace (XmlSettingsRepository.Instance.Settings.SyncRootPath, String.Empty);
			path = path.Replace ('\\', '/');
			SyncInfoPacket folder = rootFolder;
			if (!String.IsNullOrWhiteSpace (path))
			{
				foreach (var segment in path.Split ('/'))
				{
					folder = folder.Folders.First (element => element.FolderName == segment);
				}
			}
			var dbFiles = DatabaseManager.Instance.GetAllFiles ();
			GetFiles (folder, path, dbFiles);
			DatabaseManager.Instance.SaveFiles (folder);
		}
	}
}

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
using DuckSync.Core;

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

			if (FileManager.fileManager == null) {
				FileManager.fileManager = new FileManager ();
				CreateRootFolder ();
			}
		}

		public static SyncInfoPacket RootFolder {
			get { return rootFolder; }
			set { rootFolder = value; }
		}

		public static SyncInfoPacket RootFolderCopy
		{
			get{
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
		}

		private static void CreateRootFolder (SyncInfoPacket root) //TODO delete packets 
		{
			string path = XmlSettingsRepository.Instance.Settings.SyncPath;
			CreateRootFolder (root, path);
		}

		private static void CreateRootFolder (SyncInfoPacket root, string path) //TODO delete packets 
		{//TODO Serialize/Deserialize dleted files etc...
            
			if (root is FolderInfoPacket) {
				path += @"\" + (root as FolderInfoPacket).FolderName;
			}
			GetFiles (root, path);
			GetDirectories (root, path);
			FileManager.rootFolder.ElelmentsCount = FileManager.rootFolder.GetFileCount ();
		}

		static void GetFiles (SyncInfoPacket root, string path)
		{
			string[] files = System.IO.Directory.GetFiles (path);
			foreach (var x in files)
			{
				FileInfo fileInf = new FileInfo (x);
				System.Diagnostics.Debug.WriteLine ("Added file: " + x.Substring (path.Length + 1));
				root.Files.Add (new FileInfoPacket (new FileInf () {
					FileName = fileInf.Name,
					FileSize = (UInt32)fileInf.Length,
					//TODO check overfllow
					LastModyfied = (UInt64)(fileInf.LastWriteTimeUtc - new DateTime (1970, 1, 1)).Ticks,
					CheckSum = CheckSumGenerator.ComputeChecksum (fileInf),
				}));
			}
		}

		static void GetDirectories (SyncInfoPacket root, string path)
		{
			string[] folders = System.IO.Directory.GetDirectories (path);
			FolderInfoPacket folder;
			foreach (var x in folders)
			{
				System.Diagnostics.Debug.WriteLine ("Added dir: " + x);
				folder = new FolderInfoPacket (x.Substring (path.Length + 1));
				//simple path 
				CreateRootFolder (folder, path);
				root.Folders.Add (folder);
			}
		}

		public static void RefreshRootFolder ()
		{

		}

		public static void RefreshFolder(FolderInfoPacket folder)
		{

		}

		public static void RefreshPath(string path)
		{

		}
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PiggySync.Domain.Concrete;
using PiggySync.Model.Concrete;
using SQLite.Net;
using SQLite.Net.Interop;
using PiggySync.Common;

namespace PiggySync.Model
{
	public class DatabaseManager
	{
		private static DatabaseManager instance;
		private readonly string databasePath;
		private readonly object dbLock;
		private ISQLitePlatform sqlitePlatform;

		private DatabaseManager (ISQLitePlatform sqlitePlatform)
		{
			string DataBaseName = "PiggyDB.sqlite";
			string libraryPath = XmlSettingsRepository.SettingsPath;
			if (!TypeResolver.DirectoryHelper.Exists (libraryPath))
			{
				TypeResolver.DirectoryHelper.CreateHiddenDirectory (libraryPath);
			}
			databasePath = Path.Combine (libraryPath, DataBaseName);
			dbLock = new object ();
			this.sqlitePlatform = sqlitePlatform;
			using (var con = GetConnection ())
			{
				con.CreateTable<PiggyRemoteHostHistoryEntry> ();
				con.CreateTable<FileInf> ();
			}
		}

		public static void Init (ISQLitePlatform sqlitePlatform)
		{
			instance = new DatabaseManager (sqlitePlatform);
		}

		public static DatabaseManager Instance
		{
			get { return instance; }
		}

		public List<PiggyRemoteHostHistoryEntry> GetHistoricalDeviaces ()
		{
			lock (dbLock)
			{
				using (var con = GetConnection ())
				{
					return con.Table<PiggyRemoteHostHistoryEntry> ().ToList ();
				}
			}
		}

		public HashSet<FileInf> GetDeletedFiles (string path) //TODO maybe IList
		{
			lock (dbLock)
			{
				using (var con = GetConnection ())
				{
					var retVal = new HashSet<FileInf> (new FileInfComparer ());
					foreach (var element in con.Table<FileInf>().Where(x => x.Path == path && x.IsDeleted))
					{
						retVal.Add (element);
					}
					return retVal;
				}
			}
		}

		public HashSet<FileInf> GetDeletedFiles ()
		{
			lock (dbLock)
			{
				using (var con = GetConnection ())
				{
					var retVal = new HashSet<FileInf> (new FileInfComparer ());
					foreach (var element in  con.Table<FileInf>().Where(x => x.IsDeleted))
					{
						retVal.Add (element);
					}
					return retVal;
				}
			}
		}

		public HashSet<FileInf> GetFiles (string path) //TODO maybe IList
		{
			lock (dbLock)
			{
				using (var con = GetConnection ())
				{
					var retVal = new HashSet<FileInf> (new FileInfComparer ());
					foreach (var element in  con.Table<FileInf>().Where(x => x.Path == path && !x.IsDeleted))
					{
						retVal.Add (element);
					}
					return retVal;
				}
			}
		}

		public HashSet<FileInf> GetFiles ()
		{
			lock (dbLock)
			{
				using (var con = GetConnection ())
				{
					var retVal = new HashSet<FileInf> (new FileInfComparer ());
					foreach (var element in  con.Table<FileInf>().Where(x => !x.IsDeleted))
					{
						retVal.Add (element);
					}
					return retVal;
				}
			}
		}

		public HashSet<FileInf> GetAllFiles ()
		{
			lock (dbLock)
			{
				using (var con = GetConnection ())
				{
					var retVal = new HashSet<FileInf> (new FileInfComparer ());
					foreach (var element in  con.Table<FileInf>())
					{
						retVal.Add (element);
					}
					return retVal;
				}
			}
		}

		public void SaveDeletedFiles (IEnumerable<FileInf> filesToSave)
		{
			lock (dbLock)
			{
				using (var con = GetConnection ())
				{
					con.InsertAll (filesToSave.Where (x => x.Id == 0));
				}
			}
		}

		public void SaveFiles (IEnumerable<FileInf> filesToSave)
		{
			lock (dbLock)
			{
				using (var con = GetConnection ())
				{
					con.InsertAll (filesToSave.Where (x => x.Id == 0));
					con.UpdateAll (filesToSave.Where (x => x.Id != 0));
				}
			}
		}

		public void SaveFiles (SyncInfoPacket rootFolder)
		{
			lock (dbLock)
			{
				using (var con = GetConnection ())
				{
					con.InsertAll (rootFolder.Files.Select (x => x.File).Where (x => x.Id == 0));
					con.UpdateAll (rootFolder.Files.Select (x => x.File).Where (x => x.Id != 0));

					con.InsertAll (rootFolder.DeletedFiles.Select (x => x.File).Where (x => x.Id == 0));
					con.UpdateAll (rootFolder.DeletedFiles.Select (x => x.File).Where (x => x.Id != 0));
				}
				foreach (var x in rootFolder.Folders)
				{
					SaveFiles (x);
				}
			}
		}

		public void SaveHistoricalDeviaces (IEnumerable<PiggyRemoteHostHistoryEntry> hostsToSave)
		{
			lock (dbLock)
			{
				using (var con = GetConnection ())
				{
					con.InsertAll (hostsToSave.Where (x => x.Id == 0));
					con.UpdateAll (hostsToSave.Where (x => x.Id != 0));
				}
			}
		}

		public void SaveDeletedFile (FileInf fileToSave)
		{
			lock (dbLock)
			{
				using (var con = GetConnection ())
				{
					con.InsertOrReplace (fileToSave);
				}
			}
		}

		public void SaveHistoricalDeviace (PiggyRemoteHostHistoryEntry hostToSave)
		{
			lock (dbLock)
			{
				using (var con = GetConnection ())
				{
					con.Insert (hostToSave);
				}
			}
		}

		public void UpdateHistoricalDeviace (PiggyRemoteHostHistoryEntry hostToSave)
		{
			lock (dbLock)
			{
				using (var con = GetConnection ())
				{
					con.Update (hostToSave);
				}
			}
		}

		private SQLiteConnection GetConnection ()
		{
			return new SQLiteConnection (sqlitePlatform, databasePath);
		}
	}
}
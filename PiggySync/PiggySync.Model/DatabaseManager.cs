using System;
using SQLite;
using System.IO;
using System.Collections;
using PiggySyncWin.WinUI.Infrastructure;
using System.Collections.Generic;
using PiggySyncWin.WinUI.Models;
using System.Linq;
using PiggySync.Model;

namespace PiggySync.DatabaseManager
{
	public class DatabaseManager
	{
		string databasePath;
		static DatabaseManager instance;
		object dbLock;

		public static DatabaseManager Instance
		{
			get
			{ 
				return instance ?? (instance = new DatabaseManager ());
			}
		}


		private DatabaseManager ()
		{
			string DataBaseName = "PiggyDB.sqlite";
			string libraryPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			databasePath = Path.Combine (libraryPath, DataBaseName);
			dbLock = new object ();
			using (var con = GetConnection ())
			{
				con.CreateTable<PiggyRemoteHostHistoryEntry> ();
				con.CreateTable<FileInf> ();
			}
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

		public HashSet<FileInf> GetDeletedFiles (string path)//TODO maybe IList
		{
			lock (dbLock)
			{
				using (var con = GetConnection ())
				{
					HashSet<FileInf> retVal = new HashSet<FileInf> (new FileInfComparer ());
					foreach (var element in  con.Table<FileInf> ().Where ( x => x.Path == path && x.IsDeleted))
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
					HashSet<FileInf> retVal = new HashSet<FileInf> (new FileInfComparer ());
					foreach (var element in  con.Table<FileInf> ().Where ( x => x.IsDeleted))
					{
						retVal.Add (element);
					}
					return retVal;
				}
			}
		}

		public HashSet<FileInf> GetFiles (string path)//TODO maybe IList
		{
			lock (dbLock)
			{
				using (var con = GetConnection ())
				{
					HashSet<FileInf> retVal = new HashSet<FileInf> (new FileInfComparer ());
					foreach (var element in  con.Table<FileInf> ().Where ( x => x.Path == path && !x.IsDeleted))
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
					HashSet<FileInf> retVal = new HashSet<FileInf> (new FileInfComparer ());
					foreach (var element in  con.Table<FileInf> ().Where ( x => !x.IsDeleted))
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
					HashSet<FileInf> retVal = new HashSet<FileInf> (new FileInfComparer ());
					foreach (var element in  con.Table<FileInf> ())
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

		SQLiteConnection GetConnection ()
		{
			return new SQLiteConnection (databasePath);
		}
	}
}


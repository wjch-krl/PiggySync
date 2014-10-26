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
			if (!File.Exists (databasePath))
			{
				using (var con = GetConnection ())
				{
					con.CreateTable<PiggyRemoteHostHistoryEntry> ();
					con.CreateTable<FileInf> ();
				}
			}
		}

		public IList<PiggyRemoteHostHistoryEntry> GetHistoricalDeviaces ()
		{
			lock (dbLock)
			{
				using (var con = GetConnection ())
				{
					return con.Table<PiggyRemoteHostHistoryEntry> ().ToList ();
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
					con.InsertAll (filesToSave.Where (x => x.Id != 0));
				}
			}
		}

		public void SaveDeletedFiles (IEnumerable<PiggyRemoteHostHistoryEntry> hostsToSave)
		{
			lock (dbLock)
			{
				using (var con = GetConnection ())
				{
					con.InsertAll (hostsToSave.Where (x => x.Id != 0));
				}
			}
		}

		SQLiteConnection GetConnection ()
		{
			return new SQLiteConnection (databasePath, true);
		}
	}
}


using System;
using SQLite;
using System.IO;

namespace PiggySync.DatabaseManager
{
	public class DatabaseManager
	{
		public DatabaseManager ()
		{
			SQLiteConnection conn = new SQLiteConnection ("");
			conn.CreateTable<> ();
		}
	}
}


using System;
using System.Threading;

namespace PiggySync.Model.DatabaseConnection
{
    internal class SQLiteConnectionWithLock : SQLiteConnection
    {
        private readonly object _lockPoint = new object();

        public SQLiteConnectionWithLock(SQLiteConnectionString connectionString, SQLiteOpenFlags openFlags)
            : base(connectionString.DatabasePath, openFlags, connectionString.StoreDateTimeAsTicks)
        {
        }

        public IDisposable Lock()
        {
            return new LockWrapper(_lockPoint);
        }

        private class LockWrapper : IDisposable
        {
            private readonly object _lockPoint;

            public LockWrapper(object lockPoint)
            {
                _lockPoint = lockPoint;
                Monitor.Enter(_lockPoint);
            }

            public void Dispose()
            {
                Monitor.Exit(_lockPoint);
            }
        }
    }
}
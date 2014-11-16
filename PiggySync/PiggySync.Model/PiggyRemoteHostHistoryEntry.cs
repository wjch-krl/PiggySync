using System;
using PiggySync.Model.DatabaseConnection;

namespace PiggySync.Model
{
    public class PiggyRemoteHostHistoryEntry : PiggyRemoteHost
    {
        public PiggyRemoteHostHistoryEntry(PiggyRemoteHost x) : base(x.Ip, x.Name)
        {
        }

        public PiggyRemoteHostHistoryEntry() : base(null, null)
        {
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime LastSync { get; set; }
    }
}
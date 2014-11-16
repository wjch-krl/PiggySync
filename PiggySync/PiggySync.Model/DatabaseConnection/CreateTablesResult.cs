using System;
using System.Collections.Generic;

namespace PiggySync.Model.DatabaseConnection
{
    public class CreateTablesResult
    {
        internal CreateTablesResult()
        {
            Results = new Dictionary<Type, int>();
        }

        public Dictionary<Type, int> Results { get; private set; }
    }
}
using System;

namespace PiggySync.Model
{
    public class ConnectionTimeOutException : Exception
    {
        public ConnectionTimeOutException(string x)
            : base(x)
        {
        }
    }
}
using System;

namespace PiggySync.Model
{
    public class InvalidHostException : Exception
    {
        public InvalidHostException(string msg) : base(msg)
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PiggySyncWin.WinUI.Infrastructure.Concrete
{
    class ConnectionTimeOutException : Exception
    {
        public ConnectionTimeOutException(string x)
            : base(x)
        {

        }
    }
}

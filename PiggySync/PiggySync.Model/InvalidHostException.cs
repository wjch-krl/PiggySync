using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PiggySyncWin.WinUI.Infrastructure.Concrete
{
    public class InvalidHostException : Exception
    {
        public InvalidHostException(string msg): base(msg)
        {
        }

    }
}

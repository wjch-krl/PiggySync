using PiggySyncWin.WinUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggySyncWin.WinUI.Infrastructure.Abstract
{
    interface IPiggyObserver
    {
        void CreateNewConnection(PiggyRemoteHost remote);
    }
}

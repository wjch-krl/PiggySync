using PiggySyncWin.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggySyncWin.WinUI.Views.Abstract
{
    public interface ISettingsView
    {
        string P1 { set; }
        string P2 { set; }

        event EventHandler LoadSettings;
    }
}

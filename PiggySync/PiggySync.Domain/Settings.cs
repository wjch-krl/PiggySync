using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggySyncWin.Domain
{
    [Serializable]
    public class Settings
    {
        //TODO
        public string CurrentDirectory { get; set; }
        public string ComputerName { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggySyncWin.Domain.Abstract
{
    interface ISettingsRepository
    {
        Settings Settings { get; set; }
        bool SaveSettings();
        bool ReLoadSettings();
        void ClearSettings();
    }
}

using PiggySyncWin.Domain;

namespace PiggySync.Domain.Abstract
{
    interface ISettingsRepository
    {
        Settings Settings { get; set; }
        bool SaveSettings();
        bool ReLoadSettings();
        void ClearSettings();
    }
}

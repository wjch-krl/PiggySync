namespace PiggySync.Domain.Abstract
{
    internal interface ISettingsRepository
    {
        Settings Settings { get; set; }
        bool SaveSettings();
        bool ReLoadSettings();
        void ClearSettings();
    }
}
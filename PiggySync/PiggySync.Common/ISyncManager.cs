using System.Collections.Generic;

namespace PiggySync.Common
{
    public interface ISyncManager
    {
        ICollection<IStatusWather> Observers { get; set; }
        bool IsSynchronizing { get; }
        void ForceSync();
        void NotyfyAllHost();
        void Run();
    }
}
using System.IO;
using System.Threading.Tasks;

namespace PiggySync.Common.Abstract
{
    public interface ICheckSumGenerator
    {
        int ChecksumSize { get; }
        byte[] ComputeChecksum(string filePath);
        byte[] ComputeChecksum(byte[] bytes);
    }
}
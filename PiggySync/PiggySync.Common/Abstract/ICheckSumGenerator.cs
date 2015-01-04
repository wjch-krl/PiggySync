using System.IO;
using System.Threading.Tasks;

namespace PiggySync.Common.Abstract
{
    public interface ICheckSumGenerator
    {
        int ChecksumSize { get; }
        Task<byte[]> ComputeChecksum(string filePath);
        Task<byte[]> ComputeChecksum(byte[] bytes);
    }
}
using System.IO;

namespace PiggySync.Common.Abstract
{
    public interface ICheckSumGenerator
    {
        int ChecksumSize { get; }
        byte[] ComputeChecksum(FileInfo file);

        byte[] ComputeChecksum(byte[] bytes);
    }
}
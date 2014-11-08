using System.IO;

namespace PiggySync.Common.Abstract
{
	public interface ICheckSumGenerator
	{
		byte[] ComputeChecksum (FileInfo file);

		byte[] ComputeChecksum (byte[] bytes);

		int ChecksumSize { get; }
	}
}


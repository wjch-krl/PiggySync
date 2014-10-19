using System;
using System.IO;

namespace DuckSync.Core
{
	public interface ICheckSumGenerator
	{
		byte[] ComputeChecksum (FileInfo file);

		byte[] ComputeChecksum (byte[] bytes);

		int ChecksumSize { get; }
	}
}


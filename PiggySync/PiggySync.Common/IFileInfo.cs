using System;

namespace PiggySync.Common
{
	public interface IFileInfo
	{
		string Name { get; }
		long Length { get; }
		DateTime LastWriteTimeUtc { get; }
	}
}


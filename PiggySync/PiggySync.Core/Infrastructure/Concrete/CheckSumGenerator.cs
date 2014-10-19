using System;
using System.IO;

namespace DuckSync.Core
{
	public static class CheckSumGenerator
	{
		static ICheckSumGenerator generator;

		static CheckSumGenerator ()
		{
			generator = new MD5Generator ();
		}

		public static void ChangeGenerator (ICheckSumGenerator generator)
		{
			CheckSumGenerator.generator = generator;
		}

		public static byte[] ComputeChecksum (FileInfo file)
		{
			return generator.ComputeChecksum (file);
		}

		public static int ChecksumSize { get { return generator.ChecksumSize; }}

	}
}


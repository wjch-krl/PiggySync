using System;
using System.Security.Cryptography;
using System.IO;

namespace DuckSync.Core
{
	class MD5Generator : ICheckSumGenerator
	{

		public byte[] ComputeChecksum (System.IO.FileInfo file)
		{
			using (var md5 = MD5.Create())
			{
				using (var stream = File.OpenRead(file.FullName))
				{
					return md5.ComputeHash(stream);
				}
			}
		}
		public byte[] ComputeChecksum (byte[] bytes)
		{
			using (var md5 = MD5.Create())
			{
				return md5.ComputeHash(bytes);
			}
		}

		public int ChecksumSize {
			get
			{
				return MD5.Create ().HashSize / 8; //TODO optimize
			}
		}
	}

}


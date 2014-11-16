using System.IO;
using System.Security.Cryptography;
using PiggySync.Common.Abstract;

namespace PiggySync.Common.Concrete
{
	class Md5Generator : ICheckSumGenerator
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
				return 20;
			}
		}
	}

}


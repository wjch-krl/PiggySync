using System.IO;
using System.Security;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto.Digests;
using PiggySync.Common.Abstract;

namespace PiggySync.Common.Concrete
{
    public class Md5Generator : ICheckSumGenerator
    {
        public byte[] ComputeChecksum(string filePath)
        {
			using (var md5 = TypeResolver.Md5())
            {
				using (var stream = TypeResolver.DirectoryHelper.OpenFileRead (filePath))
                {
                    return md5.ComputeHash(stream);
                }
            }
        }

		public byte[] ComputeChecksum(byte[] bytes)
        {
			using (var md5 = TypeResolver.Md5())
            {
                return md5.ComputeHash(bytes);
            }
        }

        public int ChecksumSize
        {
            get { return 16; }
        }
    }
}
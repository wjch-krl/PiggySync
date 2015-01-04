using System.IO;
using System.Security;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto.Digests;
using PiggySync.Common.Abstract;
using PCLStorage;

namespace PiggySync.Common.Concrete
{
    public class Md5Generator : ICheckSumGenerator
    {
        public async Task<byte[]> ComputeChecksum(string filePath)
        {
            using (var md5 = new MD5())
            {
                var files = await FileSystem.Current.GetFileFromPathAsync(filePath);
                using (var stream = await files.OpenAsync(FileAccess.Read))
                {
                    return md5.ComputeHash(stream);
                }
            }
        }

        public async Task<byte[]> ComputeChecksum(byte[] bytes)
        {
            using (var md5 = new MD5())
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
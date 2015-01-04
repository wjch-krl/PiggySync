using System;
using System.IO;
using System.Threading.Tasks;
using PCLStorage;
using PiggySync.Common.Abstract;

namespace PiggySync.Common.Concrete
{
    public class CRC32 : ICheckSumGenerator
    {
        private const int readBlockSize = 2048;
        private static readonly UInt32[] table;

        static CRC32()
        {
            var poly = 0xedb88320;
            table = new UInt32[256];
            UInt32 temp = 0;
            for (UInt32 i = 0; i < table.Length; ++i)
            {
                temp = i;
                for (var j = 8; j > 0; --j)
                {
                    if ((temp & 1) == 1)
                    {
                        temp = (temp >> 1) ^ poly;
                    }
                    else
                    {
                        temp >>= 1;
                    }
                }
                table[i] = temp;
            }
        }

        public int ChecksumSize
        {
            get { return sizeof (UInt32); }
        }

        static async Task<int> ComputeChecksumForFile(string filePath)
        {
            var file = await FileSystem.Current.GetFileFromPathAsync(filePath);

            using (var stream = await file.OpenAsync(FileAccess.Read))
            {
                using (var reader = new BinaryReader(stream))
                {
                    //TODO
                }
            }
            return 0;
        }

        static UInt32 ComputeChecksumForBytes(byte[] bytes)
        {
            var crc = 0xffffffff;
            foreach (byte b in bytes)
            {
                var index = (byte) (((crc) & 0xff) ^ b);
                crc = (crc >> 8) ^ table[index];
            }
            return ~crc;
        }


        public async Task<byte[]> ComputeChecksum(string filePath)
        {
            return BitConverter.GetBytes(await ComputeChecksumForFile(filePath));
        }

        public async Task<byte[]> ComputeChecksum(byte[] bytes)
        {
            return BitConverter.GetBytes(ComputeChecksumForBytes(bytes));
        }
    }
}
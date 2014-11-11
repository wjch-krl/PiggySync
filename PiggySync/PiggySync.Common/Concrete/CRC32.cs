using System;
using System.IO;
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

        byte[] ICheckSumGenerator.ComputeChecksum(FileInfo file)
        {
            return BitConverter.GetBytes(ComputeChecksum(file));
        }

        public int ChecksumSize
        {
            get { return sizeof (UInt32); }
        }

        byte[] ICheckSumGenerator.ComputeChecksum(byte[] bytes)
        {
            return BitConverter.GetBytes(ComputeChecksum(bytes));
        }

        public static UInt32 ComputeChecksum(FileInfo file)
        {
            using (var stream = new FileStream(file.FullName, FileMode.Open))
            {
                using (var reader = new BinaryReader(stream))
                {
                }
            }
            return 0;
            //file.
        }

        public static UInt32 ComputeChecksum(byte[] bytes)
        {
            var crc = 0xffffffff;
            for (var i = 0; i < bytes.Length; ++i)
            {
                var index = (byte) (((crc) & 0xff) ^ bytes[i]);
                crc = (crc >> 8) ^ table[index];
            }
            return ~crc;
        }
    }
}
using System;
using System.IO;
using PiggySync.Common.Abstract;

namespace PiggySync.Common.Concrete
{
	public class CRC32 : ICheckSumGenerator
	{
		static UInt32[] table;
		const int readBlockSize = 2048;

		public static UInt32 ComputeChecksum (FileInfo file)
		{
			using (FileStream stream = new FileStream (file.FullName, FileMode.Open))
			{
				using (BinaryReader reader = new BinaryReader (stream))
				{

				}
			}
			return 0;
			//file.
		}

		public static UInt32 ComputeChecksum (byte[] bytes)
		{
			UInt32 crc = 0xffffffff;
			for (int i = 0; i < bytes.Length; ++i)
			{
				byte index = (byte)(((crc) & 0xff) ^ bytes [i]);
				crc = (UInt32)((crc >> 8) ^ table [index]);
			}
			return ~crc;
		}

		static CRC32 ()
		{
			UInt32 poly = 0xedb88320;
			table = new UInt32[256];
			UInt32 temp = 0;
			for (UInt32 i = 0; i < table.Length; ++i)
			{
				temp = i;
				for (int j = 8; j > 0; --j)
				{
					if ((temp & 1) == 1)
					{
						temp = (UInt32)((temp >> 1) ^ poly);
					}
					else
					{
						temp >>= 1;
					}
				}
				table [i] = temp;
			}
		}

		byte[] ICheckSumGenerator.ComputeChecksum (FileInfo file)
		{
			return BitConverter.GetBytes (ComputeChecksum (file));
		}

		public int ChecksumSize {
			get
			{
				return sizeof(UInt32);
			}
		}

		byte[] ICheckSumGenerator.ComputeChecksum (byte[] bytes)
		{
			return BitConverter.GetBytes (ComputeChecksum (bytes));
		}
	}
}

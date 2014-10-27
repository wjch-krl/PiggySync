using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PiggySync.Core;
using PiggySyncWin.Domain;
using SQLite;
using System.IO;

namespace PiggySync.Model
{
	public class FileInf
	{
		public FileInf ()
		{
		}

		public FileInf (string path)
		{
			FileInfo fileInf = new FileInfo (path);
			FileName = fileInf.Name;
			FileSize = (UInt32)fileInf.Length;
			LastModyfied = (Int64)(fileInf.LastWriteTimeUtc - new DateTime (1970, 1, 1)).Ticks;
			CheckSum = CheckSumGenerator.ComputeChecksum (fileInf);

		}

		public FileInf (byte[] packet, UInt32 packetSize)
		{
			CheckSum = packet.SubArray (1 + sizeof(UInt32), CheckSumGenerator.ChecksumSize);
			LastModyfied = BitConverter.ToInt64 (packet, 1 + sizeof(UInt32) + CheckSumGenerator.ChecksumSize);
			FileSize = BitConverter.ToUInt32 (packet, 1 + sizeof(UInt32) + sizeof(Int64) + CheckSumGenerator.ChecksumSize);//TODO makie it easier
			FileName = System.Text.Encoding.UTF8.GetString (packet, 1 + 2 * sizeof(UInt32) + sizeof(Int64) + CheckSumGenerator.ChecksumSize,
				(int)packetSize - (1 + 2 * sizeof(UInt32) + sizeof(Int64) + CheckSumGenerator.ChecksumSize));
		}

		[PrimaryKey, AutoIncrement]
		public int Id
		{ 
			get;
			set;
		}

		[Ignore]
		public Int64 LastModyfied
		{
			get;
			set;
		}
			
		public DateTime LastMdyfiedDate
		{
			get
			{
				return new DateTime (1970, 1, 1).AddTicks (LastModyfied);
			}
			set
			{
				LastModyfied = (value - new DateTime (1970, 1, 1)).Ticks;
			}
		}

		public UInt32 FileSize
		{
			get;
			set;
		}

		[MaxLength (16)]
		public byte[] CheckSum
		{
			get;
			set;
		}

		public string FileName
		{
			get;
			set;
		}

		[Indexed]
		[MaxLength (1024)]
		public string Path { get; set; }

		public bool IsDeleted { get; set; }
	}
}

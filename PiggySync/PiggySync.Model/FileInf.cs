using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PiggySync.Core;
using PiggySyncWin.Domain;

namespace PiggySyncWin.WinUI.Models
{
    public class FileInf
    {
        public FileInf()
        {
        }

		public FileInf (byte[] packet, UInt32 packetSize)
		{
			CheckSum = packet.SubArray( 1 + sizeof(UInt32), CheckSumGenerator.ChecksumSize);
			LastModyfied = BitConverter.ToUInt64(packet, 1 + sizeof(UInt32)+ CheckSumGenerator.ChecksumSize);
			FileSize = BitConverter.ToUInt32(packet, 1 + sizeof(UInt32) + sizeof(UInt64)+ CheckSumGenerator.ChecksumSize);//TODO makie it easier
			FileName = System.Text.Encoding.UTF8.GetString(packet,1 + 2 * sizeof(UInt32) + sizeof(UInt64)+ CheckSumGenerator.ChecksumSize,
				(int)packetSize - (1 + 2 * sizeof(UInt32) + sizeof(UInt64)+ CheckSumGenerator.ChecksumSize));
		}
        
		public UInt64 LastModyfied
        {
            get;
            set;
        }

		public DateTime LastMdyfiedDate {
			get {
				return new DateTime (1970, 1, 1).AddTicks((long)LastModyfied);
			}
			set {
				LastModyfied = (UInt64)(value - new DateTime (1970, 1, 1)).Ticks;
			}
		}

        public UInt32 FileSize
        {
            get;
            set;
        }

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
    }
}

using System;
using System.IO;
using System.Text;
using PiggySync.Common.Concrete;
using PiggySync.Model;
using PiggySync.Common;
using PiggySync.Model.DatabaseConnection;

namespace PiggySync.Model
{
    public class FileInf
    {
        public FileInf()
        {
        }

        public FileInf(string path)
        {
            var fileInf = new FileInfo(path);
            FileName = fileInf.Name;
            if (fileInf.Length > UInt32.MaxValue)
            {
                throw new PiggyFileException("File is tooo Big, Max 4GiB files");
            }
            FileSize = (UInt32) fileInf.Length;
            LastModyfied = (fileInf.LastWriteTimeUtc - new DateTime(1970, 1, 1)).Ticks;
            CheckSum = CheckSumGenerator.ComputeChecksum(fileInf);
        }

        public FileInf(byte[] packet, UInt32 packetSize)
        {
            CheckSum = packet.SubArray(1 + sizeof (UInt32), CheckSumGenerator.ChecksumSize);
            LastModyfied = BitConverter.ToInt64(packet, 1 + sizeof (UInt32) + CheckSumGenerator.ChecksumSize);
            FileSize = BitConverter.ToUInt32(packet,
                1 + sizeof (UInt32) + sizeof (Int64) + CheckSumGenerator.ChecksumSize); //TODO makie it easier
            FileName = Encoding.UTF8.GetString(packet,
                1 + 2*sizeof (UInt32) + sizeof (Int64) + CheckSumGenerator.ChecksumSize,
                (int) packetSize - (1 + 2*sizeof (UInt32) + sizeof (Int64) + CheckSumGenerator.ChecksumSize));
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Ignore]
        public Int64 LastModyfied { get; set; }

        public DateTime LastModyfiedDate
        {
            get { return new DateTime(1970, 1, 1).AddTicks(LastModyfied); }
            set { LastModyfied = (value - new DateTime(1970, 1, 1)).Ticks; }
        }

        public UInt32 FileSize { get; set; }

        [MaxLength(16)]
        public byte[] CheckSum { get; set; }

        public string FileName { get; set; }

        [Indexed]
        [MaxLength(1024)]
        public string Path { get; set; }

        public bool IsDeleted { get; set; }
    }
}
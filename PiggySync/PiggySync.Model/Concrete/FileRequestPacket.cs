using System;
using System.Linq;
using System.Text;
using PiggySync.Common.Concrete;
using PiggySync.Model.Abstract;

namespace PiggySync.Model.Concrete
{
    public class FileRequestPacket : TCPPacket
    {
        public const byte FileRequestPacketCode = 240;
        private readonly FileInf file;

        public FileRequestPacket(FileInf file, string path, byte code = FileRequestPacketCode)
            : base(code)
        {
            this.file = file;
            FilePath = path;
            PacketSize =
                (UInt32)
                    (1 + 2*sizeof (UInt32) + sizeof (Int64) + file.FileName.Length + CheckSumGenerator.ChecksumSize);
        }

        public FileRequestPacket(byte[] packet, byte code = FileRequestPacketCode)
            : base(code)
        {
            PacketSize = BitConverter.ToUInt32(packet, 1);
            file = new FileInf(packet, PacketSize);
        }

        public UInt32 PacketSize { get; set; }

        public FileInf File
        {
            get { return file; }
        }


        public string FilePath { get; set; }

        public override byte[] GetPacket()
        {
            var packet = new byte[1];
            packet[0] = Code;
            packet = packet.Concat(BitConverter.GetBytes(PacketSize)).ToArray();
            packet = packet.Concat(file.CheckSum).ToArray();
            packet = packet.Concat(BitConverter.GetBytes(file.LastModyfied)).ToArray();
            packet = packet.Concat(BitConverter.GetBytes(file.FileSize)).ToArray();
            packet = packet.Concat(Encoding.UTF8.GetBytes(file.FileName)).ToArray();

            return packet;
        }
    }
}
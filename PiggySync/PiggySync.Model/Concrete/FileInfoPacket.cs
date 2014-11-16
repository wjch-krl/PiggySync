using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using PiggySync.Common.Concrete;
using PiggySync.Model.Abstract;

namespace PiggySync.Model.Concrete
{
    public class FileInfoPacket : TCPPacket, ICloneable
    {
        private readonly FileInf file;

        public FileInfoPacket(FileInf file, byte code = 170)
            : base(code)
        {
            this.file = file;
            PacketSize =
                (UInt32)
                    (1 + 2*sizeof (UInt32) + sizeof (Int64) + file.FileName.Length + CheckSumGenerator.ChecksumSize);
                //TODO refactor
        }

        public FileInfoPacket(byte[] packet, byte code = 170)
            : base(code)
        {
            PacketSize = BitConverter.ToUInt32(packet, 1);
            file = new FileInf(packet, PacketSize);
            Debug.WriteLine("Otrzymano inf o pliku: " + file.FileName + ".");
        }

        public FileInf File
        {
            get { return file; }
        }

        public UInt32 PacketSize { get; set; }

        public object Clone()
        {
            return new FileInfoPacket(file);
        }

        public override byte[] GetPacket()
        {
            var packet = new byte[1];
            packet[0] = Code;
            packet = packet.Concat(BitConverter.GetBytes(PacketSize)).ToArray();
            packet = packet.Concat(file.CheckSum).ToArray();
            packet = packet.Concat(BitConverter.GetBytes(file.LastModyfied)).ToArray();
            packet = packet.Concat(BitConverter.GetBytes(file.FileSize)).ToArray();
            packet = packet.Concat(Encoding.UTF8.GetBytes(file.FileName)).ToArray();
                //TODO check?? write unit test for this
            return packet;
        }
    }
}
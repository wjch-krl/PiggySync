using System;
using System.Linq;
using PiggySync.Core;
using PiggySync.Model.Abstract;

namespace PiggySync.Model.Concrete
{
    public class FileInfoPacket : TCPPacket, ICloneable
    {
        public FileInfoPacket(FileInf file, byte code = 170)
            : base(code)
        {
            this.file = file;
			PacketSize = (UInt32)( 1 + 2 * sizeof(UInt32) + sizeof(Int64) + file.FileName.Length + CheckSumGenerator.ChecksumSize);//TODO refactor
        }

        public FileInfoPacket(byte[] packet, byte code = 170)
            : base(code)
        {
            PacketSize = BitConverter.ToUInt32(packet, 1);
			this.file = new FileInf (packet,PacketSize);
            System.Diagnostics.Debug.WriteLine("Otrzymano inf o pliku: "+this.file.FileName + ".");
        }

        FileInf file;
        public FileInf File
        {
            get { return file; }
        }

        public UInt32 PacketSize
        {
            get;
            set;
        }

        public override byte[] GetPacket()
        {
            byte[] packet = new byte[1];
            packet[0] = Code;
            packet = packet.Concat(BitConverter.GetBytes(PacketSize)).ToArray();
            packet = packet.Concat(file.CheckSum).ToArray();
            packet = packet.Concat(BitConverter.GetBytes(file.LastModyfied)).ToArray();
            packet = packet.Concat(BitConverter.GetBytes(file.FileSize)).ToArray();
            packet = packet.Concat(System.Text.Encoding.UTF8.GetBytes(file.FileName)).ToArray(); //TODO check?? write unit test for this
            return packet;
        }

        public object Clone()
        {
            return new FileInfoPacket(file);
        }
    }
}

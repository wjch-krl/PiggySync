using PiggySyncWin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggySyncWin.WinUI.Models
{
    public class FileInfoPacket : TCPPacket, ICloneable
    {
        public FileInfoPacket(FileInf file, byte code = 170)
            : base(code)
        {
            this.file = file;
            PacketSize = (uint)( 1 + 3 * sizeof(UInt32) + sizeof(UInt64) + file.FileName.Length);//TODO 
        }

        public FileInfoPacket(byte[] packet, byte code = 170)
            : base(code)
        {
            PacketSize = BitConverter.ToUInt32(packet, 1);
            this.file = new FileInf()
            {
                CheckSum = BitConverter.ToUInt32(packet, 1 + sizeof(uint)),
                LastModyfied = BitConverter.ToUInt64(packet, 1 + 2*sizeof(uint)),
                FileSize = BitConverter.ToUInt32(packet, 1 + 2*sizeof(UInt32) + sizeof(UInt64)),
                FileName = System.Text.Encoding.UTF8.GetString(packet,
                    1 + 3 * sizeof(UInt32) + sizeof(UInt64),
                    (int)PacketSize - (1 + 3 * sizeof(UInt32) + sizeof(UInt64))),
            };

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
            packet[0] = code;
            packet = packet.Concat(BitConverter.GetBytes(PacketSize)).ToArray();
            packet = packet.Concat(BitConverter.GetBytes(file.CheckSum)).ToArray();
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

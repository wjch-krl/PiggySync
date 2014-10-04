using PiggySyncWin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggySyncWin.WinUI.Models
{
    class FileRequestPacket :  TCPPacket
    {
        private FileInf file;
        public UInt32 PacketSize
        {
            get;
            set;
        }
        public FileInf File
        {
            get { return file; }
        }

        public static byte Code
        {
            get { return 240; }
        }

        public string FilePath
        {
            get;
            set;
        }

        public FileRequestPacket(FileInf file,string path, byte code = 240)
            : base(code)
        {
            this.file = file;
            this.FilePath = path;
            PacketSize = (UInt32)( 1 + 3 * sizeof(UInt32) + sizeof(UInt64) + file.FileName.Length);
        }

        public FileRequestPacket(byte[] packet, byte code = 240)
            : base(code)
        {
            PacketSize = BitConverter.ToUInt32(packet, 1); 
            this.file = new FileInf()
            {
                CheckSum = BitConverter.ToUInt32(packet, 1 + sizeof(uint)),
                LastModyfied = BitConverter.ToUInt64(packet, 1 + 2*sizeof(uint)),
                FileSize = BitConverter.ToUInt32(packet, 1 + 2 * sizeof(uint) + sizeof(UInt64)),
                FileName = System.Text.Encoding.UTF8.GetString(packet, 1 + 3 * sizeof(uint) + sizeof(UInt64), (int)PacketSize - (1 + 3 * sizeof(uint) + sizeof(UInt64))),
            };
        }

        public override byte[] GetPacket()
        {
            byte[] packet = new byte[1];
            packet[0] = code;
            packet = packet.Concat(BitConverter.GetBytes(PacketSize)).ToArray();
            packet = packet.Concat(BitConverter.GetBytes(file.CheckSum)).ToArray();
            packet = packet.Concat(BitConverter.GetBytes(file.LastModyfied)).ToArray();
            packet = packet.Concat(BitConverter.GetBytes(file.FileSize)).ToArray();
            packet = packet.Concat(System.Text.Encoding.UTF8.GetBytes(file.FileName)).ToArray();

            return packet;
        }

    }
}

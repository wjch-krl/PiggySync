using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PiggySync.Common;

namespace PiggySync.Model.Concrete
{
    public class FolderInfoPacket : SyncInfoPacket, ICloneable
    {
        private readonly int PacketCode;
        private string folderName; //TODO

        public FolderInfoPacket(List<FileInfoPacket> files, List<FolderInfoPacket> subfolders,
            List<FileDeletePacket> deletedFiles, string name, byte packetCode = 10)
            : base(files, subfolders, deletedFiles, packetCode)
        {
            PacketCode = packetCode;
            folderName = name;
            PacketSize = (UInt32) (1 + 2*sizeof (UInt32) + Encoding.UTF8.GetBytes(folderName).Length);
        }

        public FolderInfoPacket(byte[] msg, byte packetCode = 10)
        {
            Code = packetCode;
            PacketSize = BitConverter.ToUInt32(msg, 1);
            ElelmentsCount = BitConverter.ToUInt32(msg, 1 + sizeof (UInt32));
            folderName = Encoding.UTF8.GetString(msg, 1 + 2*sizeof (UInt32), (int) PacketSize - (1 + 2*sizeof (UInt32)));
        }

        public FolderInfoPacket(FolderInfoPacket folderInfoPacket, byte packetCode = 10)
            : base(folderInfoPacket, packetCode)
        {
            folderName = folderInfoPacket.folderName;
            PacketSize = (UInt32) (1 + 2*sizeof (UInt32) + Encoding.UTF8.GetBytes(folderName).Length);
        }

        public FolderInfoPacket(string folderName, byte packetCode = 10)
            : base(packetCode)
        {
            this.folderName = folderName;
            PacketSize = (UInt32) (1 + 2*sizeof (UInt32) + Encoding.UTF8.GetBytes(folderName).Length);
        }

        public UInt32 PacketSize { get; set; }

        public string FolderName
        {
            get { return folderName; }
            set { folderName = value; }
        }

        public bool IsDeleted
        {
            get { return PacketCode != 10; }
        }

        public new object Clone()
        {
            return new FolderInfoPacket(this);
        }

        public override byte[] GetPacket()
        {
            var packet = new byte[1];
            packet[0] = Code;
            packet = packet.Concat(BitConverter.GetBytes(PacketSize)).ToArray();
            packet = packet.Concat(BitConverter.GetBytes(ElelmentsCount)).ToArray();
            packet = packet.Concat(Encoding.UTF8.GetBytes(folderName)).ToArray();

            return packet;
        }
    }
}
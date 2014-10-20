using PiggySyncWin.WinUI.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggySyncWin.WinUI.Models //TODO folder name
{
    public class FolderInfoPacket : SyncInfoPacket, ICloneable
    {
        public FolderInfoPacket(List<FileInfoPacket> files, List<FolderInfoPacket> subfolders, List<FileDeletePacket> deletedFiles, string name)
            : base(files, subfolders, deletedFiles, 10)
        {
            this.folderName = name;
            PacketSize = (UInt32)(1 + 2 * sizeof(UInt32) + System.Text.Encoding.UTF8.GetBytes(folderName).Length);
        }

        public UInt32 PacketSize
        {
            get;
            set;
        }

        public FolderInfoPacket(byte[] msg)
        {
            this.code = 10;
            this.PacketSize = BitConverter.ToUInt32(msg, 1);
            this.ElelmentsCount = BitConverter.ToUInt32(msg, 1+sizeof(UInt32));
            this.folderName = System.Text.Encoding.UTF8.GetString(msg, 1 + 2*sizeof(UInt32),(int) PacketSize - (1 + 2*sizeof(UInt32)));
        }

        public FolderInfoPacket(FolderInfoPacket folderInfoPacket) : base (folderInfoPacket,10)
        {
            this.folderName = folderInfoPacket.folderName;
            PacketSize = (UInt32)( 1 + 2 * sizeof(UInt32) + System.Text.Encoding.UTF8.GetBytes(folderName).Length);
        }

        public FolderInfoPacket(string folderName)
            : base(10)
        {
            this.folderName = folderName;
            PacketSize = (UInt32)(1 + 2 * sizeof(UInt32) + System.Text.Encoding.UTF8.GetBytes(folderName).Length);
        }

        string folderName; //TODO
        public string FolderName
        {
            get { return folderName; }
            set { folderName = value; }
        }

        public override byte[] GetPacket()
        {
            byte[] packet = new byte[1];
            packet[0] = code;
            packet = packet.Concat(BitConverter.GetBytes(PacketSize)).ToArray();
            packet = packet.Concat(BitConverter.GetBytes(ElelmentsCount)).ToArray();
            packet = packet.Concat(System.Text.Encoding.UTF8.GetBytes(folderName)).ToArray();

            return packet;
        }

        public new object Clone()
        {
            return new FolderInfoPacket(this);
        }
    }
}

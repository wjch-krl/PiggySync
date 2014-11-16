using System;
using System.Collections.Generic;
using System.Linq;
using PiggySyncWin.WinUI.Models.Concrete;
using PiggySyncWin.WinUI.Models;

namespace PiggySync.Model.Concrete
{
    public class FolderInfoPacket : SyncInfoPacket, ICloneable
    {
		private readonly int PacketCode;

		public FolderInfoPacket(List<FileInfoPacket> files, List<FolderInfoPacket> subfolders, 
			List<FileDeletePacket> deletedFiles, string name, byte packetCode = 10)
			: base(files, subfolders, deletedFiles, packetCode)
        {
			PacketCode = packetCode;
            this.folderName = name;
            PacketSize = (UInt32)(1 + 2 * sizeof(UInt32) + System.Text.Encoding.UTF8.GetBytes(folderName).Length);
        }

        public UInt32 PacketSize
        {
            get;
            set;
        }

		public FolderInfoPacket(byte[] msg,byte packetCode =10)
        {
			this.Code = packetCode;
            this.PacketSize = BitConverter.ToUInt32(msg, 1);
            this.ElelmentsCount = BitConverter.ToUInt32(msg, 1+sizeof(UInt32));
            this.folderName = System.Text.Encoding.UTF8.GetString(msg, 1 + 2*sizeof(UInt32),(int) PacketSize - (1 + 2*sizeof(UInt32)));
        }

		public FolderInfoPacket(FolderInfoPacket folderInfoPacket,byte packetCode =10) : base (folderInfoPacket,packetCode)
        {
            this.folderName = folderInfoPacket.folderName;
            PacketSize = (UInt32)( 1 + 2 * sizeof(UInt32) + System.Text.Encoding.UTF8.GetBytes(folderName).Length);
        }

		public FolderInfoPacket(string folderName,byte packetCode =10)
			: base(packetCode)
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
			packet[0] = Code;
            packet = packet.Concat(BitConverter.GetBytes(PacketSize)).ToArray();
            packet = packet.Concat(BitConverter.GetBytes(ElelmentsCount)).ToArray();
            packet = packet.Concat(System.Text.Encoding.UTF8.GetBytes(folderName)).ToArray();

            return packet;
        }

        public new object Clone()
        {
            return new FolderInfoPacket(this);
        }

		public bool IsDeleted
		{
			get { return PacketCode != 10; }
		}
    }
}

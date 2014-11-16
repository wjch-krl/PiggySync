using System;
using System.Collections.Generic;
using System.Linq;
using PiggySync.Common;
using PiggySync.Model.Abstract;

namespace PiggySync.Model.Concrete
{
    public class SyncInfoPacket : TCPPacket, ICloneable
    {
        public const byte SyncInfoPacketCode = 255;

        public SyncInfoPacket(List<FileInfoPacket> files, List<FolderInfoPacket> folders,
            List<FileDeletePacket> deletedFiles)
            : base(SyncInfoPacketCode)
        {
            Files = files;
            Folders = folders;
            DeletedFiles = deletedFiles;
            ElelmentsCount = GetFileCount();
        }

        public SyncInfoPacket(byte[] data, byte code = SyncInfoPacketCode)
            : base(code)
        {
            ElelmentsCount = BitConverter.ToUInt32(data, 1);
            Files = new List<FileInfoPacket>();
            Folders = new List<FolderInfoPacket>();
            DeletedFiles = new List<FileDeletePacket>();
        }

        //lllll
        protected SyncInfoPacket(SyncInfoPacket syncInfoPacket, byte code = SyncInfoPacketCode)
            : base(code)
        {
            Files = syncInfoPacket.Files.Clone();
            Folders = syncInfoPacket.Folders.Clone();
            DeletedFiles = syncInfoPacket.DeletedFiles.Clone();
            ElelmentsCount = GetFileCount();
        }

        protected SyncInfoPacket(List<FileInfoPacket> files, List<FolderInfoPacket> folders,
            List<FileDeletePacket> deletedFiles, byte code)
            : base(code)
        {
            Files = files;
            Folders = folders;
            DeletedFiles = deletedFiles;
            ElelmentsCount = GetFileCount();
        }

        public SyncInfoPacket()
            : base(SyncInfoPacketCode)
        {
            Files = new List<FileInfoPacket>();
            Folders = new List<FolderInfoPacket>();
            DeletedFiles = new List<FileDeletePacket>();
        }

        public SyncInfoPacket(byte code) : base(code)
        {
            Files = new List<FileInfoPacket>();
            Folders = new List<FolderInfoPacket>();
            DeletedFiles = new List<FileDeletePacket>();
        }

        public List<FileDeletePacket> DeletedFiles { get; set; }

        public List<FileInfoPacket> Files { get; set; }

        public List<FolderInfoPacket> Folders { get; set; }
        public UInt32 ElelmentsCount { get; set; }

        public object Clone()
        {
            return new SyncInfoPacket(this);
        }

        public override byte[] GetPacket()
        {
            var packet = new byte[1];
            packet[0] = Code;

            packet = packet.Concat(BitConverter.GetBytes(ElelmentsCount)).ToArray();
            return packet;
        }

        public UInt32 GetFileCount()
        {
            return (UInt32) (Files.Count + Folders.Count + DeletedFiles.Count);
        }

        public byte[][] GetFilePackets() //TODO Those methods must be tested well
        {
            return GetPackets(Files);
        }

        public byte[][] GetDeletedFilePackets() //TODO Those methods must be tested well
        {
            return GetPackets(DeletedFiles);
        }

        private byte[][] GetPackets(IEnumerable<TCPPacket> packets)
        {
            var packetsData = new byte[packets.Count()][];
            int i = 0;
            foreach (var x in packets)
            {
                packetsData[i++] = x.GetPacket();
            }
            return packetsData;
        }

        public SyncInfoPacket Copy()
        {
            return new SyncInfoPacket(this);
        }
    }
}
using PiggySyncWin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PiggySyncWin.Domain;
using PiggySyncWin.WinUI.Models.Concrete;

namespace PiggySyncWin.WinUI.Models
{
    public class SyncInfoPacket : TCPPacket, ICloneable
    {
        public const byte Code = 255;

        public SyncInfoPacket(List<FileInfoPacket> files, List<FolderInfoPacket> folders,List<FileDeletePacket> deletedFiles)
            : base(Code)
        {
            this.files = files;
            this.folders = folders;
            this.deletedFiles = deletedFiles;
            this.ElelmentsCount = this.GetFileCount();
        }

        public SyncInfoPacket(byte[] data, byte code = Code)
            : base(code)
        {
            this.ElelmentsCount = BitConverter.ToUInt32(data, 1);
            this.files = new List<FileInfoPacket>();
            this.folders = new List<FolderInfoPacket>();
            this.deletedFiles = new List<FileDeletePacket>();
        }

        //lllll
        protected SyncInfoPacket(SyncInfoPacket syncInfoPacket, byte code = Code)
            : base(code)
        {
            this.files = syncInfoPacket.files.Clone();
            this.folders = syncInfoPacket.folders.Clone();
            this.deletedFiles = syncInfoPacket.deletedFiles.Clone();
            this.ElelmentsCount = GetFileCount();
        }

        protected SyncInfoPacket(List<FileInfoPacket> files, List<FolderInfoPacket> folders,List<FileDeletePacket> deletedFiles, byte code)
            : base(code)
        {
            this.files = files;
            this.folders = folders;
            this.deletedFiles = deletedFiles;
            this.ElelmentsCount = GetFileCount();
        }

        public SyncInfoPacket()
            : base(Code)
        {
            this.files = new List<FileInfoPacket>();
            this.folders = new List<FolderInfoPacket>();
            this.deletedFiles = new List<FileDeletePacket>();
        }

        public SyncInfoPacket(byte code): base(code)
        {
            this.files = new List<FileInfoPacket>();
            this.folders = new List<FolderInfoPacket>();
            this.deletedFiles = new List<FileDeletePacket>();
        }


        private List<FileDeletePacket> deletedFiles;
        public List<FileDeletePacket> DeletedFiles
        {
            get { return deletedFiles; }
            set { deletedFiles = value; }
        }
        private List<FileInfoPacket> files;
        public List<FileInfoPacket> Files
        {
            get { return files; }
            set { files = value; }
        }
        private List<FolderInfoPacket> folders;

        public List<FolderInfoPacket> Folders
        {
            get { return folders; }
            set { folders = value; }
        }

        public override byte[] GetPacket()
        {
            byte[] packet = new byte[1];
            packet[0] = code;

            packet = packet.Concat(BitConverter.GetBytes(ElelmentsCount)).ToArray();
            return packet;
        }

        public UInt32 GetFileCount()
        {
            //UInt32 tmp=(UInt32)files.Count;
            //foreach (var x in folders)
            //{
            //    tmp += x.GetFileCount();
            //}
            //return tmp;
            return (UInt32) (files.Count + folders.Count + deletedFiles.Count);
        }

        public byte[][] GetFilePackets() //TODO Those methods must be tested well
        {
            byte[][] packets= new byte[files.Count][];
            int i = 0;
            foreach (var x in files)
            {
                packets[i++] = x.GetPacket();
            }

            return packets;
        }

        public byte[][] GetDeletedFilePackets() //TODO Those methods must be tested well
        {
            byte[][] packets = new byte[deletedFiles.Count][];
            int i = 0;
            foreach (var x in deletedFiles)
            {
                packets[i++] = x.GetPacket();
            }

            return packets;
        }

        public SyncInfoPacket Copy()
        {
            return new SyncInfoPacket(this);
        }

        public UInt32 ElelmentsCount { get; set; }

        public object Clone()
        {
            return new SyncInfoPacket(this);
        }
    }
}

using PiggySyncWin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PiggySyncWin.WinUI.Models.Concrete;
using PiggySyncWin.Domain;

namespace PiggySyncWin.WinUI.Models
{
	public class SyncInfoPacket : TCPPacket, ICloneable
	{
		public const byte Code = 255;

		public SyncInfoPacket (List<FileInfoPacket> files, List<FolderInfoPacket> folders, List<FileDeletePacket> deletedFiles)
			: base (Code)
		{
			this.Files = files;
			this.Folders = folders;
			this.DeletedFiles = deletedFiles;
			this.ElelmentsCount = this.GetFileCount ();
		}

		public SyncInfoPacket (byte[] data, byte code = Code)
			: base (code)
		{
			this.ElelmentsCount = BitConverter.ToUInt32 (data, 1);
			this.Files = new List<FileInfoPacket> ();
			this.Folders = new List<FolderInfoPacket> ();
			this.DeletedFiles = new List<FileDeletePacket> ();
		}

		//lllll
		protected SyncInfoPacket (SyncInfoPacket syncInfoPacket, byte code = Code)
			: base (code)
		{
			this.Files = syncInfoPacket.Files.Clone ();
			this.Folders = syncInfoPacket.Folders.Clone ();
			this.DeletedFiles = syncInfoPacket.DeletedFiles.Clone ();
			this.ElelmentsCount = GetFileCount ();
		}

		protected SyncInfoPacket (List<FileInfoPacket> files, List<FolderInfoPacket> folders, List<FileDeletePacket> deletedFiles, byte code)
			: base (code)
		{
			this.Files = files;
			this.Folders = folders;
			this.DeletedFiles = deletedFiles;
			this.ElelmentsCount = GetFileCount ();
		}

		public SyncInfoPacket ()
			: base (Code)
		{
			this.Files = new List<FileInfoPacket> ();
			this.Folders = new List<FolderInfoPacket> ();
			this.DeletedFiles = new List<FileDeletePacket> ();
		}

		public SyncInfoPacket (byte code) : base (code)
		{
			this.Files = new List<FileInfoPacket> ();
			this.Folders = new List<FolderInfoPacket> ();
			this.DeletedFiles = new List<FileDeletePacket> ();
		}

		public List<FileDeletePacket> DeletedFiles { get; set; }

		public List<FileInfoPacket> Files { get; set; }

		public List<FolderInfoPacket> Folders { get; set; }

		public override byte[] GetPacket ()
		{
			byte[] packet = new byte[1];
			packet [0] = code;

			packet = packet.Concat (BitConverter.GetBytes (ElelmentsCount)).ToArray ();
			return packet;
		}

		public UInt32 GetFileCount ()
		{
			return (UInt32)(Files.Count + Folders.Count + DeletedFiles.Count);
		}

		public byte[][] GetFilePackets () //TODO Those methods must be tested well
		{
			return GetPackets (Files);

		}

		public byte[][] GetDeletedFilePackets () //TODO Those methods must be tested well
		{
			return GetPackets (DeletedFiles);
		}

		byte[][] GetPackets (IEnumerable<TCPPacket> packets)
		{
			byte[][] packetsData = new byte[packets.Count()][];
			int i = 0;
			foreach (var x in packets)
			{
				packetsData [i++] = x.GetPacket ();
			}
			return packetsData;
		}

		public SyncInfoPacket Copy ()
		{
			return new SyncInfoPacket (this);
		}

		public UInt32 ElelmentsCount { get; set; }

		public object Clone ()
		{
			return new SyncInfoPacket (this);
		}
	}
}

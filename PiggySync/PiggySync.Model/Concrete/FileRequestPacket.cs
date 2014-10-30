using PiggySyncWin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PiggySync.Core;
using PiggySync.Model;

namespace PiggySyncWin.WinUI.Models
{
	public class FileRequestPacket :  TCPPacket
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

		public const byte FileRequestPacketCode = 240;


		public string FilePath
		{
			get;
			set;
		}

		public FileRequestPacket (FileInf file, string path, byte code = FileRequestPacketCode)
			: base (code)
		{
			this.file = file;
			this.FilePath = path;
			PacketSize = (UInt32)(1 + 2 * sizeof(UInt32) + sizeof(Int64) + file.FileName.Length + CheckSumGenerator.ChecksumSize);
		}

		public FileRequestPacket (byte[] packet, byte code = FileRequestPacketCode)
			: base (code)
		{
			PacketSize = BitConverter.ToUInt32 (packet, 1); 
			this.file = new FileInf (packet, PacketSize);        
		}

		public override byte[] GetPacket ()
		{
			byte[] packet = new byte[1];
			packet [0] = Code;
			packet = packet.Concat (BitConverter.GetBytes (PacketSize)).ToArray ();
			packet = packet.Concat (file.CheckSum).ToArray ();
			packet = packet.Concat (BitConverter.GetBytes (file.LastModyfied)).ToArray ();
			packet = packet.Concat (BitConverter.GetBytes (file.FileSize)).ToArray ();
			packet = packet.Concat (System.Text.Encoding.UTF8.GetBytes (file.FileName)).ToArray ();

			return packet;
		}

	}
}

using PiggySync.Common;
using PiggySync.Model.Abstract;
using PiggySync.Model.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace PiggySync.Core
{
	class TCPPacketReCreator
	{
		public static byte[] tmpBuffer;

		public static List<TCPPacket> RecrateFromRecivedData (byte[] recivedData, int bytes)
		{
			if (tmpBuffer != null)
			{
				recivedData = tmpBuffer.Concat (recivedData).ToArray ();
			}
			var packets = new List<TCPPacket> ();
			byte[] data = recivedData;
			int singleLen = 0;
			int pointer = 0;

			if (recivedData [0] == 240 || recivedData [0] == 170 || recivedData [0] == 10 || recivedData [0] == 0)
			{
				singleLen = (int)BitConverter.ToUInt32 (data, 1);
			}
			else
			{
				singleLen = getLenghtForPacketType (recivedData [0]);
			}

			while (pointer < bytes)
			{  
				try
				{
					data = recivedData.SubArray (pointer, recivedData.Length - pointer);
					tmpBuffer = null;
				}
				catch (Exception e)
				{
					Debug.WriteLine (e);
					tmpBuffer = recivedData.SubArray (pointer, bytes - pointer);
					return packets;
				}

				if (data [0] == 240 || data [0] == 170 || data [0] == 10 || data [0] == 0)
				{
					singleLen = (int)BitConverter.ToUInt32 (data, 1);
				}
				else
				{
					singleLen = getLenghtForPacketType (data [0]);
				}
				pointer += singleLen;
				switch (data [0])
				{
				case 255:
					var root = new SyncInfoPacket (data);
					packets.Add (root);
					break;
				case 240:
					var filereq = new FileRequestPacket (data);
					packets.Add (filereq);
					break;
				case 170:
					var file = new FileInfoPacket (data);
					System.Diagnostics.Debug.WriteLine ("File: " + file.File.FileSize + " " + file.File.FileName + " " + file.File.LastModyfiedDate);
					packets.Add (file);
					break;
				case 160:
					var noPacket = new NoRequestPacket ();
					packets.Add (noPacket);
					break;
				case 10:
					var folder = new FolderInfoPacket (data);
					packets.Add (folder);
					break;
				case 0:
					var deletedFile = new FileDeletePacket (data);
					packets.Add (deletedFile);
					break;
				default:
					throw new Exception ("Shit just happened " + data [0].ToString ());
				}
			}
			return packets;
		}

		private static int noRequestPacketSize = 1;
		private static int syncInfoPacketSize = 1 + sizeof(UInt32);

		public static int getLenghtForPacketType (byte code)
		{
			switch (code)
			{
			case SyncInfoPacket.SyncInfoPacketCode:
				return syncInfoPacketSize;
			case NoRequestPacket.NoRequestPacketCode:
				return noRequestPacketSize;
			default:
				return -1;
			}
		}
	}
}

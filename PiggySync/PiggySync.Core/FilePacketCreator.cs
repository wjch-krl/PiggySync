using System.IO;
using PiggySync.Common;

namespace PiggySync.Core
{
    public static class FilePacketCreator
    {
        public static byte[] CreatePacket(string filePath, int position, int size)
		{
			byte[] packet;
			using (var fs = TypeResolver.DirectoryHelper.OpenFileRead (filePath))
			{
				var br = new BinaryReader (fs);
				fs.Seek (position * size, SeekOrigin.Begin);
				packet = br.ReadBytes (size);
			}
			return packet;
		}
    }
}
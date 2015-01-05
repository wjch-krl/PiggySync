using System.IO;
using PCLStorage;

namespace PiggySync.Core
{
    public static class FilePacketCreator
    {
        public static byte[] CreatePacket(string filePath, int position, int size)
        {
            byte[] packet;
            var file = FileSystem.Current.GetFileFromPathAsync(filePath).Result;
            using (var fs = file.OpenAsync(FileAccess.Read).Result)
            {
                var br = new BinaryReader(fs);
                fs.Seek(position*size, SeekOrigin.Begin);
                packet = br.ReadBytes(size);
            }
            return packet;
        }
    }
}
using System.IO;

namespace PiggySync.Core
{
    public static class FilePacketCreator
    {
        public static byte[] CreatePacket(string filePath, int position, int size)
        {
            var packet = new byte[size];
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var br = new BinaryReader(fs);
                fs.Seek(position*size, SeekOrigin.Begin);
                packet = br.ReadBytes(size);
            }
            return packet;
        }
    }
}
using PiggySyncWin.Domain.Concrete;
using System.IO;

namespace PiggySyncWin.WinUI.Infrastructure
{
    public static class FilePacketCreator
    {
        public static byte[] CreatePacket(string filePath, int position, int size)
        {
            byte[] packet = new byte[size];
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                BinaryReader br = new BinaryReader(fs);
                fs.Seek(position * size, SeekOrigin.Begin);
                packet = br.ReadBytes(size);
            }
            return packet;
        }
    }
}

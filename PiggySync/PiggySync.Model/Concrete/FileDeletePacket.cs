using PiggySyncWin.WinUI.Models;

namespace PiggySync.Model.Concrete
{
    public class FileDeletePacket : FileInfoPacket
    {
        public FileDeletePacket(FileInf file)
            : base (file, 0)
        {
        }

        public FileDeletePacket(byte[] packet)
            : base(packet, 0)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PiggySync.Model;

namespace PiggySyncWin.WinUI.Models.Concrete
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

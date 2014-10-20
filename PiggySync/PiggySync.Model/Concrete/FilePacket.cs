using PiggySyncWin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggySyncWin.WinUI.Models
{
	public class FilePacket
    {
        public FilePacket() //TODO          
        {
            
        }


        /// <summary>
        /// Firt Byte value must be 0
        /// </summary>

        public byte[] RawData
        {
            get;
            set;
        }

        public byte[] GetPacket()
        {
            return RawData;
        }

    }
}

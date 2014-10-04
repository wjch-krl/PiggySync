using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PiggySyncWin.WinUI.Models
{
    public class FileInf
    {
        public FileInf()
        {
        }
        
        public ulong LastModyfied
        {
            get;
            set;
        }

        public uint FileSize
        {
            get;
            set;
        }

        public uint CheckSum
        {
            get;
            set;
        }

        public string FileName
        {
            get;
            set;
        }
    }
}

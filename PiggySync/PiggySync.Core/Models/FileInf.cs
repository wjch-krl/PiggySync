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
        
		public UInt64 LastModyfied
        {
            get;
            set;
        }

		public DateTime LastMdyfiedDate {
			get {
				return new DateTime (1970, 1, 1).AddTicks((long)LastModyfied);
			}
			set {
				LastModyfied = (UInt64)(value - new DateTime (1970, 1, 1)).Ticks;
			}
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

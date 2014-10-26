using System;
using System.Collections.Generic;
using PiggySyncWin.WinUI.Models;

namespace PiggySync.Model
{
	public class FileInfComparer:IEqualityComparer<FileInf>
	{
		public FileInfComparer ()
		{
		}

		public bool Equals (FileInf x, FileInf y)
		{
			throw new NotImplementedException ();
		}

		public int GetHashCode (FileInf obj)
		{
			throw new NotImplementedException ();
		}
	}
}


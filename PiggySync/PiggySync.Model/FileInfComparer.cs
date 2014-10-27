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
			return x.FileName == y.FileName && x.Path == y.Path;
		}

		public int GetHashCode (FileInf obj)
		{
			return String.Format ("{0}_{1}",obj.FileName,obj.Path).GetHashCode ();
		}
	}
}


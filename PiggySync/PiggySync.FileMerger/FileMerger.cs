using System;
using System.IO;
using PiggySync.Model;

namespace PiggySync.FileMerger
{
	public class FileMerger
	{
		public FileMerger (string fileAPath, string fileBPath)
		{
			string extension;
			if ((extension = Path.GetExtension (fileAPath)) != Path.GetExtension (fileBPath))
			{
				throw new PiggyFileException ("File extensions dosn't math");
			}

		}

		public void MergeFiles()
		{

		}
	}
}


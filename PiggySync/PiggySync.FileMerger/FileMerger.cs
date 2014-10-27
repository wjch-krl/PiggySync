using System;
using System.IO;
using PiggySync.Model;
using PiggySyncWin.Domain;
using PiggySyncWin.Domain.Concrete;
using System.Linq;

namespace PiggySync.FileMerger
{
	public class FileMerger
	{
		string fileAPath;
		string fileBPath;
		string resultPath;

		public FileMerger (string fileAPath, string fileBPath,string resultPath)
		{
			string extension;
			if ((extension = Path.GetExtension (fileAPath)) != Path.GetExtension (fileBPath))
			{
				throw new PiggyFileException ("File extensions dosn't math");
			}
			if (!IsTextFile (extension))
			{
				throw new PiggyFileException ("Not a text file");
			}
			this.fileAPath = fileAPath;
			this.fileBPath = fileBPath;
			this.resultPath = resultPath;
		}

		public void MergeFiles()
		{
			string fileA = File.ReadAllText(fileAPath);
			string fileB = File.ReadAllText(fileBPath);
			string merged = String.Empty;

			merged = String.Format ("<<<<<<<<<<<\n{0}\n>>>>>>>>>>>>>\n<<<<<<<<<<<\n{1}\n>>>>>>>>>>>>>\n", fileA, fileB);

			File.WriteAllText (resultPath,merged);
		}

		public static bool IsTextFile (string extension)
		{
			return XmlSettingsRepository.Instance.Settings.TextFiles.Contains (extension);
		}
	}
}


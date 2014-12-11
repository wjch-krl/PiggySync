using PiggySync.Domain;
using PiggySync.Domain.Concrete;
using System.Linq;
using System.IO;

namespace PiggySync.FileMerger
{
	public static class FileTools
	{
		public static TextFile TextFile (string extension, string aPath, string bPath)
		{
			var file = XmlSettingsRepository.Instance.Settings.TextFiles.FirstOrDefault (
				x => x.Extension == extension
			);
			if (file != null)
			{
				return file;
			}
			if (IsTextFile (aPath) && IsTextFile (bPath))
			{
				return	XmlSettingsRepository.Instance.Settings.TextFiles.First (x => x.Extension == ".txt");
			}
		    return null;
		}

		static bool IsTextFile (string filePath)
		{
			using (var stream = File.OpenRead (filePath))
			{
				byte[] testArr = new byte[2048];
				int len = stream.Read (testArr, 0, testArr.Length);
				for (int i = 0; i < len - 1; i++)
					if (testArr [i] == 0 && testArr [i + 1] == 0) //Sprawdzamy czy w pliku występuą dwa kolejne zera
						return false;
			}
			return true;
		}
	}
}


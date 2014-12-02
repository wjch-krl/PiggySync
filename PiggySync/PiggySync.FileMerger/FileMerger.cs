using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using PiggySync.Domain;
using PiggySync.Domain.Concrete;
using PiggySync.Model;
using System.Xml.Linq;
using PiggySync.Common;

namespace PiggySync.FileMerger
{
	public class FileMerger
	{
		private readonly string fileAPath;
		private readonly string fileBPath;
		private readonly string resultPath;
		private MergePattern pattern;
		bool usePattern;
		const double NullCharCoeficent = 0.001;
		const double ControlCharCoeficent = 0.01;

		public FileMerger (string fileAPath, string fileBPath, string resultPath)
		{
			string extension;
			if ((extension = Path.GetExtension (fileAPath)) != Path.GetExtension (fileBPath))
			{
				throw new PiggyFileException ("File extensions dosn't math");
			}
			var file = TextFile (extension,fileAPath,fileBPath);
			usePattern = string.IsNullOrWhiteSpace (file.TemplatePath);

			if (!usePattern)
			{
				var serializer = new XmlSerializer (typeof(MergePattern));
				pattern = (MergePattern)serializer.Deserialize (File.OpenRead (file.TemplatePath));
			}
			this.fileAPath = fileAPath;
			this.fileBPath = fileBPath;
			this.resultPath = resultPath;
		}

		public bool MergeFiles ()
		{
			string fileA = File.ReadAllText (fileAPath);
			string fileB = File.ReadAllText (fileBPath);
			string merged = String.Empty;

			var differ = new Differ ();
			var inlineBuilder = new InlineDiffBuilder (differ);
			var result = inlineBuilder.BuildDiffModel (fileA, fileB);
			foreach (var line in result.Lines)
			{
				if (line.Type == ChangeType.Inserted)
				{
					merged += String.Format ("+ {0}\n", line.Text);
				}
				else if (line.Type == ChangeType.Deleted)
				{
					merged += String.Format ("- {0}\n", line.Text);
				}
				else
				{
					merged += String.Format ("{0}\n", line.Text);
				}
			}
			File.WriteAllText (resultPath, merged);
			return ValidateFile (ref merged);
		}

	    bool ValidateFile (ref string resultFile)
		{
			return resultFile.CountSubstrings(pattern.TagOpenString)
				== resultFile.CountSubstrings (pattern.TagCloseString);
		}

		public static TextFile TextFile (string extension,string aPath,string bPath)
		{
			try
			{
					return XmlSettingsRepository.Instance.Settings.TextFiles.FirstOrDefault (
					x => x.Extension == extension
				);
			}
			catch (Exception e)
			{
				if (IsTextFile (aPath) && IsTextFile (bPath))
				{
					return	XmlSettingsRepository.Instance.Settings.TextFiles.First ( x => x.Extension == "txt");

				}
				throw new PiggyFileException ("Not a text file", e);
			}
		}

		static bool IsTextFile (string filePath)
		{
			using (var stream = File.OpenRead (filePath))
			{
				byte[] testArr = new byte[2048];
				int len = stream.Read (testArr, 0, testArr.Length);
				double nullIndicator = testArr.Take (len).Count (x => x == 0) / len;
				double controlIndicator = testArr.Take (len).Count (x => x <= 0x0f || x == 0x7f) / len;
				return (nullIndicator < NullCharCoeficent && controlIndicator > ControlCharCoeficent);
			}
		}
	}
}
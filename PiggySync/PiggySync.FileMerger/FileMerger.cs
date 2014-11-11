using System;
using System.IO;
using PiggySync.Model;
using PiggySyncWin.Domain;
using PiggySyncWin.Domain.Concrete;
using System.Linq;
using DiffPlex;
using System.Diagnostics;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using System.Xml.Serialization;
using PiggySync.Domain;

namespace PiggySync.FileMerger
{
	public class FileMerger
	{
		string fileAPath;
		string fileBPath;
		string resultPath;
		MergePattern pattern;

		public FileMerger (string fileAPath, string fileBPath, string resultPath)
		{
			string extension;
			if ((extension = Path.GetExtension (fileAPath)) != Path.GetExtension (fileBPath))
			{
				throw new PiggyFileException ("File extensions dosn't math");
			}
			var file = TextFile (extension);
			var serializer = new XmlSerializer (typeof(MergePattern));
			Stream stream = new FileStream (file.TemplatePath, FileMode.Open, FileAccess.Read);
			pattern = (MergePattern)serializer.Deserialize (stream);
			this.fileAPath = fileAPath;
			this.fileBPath = fileBPath;
			this.resultPath = resultPath;
		}

		public void MergeFiles ()
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
					merged += String.Format ("- {0}\n", line.Text);
				}
				else if (line.Type == ChangeType.Deleted)
				{
					merged += String.Format ("+ {0}\n", line.Text);
				}
				else
				{
					merged += String.Format ("{0}\n", line.Text);
				}

				Console.WriteLine (line.Text);
			}
			File.WriteAllText (resultPath, merged);
		}

		public static TextFile TextFile (string extension)
		{
			try
			{
				return XmlSettingsRepository.Instance.Settings.TextFiles.First (x => x.Extension == extension);		
			}
			catch (Exception e)
			{
				throw new PiggyFileException ("Not a text file");
			}
		}
	}
}


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
		private readonly string fileCPath;
		private readonly string resultPath;
		private MergePattern pattern;
		bool usePattern;

		public FileMerger (string fileAPath, string fileBPath, string fileCPath, string resultPath)
		{
			string extension;
			if ((extension = Path.GetExtension (fileAPath)) != Path.GetExtension (fileBPath))
			{
				throw new PiggyFileException ("File extensions dosn't math");
			}
			var file = FileTools.TextFile (extension, fileAPath, fileBPath);
			usePattern = string.IsNullOrWhiteSpace (file.TemplatePath);

			if (!usePattern)
			{
				var serializer = new XmlSerializer (typeof(MergePattern));
				pattern = (MergePattern)serializer.Deserialize (File.OpenRead (file.TemplatePath));
			}
			this.fileAPath = fileAPath;
			this.fileBPath = fileBPath;
			this.fileCPath = fileCPath;
			this.resultPath = resultPath;
		}

		public bool MergeFiles ()
		{
			string fileA = File.ReadAllText (fileAPath);
			string fileB = File.ReadAllText (fileBPath);
			string fileC = File.ReadAllText (fileCPath);
			string merged = String.Empty;

			var differ = new Differ ();
			var inlineBuilder = new InlineDiffBuilder (differ);
			var diferenceAB = inlineBuilder.BuildDiffModel (fileA, fileB);
			var diferenceAC = inlineBuilder.BuildDiffModel (fileA, fileC);

			foreach (var line in diferenceAB.Lines)
			{
				if (line.Type == ChangeType.Inserted)
				{
					merged = String.Format ("{0}\n{1}", merged, line.Text);
				}
				else if (line.Type == ChangeType.Deleted)
				{
					//merged += String.Format ("- {0}\n", line.Text);
				}
				else
				{
					merged += String.Format ("{0}\n", line.Text);
				}
			}
			File.WriteAllText (resultPath, merged);
			return  Validator.ValidateXML (ref merged);
		}
	}
}
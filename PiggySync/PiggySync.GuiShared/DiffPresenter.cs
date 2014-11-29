using System;
using PiggySync.FileMerger;
using System.IO;

namespace PiggySync.GuiShared
{
	public class DiffPresenter
	{
		IDiffView view;
		string fileAPath;
		string fileBPath;
		string resultPath;

		public DiffPresenter (IDiffView view, string fileAPath, string fileBPath, string resultPath)
		{
			this.view = view;
			this.fileAPath = fileAPath;
			this.fileBPath = fileBPath;
			this.resultPath = resultPath;
			CreateDiff ();
		}

		public void CreateDiff ()
		{
			var m = new FileMerger.FileMerger (fileAPath, fileBPath, resultPath);
			m.MergeFiles ();
			ReloadView ();
			//m.
		}

		public void ReloadView ()
		{
			view.SourceFile = File.ReadAllText (fileAPath);
			view.ResultFile = File.ReadAllText (resultPath);
			view.ChangedFile = File.ReadAllText (fileBPath);
		}

		public void Save ()
		{
			File.WriteAllText (resultPath, view.ResultFile);
		}
	}
}


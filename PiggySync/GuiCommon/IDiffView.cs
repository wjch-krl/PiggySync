using System;

namespace PiggySync.GuiCommon
{
	public interface IDiffView
	{
		string SourceFile { set; }
		string ChangedFile { set; }
		string ResultFile { get; set; }
	}
}


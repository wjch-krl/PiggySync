using System;
using MonoMac.AppKit;
using System.Collections;
using PiggySync.Domain;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;

namespace PiggySync.MonoMacGui
{
	public class TextFilesDataSource : NSTableViewDataSource
	{
		public IEnumerable<TextFile>  Files { get; set; }
		public TextFilesDataSource (IEnumerable<TextFile> textFiles)
		{
			this.Files = textFiles;
		}

		public override int GetRowCount(NSTableView table)
		{
			return Files.Count ();
		}

		// what to draw in the table
		public override NSObject GetObjectValue (NSTableView table, NSTableColumn col, int row)
		{
			// assume you've setup your tableview in IB with two columns, "Index" and "Value"

			string text = string.Empty;

			if (col.HeaderCell.Title == "Extension") {
				text = Files.ElementAtOrDefault (row).Extension;
			} else {
				text = Files.ElementAtOrDefault (row).TemplatePath;
			}

			return new NSString (text);
		} 
	}
}


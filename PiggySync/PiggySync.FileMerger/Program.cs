using System;
using System.IO;

namespace PiggySync.FileMerger
{
	public class Program
	{
		public static void main(string[] args)
		{
			if (args.Length != 5)
			{
				Console.WriteLine ("USAGE: {0} filAPath, fileBPath, fileCPath, resultPath", args [0]);
			}
			else
			{
				if (new FileMerger (args [1], args [2], args [3], args [4]).MergeFiles ())
				{
					Console.WriteLine ("Succesfull merge");
				}
				else
				{
					Console.WriteLine ("Merge failed.");
				}
			}
		}
	}
}


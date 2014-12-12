using System;

namespace PiggySync.FileMerger
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("USAGE: filAPath, fileBPath, resultPath");
            }
            else
            {
                if (new FileMerger(args[0], args[1], args[2]).MergeFiles())
                {
                    Console.WriteLine("Succesfull merge");
                }
                else
                {
                    Console.WriteLine("Merge failed.");
                }
            }
        }
    }
}


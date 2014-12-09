using System;

namespace PiggySync.FileMerger
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 5)
            {
                Console.WriteLine("USAGE: {0} filAPath, fileBPath, resultPath", args[0]);
            }
            else
            {
                if (new FileMerger(args[1], args[2], args[3]).MergeTextFiles())
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


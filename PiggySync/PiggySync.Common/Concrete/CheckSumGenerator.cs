using System.IO;
using PiggySync.Common.Abstract;

namespace PiggySync.Common.Concrete
{
    public static class CheckSumGenerator
    {
        private static ICheckSumGenerator generator;

        static CheckSumGenerator()
        {
            generator = new Md5Generator();
        }

        public static int ChecksumSize
        {
            get { return generator.ChecksumSize; }
        }

        public static void ChangeGenerator(ICheckSumGenerator generator)
        {
            CheckSumGenerator.generator = generator;
        }

        public static byte[] ComputeChecksum(string file)
        {
            return generator.ComputeChecksum(file);
        }
    }
}
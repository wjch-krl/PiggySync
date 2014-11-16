using NUnit.Framework;
using PiggySync.FileMerger;

namespace DuckSync.Tests
{
    [TestFixture]
    public class FileMergerTests
    {
        [Test]
        public void TestMerge1()
        {
            var merger = new FileMerger("TestFiles/MainWindow.txt", "TestFiles/MainWindow-kopia.txt",
                "TestFiles/Result.txt");
            merger.MergeFiles();
        }
    }
}
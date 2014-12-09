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
			var merger = new FileMerger("TestFiles/!LocalV~MainWindow.txt", "TestFiles/!RemoteV~MainWindow.txt", "OldMainWindow.txt",
                "TestFiles/Result.txt");
            merger.MergeFiles();
        }
    }
}
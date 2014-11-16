using System;
using NUnit.Framework;
using PiggySync.DesktopFileWather;

namespace DuckSync.Tests
{
    /// <summary>
    ///     This is a test class for FileWatcherTest and is intended
    ///     to contain all FileWatcherTest Unit Tests
    /// </summary>
    [TestFixture]
    public class FileWatcherTest
    {
        /// <summary>
        ///     A test for RefreshMonitoredDirectory
        /// </summary>
        [Test]
        [ExpectedException(typeof (NullReferenceException))]
        public void RefreshMonitoredDirectoryTest()
        {
            string dir = "cos";
            FileWatcher.RefreshMonitoredDirectory(dir);
        }
    }
}
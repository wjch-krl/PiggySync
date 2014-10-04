using PiggySyncWin.WinUI.Infrastructure;
using System;
using NUnit.Framework;

namespace PiggySyncWin.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for FileWatcherTest and is intended
    ///to contain all FileWatcherTest Unit Tests
    ///</summary>
    [TestFixture()]
    public class FileWatcherTest
    {


        /// <summary>
        ///A test for RefreshMonitoredDirectory
        ///</summary>
        [Test()]
        [ExpectedException(typeof(NullReferenceException))]
        public void RefreshMonitoredDirectoryTest()
        {
            string dir = "cos"; // TODO: Initialize to an appropriate value
            FileWatcher.RefreshMonitoredDirectory(dir);
        }
    }
}

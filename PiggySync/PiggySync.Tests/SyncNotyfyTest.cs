using PiggySyncWin.WinUI.Models;
using NUnit.Framework;
using System;

namespace PiggySyncWin.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for SyncNotyfyTest and is intended
    ///to contain all SyncNotyfyTest Unit Tests
    ///</summary>
    [TestFixture()]
    public class SyncNotyfyTest
    {

        /// <summary>
        ///A test for SyncNotyfy Constructor
        ///</summary>
        [Test()]
        public void SyncNotyfyConstructorTest()
        {
            UInt32 seqNumber = 170; // TODO: Initialize to an appropriate value
            SyncNotyfy target = new SyncNotyfy(seqNumber);
            Assert.IsNotNull(target);
        }
    }
}

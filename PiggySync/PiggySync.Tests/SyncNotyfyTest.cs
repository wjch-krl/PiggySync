using System;
using NUnit.Framework;
using PiggySync.Model.Concrete;

namespace DuckSync.Tests
{
    /// <summary>
    ///     This is a test class for SyncNotyfyTest and is intended
    ///     to contain all SyncNotyfyTest Unit Tests
    /// </summary>
    [TestFixture]
    public class SyncNotyfyTest
    {
        /// <summary>
        ///     A test for SyncNotyfy Constructor
        /// </summary>
        [Test]
        public void SyncNotyfyConstructorTest()
        {
            UInt32 seqNumber = 170;
            var target = new SyncNotyfy(seqNumber);
            Assert.IsNotNull(target);
        }
    }
}
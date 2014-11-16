using System;
using NUnit.Framework;
using PiggySync.Common.Concrete;

namespace DuckSync.Tests
{
    /// <summary>
    ///     This is a test class for CRC32Test and is intended
    ///     to contain all CRC32Test Unit Tests
    /// </summary>
    [TestFixture]
    public class CRC32Test
    {
        /// <summary>
        ///     A test for CRC32 Constructor
        /// </summary>
        [Test]
        public void CRC32ConstructorTest()
        {
            var target = new CRC32();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///     A test for ComputeChecksum
        /// </summary>
        [Test]
        public void ComputeChecksumTest()
        {
            byte[] bytes =
            {
                1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0,
                1
            };
            UInt32 expected = 310811753;
            UInt32 actual;
            actual = CRC32.ComputeChecksum(bytes);
            Assert.AreEqual(expected, actual);
        }
    }
}
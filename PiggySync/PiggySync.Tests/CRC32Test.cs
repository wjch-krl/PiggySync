using System;
using System.ComponentModel;
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
            var expectedSum = BitConverter.GetBytes(expected);
            var crc = new CRC32();
            var actual = crc.ComputeChecksum(bytes);
            CollectionAssert.AreEqual(expectedSum, actual);
        }
    }
}
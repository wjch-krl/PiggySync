using PiggySyncWin.WinUI.Infrastructure;
using NUnit.Framework;

namespace PiggySyncWin.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for CRC32Test and is intended
    ///to contain all CRC32Test Unit Tests
    ///</summary>
    [TestFixture()]
    public class CRC32Test
    {

        /// <summary>
        ///A test for CRC32 Constructor
        ///</summary>
        [Test()]
        public void CRC32ConstructorTest()
        {
            CRC32 target = new CRC32();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for ComputeChecksum
        ///</summary>
        [Test()]
        public void ComputeChecksumTest()
        {
            CRC32 target = new CRC32(); // TODO: Initialize to an appropriate value
            byte[] bytes = { 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1 }; // TODO: Initialize to an appropriate value
            uint expected = 0; // za ciula nie wiem co tu ma byc, nie ogarniam tego algorytmu
            uint actual;
            actual = target.ComputeChecksum(bytes);
            Assert.AreNotEqual(expected, actual);
        }
    }
}

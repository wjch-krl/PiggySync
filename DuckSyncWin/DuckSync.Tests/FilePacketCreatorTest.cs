using PiggySyncWin.WinUI.Infrastructure;
using NUnit.Framework;

namespace PiggySyncWin.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for FilePacketCreatorTest and is intended
    ///to contain all FilePacketCreatorTest Unit Tests
    ///</summary>
    [TestFixture()]
    public class FilePacketCreatorTest
    {
        /// <summary>
        ///A test for CreatePacket
        ///</summary>
        [Test()]
        public void CreatePacketTest() //nie ogarniam :(
        {
            string filePath = "siema.txt"; // TODO: Initialize to an appropriate value
            int position = 1; // TODO: Initialize to an appropriate value
            int size = 1; // TODO: Initialize to an appropriate value
            byte[] expected = {1}; // TODO: Initialize to an appropriate value
            byte[] actual;
            actual = FilePacketCreator.CreatePacket(filePath, position, size);
            Assert.AreEqual(expected, actual);
        }
    }
}

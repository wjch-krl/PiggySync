using NUnit.Framework;

namespace DuckSync.Tests
{
    /// <summary>
    ///     This is a test class for FilePacketCreatorTest and is intended
    ///     to contain all FilePacketCreatorTest Unit Tests
    /// </summary>
    [TestFixture]
    public class FilePacketCreatorTest
    {
        /// <summary>
        ///     A test for CreatePacket
        /// </summary>
        [Test]
        public void CreatePacketTest() //nie ogarniam :(
        {
            string filePath = "siema.txt";
            int position = 1;
            int size = 1;
            byte[] expected = {1};
            byte[] actual;
            actual = FilePacketCreator.CreatePacket(filePath, position, size);
            Assert.AreEqual(expected, actual);
        }
    }
}
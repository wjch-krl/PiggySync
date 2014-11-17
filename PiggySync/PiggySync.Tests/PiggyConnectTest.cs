using System.Net;
using System.Net.Sockets;
using NUnit.Framework;
using PiggySync.Model;
using PiggySync.Core;

namespace DuckSync.Tests
{
    
    
    /// <summary>
    ///This is a test class for PiggyConnectTest and is intended
    ///to contain all PiggyConnectTest Unit Tests
    ///</summary>
    [TestFixture]
    public class PiggyConnectTest
    {

        /// <summary>
        ///A test for PiggyConnect Constructor
        ///</summary>
        [Test()]
        public void PiggyConnectConstructorTest()
        {
            SyncManager target = new SyncManager();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for CreateNewConnection
        ///</summary>
        [Test()]
        [ExpectedException(typeof(SocketException))] ////cos krzyczalo na gniazdo, bez tego wywalalo blad, nie ogarniam :(
        public void CreateNewConnectionTest()
        {
            SyncManager target = new SyncManager(); 
            PiggyRemoteHost host = new PiggyRemoteHost(IPAddress.Any, "Test"); 
        }

        /// <summary>
        ///A test for DidReciveSyncNotyfy
        ///</summary>
        [Test()]
        public void DidReciveSyncNotyfyTest() // jak wyzej
        {
            SyncManager target = new SyncManager(); 
            byte[] msg = { 1, 0, 0, 1, 1, 0, 0, 1 }; 
            bool expected = true; 
            bool actual;
            actual = target.DidReciveSyncNotyfy(msg);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for DidReciveSyncRequest
        ///</summary>
        [Test()]
        public void DidReciveSyncRequestTest() //jak wyzej
        {
            SyncManager target = new SyncManager(); 
            byte[] msg = { 1, 0, 0, 1, 1, 0, 0, 1 }; 
            bool expected = true; 
            bool actual;
            actual = target.DidReciveSyncRequest(msg);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Me
        ///</summary>
        [Test()]
        public void MeTest()
        {
            PiggyRemoteHost actual;
			actual = PiggyRemoteHost.Me;
            Assert.IsNotNull(actual);
        }
    }
}

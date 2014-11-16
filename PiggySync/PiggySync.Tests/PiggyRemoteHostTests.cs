using System;
using System.Net;
using NUnit.Framework;
using PiggySync.Model;

namespace DuckSync.Tests
{
	[TestFixture]
	public class PiggyRemoteHostTests
	{
		[Test]
		public void ObjectCreationWithGoodData()
		{
			PiggyRemoteHost host = new PiggyRemoteHost(IPAddress.Any,"Test");
			Assert.AreEqual(host.Name, "Test");
		}

		[Test]
		[ExpectedException(typeof(FormatException))]
		public void ObjectCreationWithBadData()
		{
			PiggyRemoteHost host = new PiggyRemoteHost(IPAddress.Parse("q.1.1.1"), "Test");
			host = new PiggyRemoteHost(IPAddress.Parse("192.258.1.2"), "Test");
		}

        [Test]
        [ExpectedException(typeof(FormatException))]
        public void ObjectCreationWithBadData2()
        {
            PiggyRemoteHost host = new PiggyRemoteHost(IPAddress.Parse("192.258.1.2"), null);
        }

		[Test]
		public void EqualsTest()
		{
			PiggyRemoteHost host2 = new PiggyRemoteHost(IPAddress.Parse("127.0.0.1"), "Test");
			PiggyRemoteHost host1 = new PiggyRemoteHost(IPAddress.Loopback, "Test");
			Assert.AreEqual(host2, host1);
		}

	}
}


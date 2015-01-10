using System;
using PiggySync.Common;
using System.Net;

namespace PiggySync.StandardTypeResolver
{
	public class StandardIpEndPoint : IIPEndPoint
	{
		public IPEndPoint IPEndPoint { get; set; }
		public StandardIpEndPoint(IIPAddress iPAddress, int port){
			IPEndPoint = new IPEndPoint ((iPAddress as StandardIPAddress).IpAdress, port);
		}
	}
}


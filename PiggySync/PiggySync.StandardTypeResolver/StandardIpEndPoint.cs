using System;
using PiggySync.Common;
using System.Net;

namespace PiggySync.StandardTypeResolver
{
	public class StandardIpEndPoint : IIPEndPoint
	{
		private IPEndPoint ip;
		public IPEndPoint IPEndPoint { get { return ip; } }
		public StandardIpEndPoint(IIPAddress iPAddress, int port){
			ip = new IPEndPoint ((iPAddress as StandardIPAddress).IpAdress, port);
		}
	}
}


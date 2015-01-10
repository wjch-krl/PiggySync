using System;
using PiggySync.Common;
using System.Net;

namespace PiggySync.StandardTypeResolver
{
	public class IpHelper : IIPHelper
	{
		public IIPAddress Create (byte[] adrBytes)
		{
			return new StandardIPAddress (new IPAddress (adrBytes));
		}
			
		public IIPAddress Any//TODO
		{
			get
			{
				return new StandardIPAddress (IPAddress.Any);
			}
			set
			{
				throw new NotImplementedException ();
			}
		}

		public IIPAddress Broadcast
		{
			get
			{
				throw new NotImplementedException ();
			}
			set
			{
				throw new NotImplementedException ();
			}
		}

		public IIPAddress LocalIp
		{
			get
			{
				throw new NotImplementedException ();
			}
			set
			{
				throw new NotImplementedException ();
			}
		}

	}
}


using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using PiggySync.Model;

namespace PiggySync.Core
{
	public interface IHostWather
	{
		void RefreshHostsList (IEnumerable<PiggyRemoteHost> hosts);
	}


}

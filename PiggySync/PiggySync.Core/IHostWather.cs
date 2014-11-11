using PiggySyncWin.WinUI.Infrastructure;
using PiggySyncWin.WinUI.Infrastructure.Concrete;
using PiggySyncWin.WinUI.Models;
using PiggySyncWin.WinUI.Models.Concrete;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using PiggySyncWin.Domain;

namespace PiggySyncWin.Core
{
	public interface IHostWather
	{
		void RefreshHostsList (IEnumerable<PiggyRemoteHost> hosts);
	}


}

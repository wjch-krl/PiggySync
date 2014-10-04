using PiggySyncWin.Domain.Concrete;
using PiggySyncWin.WinUI.Infrastructure;
using PiggySyncWin.WinUI.Infrastructure.Abstract;
using PiggySyncWin.WinUI.Infrastructure.Concrete;
using PiggySyncWin.WinUI.Models;
using PiggySyncWin.WinUI.Models.Concrete;
using PiggySyncWin.WinUI.Sync;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace PiggySyncWin.WinUI
{
	public class TcpSocket
	{
		public TcpSocket (IPEndPoint iPEndPoint)
		{
			throw new NotImplementedException ();
		}

		public void Connect (IPEndPoint host)
		{
			throw new NotImplementedException ();
		}

		public void Close ()
		{
			throw new NotImplementedException ();
		}

		public NetworkStream GetStream ()
		{
			throw new NotImplementedException ();
		}
	}


}

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
	class UdpSocket
	{
		public bool EnableBroadcast {
			get;
			set;
		}

		public UdpSocket (int i)
		{
			throw new NotImplementedException ();
		}

		public UdpSocket ()
		{
			throw new NotImplementedException ();
		}

		public byte[] Receive (ref IPEndPoint source)
		{
			throw new NotImplementedException ();
		}
	}


}

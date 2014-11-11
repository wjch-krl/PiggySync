using PiggySync.Model.Concrete;
using PiggySyncWin.Domain;
using PiggySyncWin.Models;
using PiggySyncWin.WinUI.Infrastructure;
using PiggySyncWin.WinUI.Models;
using PiggySyncWin.WinUI.Models.Concrete;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices;

namespace PiggySyncWin.Models
{
	public class FileChangesCollection
	{
		public List<FileRequestPacket> FileRequests{ get ; set; }

		public List<FileDeletePacket> DeletedFiles{ get; set; }

		public FileChangesCollection ()
		{
			DeletedFiles = new List<FileDeletePacket> ();
			FileRequests = new List<FileRequestPacket> ();
		}
	}

}

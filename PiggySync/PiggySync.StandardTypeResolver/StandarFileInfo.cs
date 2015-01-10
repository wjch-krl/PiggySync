using System;
using PiggySync.Common;
using System.Net.Sockets;
using System.IO;

namespace PiggySync.StandardTypeResolver
{
	public class StandarFileInfo:IFileInfo
	{
		public string Name
		{
			get
			{
				return FileInfo.Name;
			}
		}

		public long Length
		{
			get
			{
				return FileInfo.Length;
			}
		}

		public DateTime LastWriteTimeUtc
		{
			get
			{
				return FileInfo.LastWriteTimeUtc;
			}
		}

		internal FileInfo FileInfo { get; private set; }

		public StandarFileInfo (string path)
		{
			FileInfo = new FileInfo (path);
		}
	}

}


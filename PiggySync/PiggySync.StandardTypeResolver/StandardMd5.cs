using System;
using PiggySync.Common;
using System.Net.Sockets;
using System.Security.Cryptography;

namespace PiggySync.StandardTypeResolver
{
	public class StandardMd5 : Imd5
	{
		public MD5 Md5 { get; private set; }
		public StandardMd5(MD5 md5)
		{
			Md5 = md5;
		}

		public byte[] ComputeHash (byte[] bytes)
		{
			return Md5.ComputeHash (bytes);
		}

		public byte[] ComputeHash (System.IO.Stream stream)
		{
			return Md5.ComputeHash (stream);
		}

		public void Dispose ()
		{
			Md5.Dispose ();
		}
	}

}


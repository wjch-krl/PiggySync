using System.IO;
using System.Security;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto.Digests;
using PiggySync.Common.Abstract;
using System;

namespace PiggySync.Common
{
	public interface Imd5:IDisposable
	{
		byte[] ComputeHash (byte[] bytes);

		byte[] ComputeHash (Stream stream);
	}
}
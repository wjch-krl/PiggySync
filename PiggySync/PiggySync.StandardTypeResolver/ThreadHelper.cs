using System;
using PiggySync.Common;
using System.Threading;

namespace PiggySync.StandardTypeResolver
{
	public class ThreadHelper : IThreadHelper
	{
		public void Sleep (int i)
		{
			Thread.Sleep (i);
		}

	}
}


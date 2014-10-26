using System;

namespace PiggySync.Model
{
	public class PiggyFileException : Exception
	{
		public PiggyFileException (string message) :
			base (message)
		{
		}

		public PiggyFileException (string message, Exception innerException) :
			base (message, innerException)
		{
		}
	}
}


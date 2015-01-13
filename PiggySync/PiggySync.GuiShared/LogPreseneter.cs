using System;
using System.IO;
using PiggySync.LogManager;

namespace PiggySync.GuiShared
{
	public class LogPreseneter
	{
		ILogView view;

		public LogPreseneter (ILogView view)
		{
			this.view = view;
			view.LogLines = File.ReadAllLines (LogManager.LogManager.LoggPath);
		}
	}
}


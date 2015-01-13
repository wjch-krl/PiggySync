using System;
using PiggySync.Domain.Concrete;
using System.IO;

namespace PiggySync.LogManager
{
	public class LogManager
	{
		public static string LoggPath { get; private set; }

		static LogManager()
		{
			LoggPath = Path.Combine ( XmlSettingsRepository.SettingsPath, "error.log");
		}

		public LogManager ()
		{
		}
	}
}


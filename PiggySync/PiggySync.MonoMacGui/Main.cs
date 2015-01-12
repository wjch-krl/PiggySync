using System;
using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using PiggySync.Common;
using PiggySync.StandardTypeResolver;
using PiggySync.Model;
using SQLite.Net.Platform.Generic;

namespace PiggySync.MacApp
{
	class MainClass
	{
		static void Main (string[] args)
		{
			TypeResolver.Factory = new Resolver ();
			DatabaseManager.Init (new SQLitePlatformGeneric());
			NSApplication.Init ();
			NSApplication.Main (args);
		}
	}
}


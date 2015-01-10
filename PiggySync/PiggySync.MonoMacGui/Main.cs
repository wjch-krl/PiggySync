using System;
using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using PiggySync.Common;
using PiggySync.StandardTypeResolver;

namespace PiggySync.MacApp
{
	class MainClass
	{
		static void Main (string[] args)
		{
			TypeResolver.Factory = new Resolver ();
			NSApplication.Init ();
			NSApplication.Main (args);
		}
	}
}


using System;
using System.Windows.Forms;
using PiggySync.Common;
using PiggySync.Model;
using PiggySync.StandardTypeResolver;
using SQLite.Net.Platform.Win32;

namespace PiggySync.WinApp
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
			var resolver = new Resolver ();
            resolver.FileWather = new DesktopFileWather.FileWatcher();
            TypeResolver.Factory = resolver;
            DatabaseManager.Init(new SQLitePlatformWin32());

			Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
using System;
using System.Windows.Forms;
using PiggySync.Common;
using PiggySync.StandardTypeResolver;

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
			TypeResolver.Factory = new Resolver ();
			Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
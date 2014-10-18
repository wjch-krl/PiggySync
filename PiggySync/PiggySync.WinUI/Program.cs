using PiggySyncWin.Domain;
using PiggySyncWin.WinUI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PiggySyncWin.WinUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
           // ConsoleManager.Show(); //Shows the console window
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var x = new MainWindow();

			do {
				System.Threading.Thread.Sleep(1000);
			} while (true);

        }
    }

}
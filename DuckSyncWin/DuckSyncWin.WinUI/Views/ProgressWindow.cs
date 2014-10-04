using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PiggySyncWin.WinUI.Views
{
    public partial class ProgressWindow : Form
    {
        MainWindow mainWindow;
        public ProgressWindow(MainWindow okno)
        {
            InitializeComponent();
            mainWindow = okno;
            progressBar1.Value = 0;
            this.CenterToScreen();
        }

        private void stopButtonClick(object sender, EventArgs e)
        {
            mainWindow.synchButtonFunc();
            this.Hide();
            this.setProgressBar(0);
        }

        public void setProgressBar(int i)
        {
            progressBar1.Value = i;
        }

        public int getProgressBar()
        {
            return progressBar1.Value;
        }

        // Poprawka na brak "X" button
        private const int WS_SYSMENU = 0x80000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style &= ~WS_SYSMENU;
                return cp;
            }
        }

    }
}

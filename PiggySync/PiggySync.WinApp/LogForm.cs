using PiggySync.GuiShared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PiggySync.WinApp
{
    public partial class LogForm : Form, ILogView
    {
        public LogForm()
        {
            InitializeComponent();
        }

        public string[] LogLines
        {
            set { logTextBox.Lines = value; }
        }
    }
}

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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            new SettingsForm().ShowDialog();
        }

        private void hostsButton_Click(object sender, EventArgs e)
        {
            new HostsViewForm().ShowDialog();
        }
    }
}

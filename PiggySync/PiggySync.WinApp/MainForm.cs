using System;
using System.Windows.Forms;
using PiggySync.Common;
using PiggySync.GuiShared;
using PiggySync.WinApp.Properties;

namespace PiggySync.WinApp
{
    public partial class MainForm : Form, IMainView
    {
        private MainPresenter presenter;

        public MainForm()
        {
            InitializeComponent();
            presenter = new MainPresenter(this);
        }

        public double ProgresLevel
        {
            get { return progressBar1.Value/100.0; }
            set { this.GuiInvokeMethod(() => { progressBar1.Value = (int)Math.Round(value * 100); }); }
        }

        public SyncStatus SyncStatus
        {
            set { this.GuiInvokeMethod(() => { statusTextBox.Lines = new[] { Resources.SyncStatus, value.ToString() }; }); }
        }

        public bool ProgresEnabled
        {
            set { this.GuiInvokeMethod(() => { progressBar1.Visible = value; }); }
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            new SettingsForm().ShowDialog();
        }

        private void hostsButton_Click(object sender, EventArgs e)
        {
            new HostsViewForm().ShowDialog();
        }

        private void s_Click(object sender, EventArgs e)
        {
            new LogForm().ShowDialog();
        }
    }
}
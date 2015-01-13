using System;
using System.Windows.Forms;
using PiggySync.GuiShared;

namespace PiggySync.WinApp
{
    public partial class MainForm : Form, IMainView
    {
        private MainPresenter presenter;

        public MainForm()
        {
            InitializeComponent();
      //      presenter = new MainPresenter(this);
        }

        public double ProgresLevel
        {
			get { return progressBar1.Value / 100.0; }
			set { progressBar1.Value = (int)Math.Round (value * 100); }
        }

        public SyncStatus SyncStatus
        {
			set { statusTextBox.Text = string.Format("Sync Status:\n{0}", value.ToString ()); }
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
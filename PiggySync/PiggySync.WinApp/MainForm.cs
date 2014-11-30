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
            presenter = new MainPresenter(this);
        }

        public double ProgresLevel
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public SyncStatus SyncStatus
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
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
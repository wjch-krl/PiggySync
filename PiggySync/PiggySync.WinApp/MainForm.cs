using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PiggySync.GuiShared;

namespace PiggySync.WinApp
{
	public partial class MainForm : Form , IMainView
    {
        public MainForm()
        {
            InitializeComponent();
			presenter = new MainPresenter (this);
        }

		MainPresenter presenter;

        private void settingsButton_Click(object sender, EventArgs e)
        {
            new SettingsForm().ShowDialog();
        }

        private void hostsButton_Click(object sender, EventArgs e)
        {
            new HostsViewForm().ShowDialog();
        }
			
		public double ProgresLevel
		{
			get
			{
				throw new NotImplementedException ();
			}
			set
			{
				throw new NotImplementedException ();
			}
		}

		public SyncStatus SyncStatus
		{
			get
			{
				throw new NotImplementedException ();
			}
			set
			{
				throw new NotImplementedException ();
			}
		}
    }
}

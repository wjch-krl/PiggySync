using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PiggySync.GuiShared;
using PiggySync.Model;

namespace PiggySync.WinApp
{
    public partial class HostsViewForm : Form , IHostView
    {
        private HostsPresenter presenter;
        public HostsViewForm()
        {
            InitializeComponent();
            this.presenter = new HostsPresenter(this);
        }

        public IEnumerable<PiggyRemoteHost> Hosts
        {
            set { throw new NotImplementedException(); }
        }

        private void HostsViewForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            presenter.UnSucribe();
        }
    }
}
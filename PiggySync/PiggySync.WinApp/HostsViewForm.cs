using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PiggySync.GuiShared;
using PiggySync.Model;

namespace PiggySync.WinApp
{
    public partial class HostsViewForm : Form, IHostView
    {
        private readonly HostsPresenter presenter;

        public HostsViewForm()
        {
            InitializeComponent();
            presenter = new HostsPresenter(this);
        }

        public IEnumerable<PiggyRemoteHost> Hosts
        {
            set
            {
                hostsListView.Items.AddRange(value.
                    Select(x => new ListViewItem(new[]
                    {
                        x.GetShortName(), x.Ip.ToString(),
                        x is PiggyRemoteHostHistoryEntry
                            ? ((PiggyRemoteHostHistoryEntry) x).LastSync.ToShortDateString()
                            : string.Empty
                    })).ToArray());
                hostsListView.Refresh();
            }
        }

        private void HostsViewForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            presenter.UnSucribe();
        }
    }
}
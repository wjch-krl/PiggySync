using System;
using System.Windows.Forms;
using PiggySync.GuiShared;

namespace PiggySync.WinApp
{
    public partial class SettingsForm : Form , ISettingsView
    {
        private SettingsPresenter presenter;

        public SettingsForm()
        {
            InitializeComponent();
            this.presenter = new SettingsPresenter(this);
        }

        public string SyncRootPath
        {
            get { return pathTextBox.Text; }
            set { pathTextBox.Text = value; }
        }

        public bool AutoSync
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool UseTcp
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool UseEncryption
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string ComputerName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public System.Collections.Generic.IEnumerable<Domain.TextFile> TextFiles
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
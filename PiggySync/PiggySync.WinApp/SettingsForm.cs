using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PiggySync.Domain;
using PiggySync.GuiShared;

namespace PiggySync.WinApp
{
    public partial class SettingsForm : Form, ISettingsView
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


        public bool UseTcp
        {
            get { return useTcp.Checked; }
            set { useTcp.Checked = value; }
        }

        public bool UseEncryption
        {
            get { return useEncryption.Checked; }
            set { useEncryption.Checked = value; }
        }

        public string ComputerName
        {
            get { return computerNameTextBox.Text; }
            set { computerNameTextBox.Text = value; }
        }

        public System.Collections.Generic.IEnumerable<Domain.TextFile> TextFiles
        {
            get { return textFiles.Lines.Select(x => new TextFile {Extension = x}); }
            set { textFiles.Lines = value.Select(x => x.Extension).ToArray(); }
        }


        public IEnumerable<string> BannedFiles
        {
            get { return bannedFiles.Lines; }
            set { bannedFiles.Lines = value.ToArray(); }
        }

        public int KeepDeletedInfo
        {
            get { return Convert.ToInt32(deletedUpDown.Value); }
            set { deletedUpDown.Value = value; }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (presenter.SaveSettings())
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Error in configuration");
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void selectPath_Click(object sender, EventArgs e)
        {
            if (folderSelector.ShowDialog() == DialogResult.OK)
            {
                SyncRootPath = folderSelector.SelectedPath;
            }
        }
    }
}
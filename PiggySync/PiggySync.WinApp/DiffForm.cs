using System;
using System.Windows.Forms;
using PiggySync.GuiShared;

namespace PiggySync.WinApp
{
    public partial class DiffForm : Form, IDiffView
    {
        private DiffPresenter presenter;
        public DiffForm(string fileAPath, string fileBPath, string resultFilePath)
        {
            InitializeComponent();
            this.presenter = new DiffPresenter(this, fileAPath, fileBPath, resultFilePath);
        }

        public string SourceFile
        {
            set { localFileTextBox.Text = value; }
        }

        public string ChangedFile
        {
            set { remoteFileTextBox.Text = value; }
        }

        public string ResultFile
        {
            get { return resultFileTextBox.Text; }
            set { resultFileTextBox.Text = value; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}
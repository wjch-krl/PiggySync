using PiggySyncWin.Domain;
using PiggySyncWin.WinUI.Presenters;
using PiggySyncWin.WinUI.Views.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PiggySyncWin.WinUI.Views
{
    public partial class SettingsView : Form, ISettingsView
    {
        public SettingsView()
        {
            InitializeComponent();
            new SettingsPresenter(this);
        }

        private void SettingsView_Load(object sender, EventArgs e)
        {
            if (LoadSettings != null)
            {
                LoadSettings(sender, e);
            }
        }

        public event EventHandler LoadSettings;

        public string P1
        {
            set { label1.Text = value; }
        }

        public string P2
        {
            set { label2.Text = value; }
        }
    }
}

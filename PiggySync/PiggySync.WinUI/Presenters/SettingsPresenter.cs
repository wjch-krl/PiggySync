using PiggySync.Domain.Concrete;
using PiggySyncWin.Domain;
using PiggySyncWin.WinUI.Views.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggySyncWin.WinUI.Presenters
{
    public class SettingsPresenter
    {
        private XmlSettingsRepository model;
        private ISettingsView view;

        public SettingsPresenter(ISettingsView view)
            : this(view, XmlSettingsRepository.Instance)
        {

        }

        public SettingsPresenter(ISettingsView view, XmlSettingsRepository model)
        {
            this.model = model;
            this.view = view;
            SettingsViewEvents();
        }

        private void SettingsViewEvents()
        {
            view.LoadSettings += view_LoadSettings;
        }

        private void view_LoadSettings(object sender, EventArgs e)
        {
			view.P1 = model.Settings.ComputerName;
          //  view.P2 = model.Settings.Prop2;
        }
    }
}

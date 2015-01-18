using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PiggySync.WinApp
{
    static class FormExtensions
    {
        public static void GuiInvokeMethod(this Form form,Action @delegate)
        {
            // Check if we need to invoke as GUI thread
            if (form.InvokeRequired)
            {
                form.Invoke(@delegate);
                return;
            }
            // Execute
            @delegate.Invoke();
        }
    }
}

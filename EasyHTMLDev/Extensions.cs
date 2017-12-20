using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyHTMLDev
{
    public static class ExtensionForms
    {
        public static void RegisterControls(this System.Windows.Forms.Control control, ref int id, params object[] pars)
        {
            id = Localization.Strings.RelativeWindow(control, pars);
        }

        public static void UnregisterControls(this System.Windows.Forms.Control control, ref int id)
        {
            Localization.Strings.FreeWindow(ref id);
        }
    }
}

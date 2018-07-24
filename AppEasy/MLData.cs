using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppEasy
{
    public class MLData : UXFramework.IMLData
    {

        /// <summary>
        /// Delegate to export
        /// </summary>
        /// <returns>ux object</returns>
        public event EventHandler ExportEvent;
        /// <summary>
        /// Export ux from data
        /// </summary>
        /// <returns>ux instance</returns>
        public UXFramework.IUXObject Export()
        {
            UXFramework.UXWindow window = new UXFramework.UXWindow();
            window.Add("C'est un test.");
            UXFramework.UXButton button = new UXFramework.UXButton();
            window.Add(button);
            button.SetUpdate(() => {
                window.Add("Le texte a été modifié.");
                window.Navigate(window.GetWebBrowser());
            });
            return window;
        }
    }
}

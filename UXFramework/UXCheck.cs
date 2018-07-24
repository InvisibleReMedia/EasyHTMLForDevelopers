using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UXFramework
{
    public class UXCheck : UXControl
    {

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UXCheck()
        {
            this.Add("<input type='checkbox' value='OK' name='chk' id='chk1'/>");
        }

        #endregion

        #region Overriden Methods

        /// <summary>
        /// Connect for interoperability C#/Web
        /// </summary>
        public override void Connect()
        {
            base.Connect();
            HtmlElement e = this.GetWebBrowser().Document.GetElementById("chk1");
            if (e != null)
            {
                e.Click += UXCheck_Click;
            }

        }

        /// <summary>
        /// Delegate to click
        /// </summary>
        /// <param name="sender">html element</param>
        /// <param name="e">args</param>
        private void UXCheck_Click(object sender, HtmlElementEventArgs e)
        {
            HtmlElement h = (HtmlElement)sender;
            if (h.GetAttribute("checked") == "true")
                h.SetAttribute("checked", "false");
            else
                h.SetAttribute("checked", "true");
        }

        #endregion

    }
}

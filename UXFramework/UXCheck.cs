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
        }

        /// <summary>
        /// Creates elements
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="e">elements</param>
        public UXCheck(string name, IDictionary<string, dynamic> e)
            : base(name, e)
        {
        }

        #endregion

        #region Overriden Methods

        /// <summary>
        /// Connect for interoperability C#/Web
        /// </summary>
        /// <param name="web">web browser</param>
        public override void Connect(WebBrowser web)
        {
            base.Connect(web);
            HtmlElement e = this.GetWebBrowser().Document.GetElementById(this.GetProperty("Id").Value);
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
            {
                h.SetAttribute("checked", "false");
                this.Set("Checked", false);
            }
            else
            {
                h.SetAttribute("checked", "true");
                this.Set("Checked", true);
            }
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Create ckeck box
        /// </summary>
        /// <param name="data">data to show</param>
        /// <param name="ui">ui properties</param>
        public static UXCheck CreateUXCheck(Marshalling.MarshallingHash data, Marshalling.MarshallingHash ui)
        {
            UXCheck ux = new UXCheck();
            ux.Bind(data);
            ux.Bind(ui);
            return ux;
        }

        /// <summary>
        /// Create UXCheck
        /// </summary>
        /// <param name="f">function to enter data</param>
        /// <returns>marshalling</returns>
        public static UXCheck CreateUXCheck(string name, Func<IDictionary<string, dynamic>> f)
        {
            return new UXCheck(name, f());
        }

        #endregion

    }
}

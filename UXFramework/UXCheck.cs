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

        #region Fields

        /// <summary>
        /// Text
        /// </summary>
        public static readonly string textName = "text";
        /// <summary>
        /// Id
        /// </summary>
        public static readonly string idName = "id";

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UXCheck()
        {
            this.Add("<input type='checkbox' value='OK' name='chk' id='chk1'/>");
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the text content
        /// </summary>
        public string Text
        {
            get { return this.Get(textName, string.Empty); }
            set { this.Set(textName, value); }
        }

        /// <summary>
        /// Gets or sets the id object
        /// </summary>
        public string Id
        {
            get { return this.Get(idName, string.Empty); }
            set { this.Set(idName, value); }
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
            HtmlElement e = this.GetWebBrowser().Document.GetElementById(this.Id);
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

        #region Static Methods

        /// <summary>
        /// Create ckeck box
        /// </summary>
        /// <param name="data">data to show</param>
        /// <param name="ui">ui properties</param>
        public static UXCheck CreateUXCheck(Marshalling.MarshallingHash data, Marshalling.MarshallingHash ui)
        {
            UXCheck ux = new UXCheck();
            ux.Construct(data, ui);
            return ux;
        }

        #endregion

    }
}

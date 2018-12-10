using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UXFramework
{
    public class UXEditableText : UXControl
    {

        #region Fields

        private string text;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UXEditableText()
        {
            this.Add("<textarea id='txt1'></textarea>");
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
            HtmlElement e = web.Document.GetElementById("txt1");
            if (e != null)
            {
                e.LostFocus += UXEditableText_LostFocus;
            }

        }

        /// <summary>
        /// Delegate to lost focus
        /// </summary>
        /// <param name="sender">html element</param>
        /// <param name="e">args</param>
        private void UXEditableText_LostFocus(object sender, HtmlElementEventArgs e)
        {
            this.text = ((HtmlElement)sender).InnerText;
            this.UpdateOne();
        }

        #endregion

    }
}

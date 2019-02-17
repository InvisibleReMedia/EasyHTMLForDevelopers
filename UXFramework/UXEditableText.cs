using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UXFramework.BeamConnections;

namespace UXFramework
{
    public class UXEditableText : UXControl
    {

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UXEditableText()
        {
        }

        /// <summary>
        /// Creates elements
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="e">elements</param>
        public UXEditableText(string name, IDictionary<string, dynamic> e)
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
            HtmlElement e = web.Document.GetElementById(this.GetProperty("Id").Value);
            if (e != null)
            {
                e.LostFocus += UXEditableText_LostFocus;
            }

        }

        /// <summary>
        /// Disconnect for interoperability C#/Web
        /// </summary>
        public override void Disconnect(WebBrowser web)
        {
            base.Disconnect(web);
            HtmlElement e = web.Document.GetElementById(this.GetProperty("Id").Value);
            if (e != null)
            {
                e.Click -= UXEditableText_LostFocus;
            }

        }

        /// <summary>
        /// Delegate to lost focus
        /// </summary>
        /// <param name="sender">html element</param>
        /// <param name="e">args</param>
        private void UXEditableText_LostFocus(object sender, HtmlElementEventArgs e)
        {
            this.Set("Text", ((HtmlElement)sender).InnerText);
            this.UpdateOne();
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Create editable text
        /// </summary>
        /// <param name="data">data to show</param>
        /// <param name="ui">ui properties</param>
        public static UXEditableText CreateUXEditableText(Marshalling.MarshallingHash data, Marshalling.MarshallingHash ui)
        {
            UXEditableText ux = new UXEditableText();
            ux.Bind(data);
            ux.Bind(ui);
            return ux;
        }

        /// <summary>
        /// Create UXEditableText
        /// </summary>
        /// <param name="f">function to enter data</param>
        /// <returns>marshalling</returns>
        public static UXEditableText CreateUXEditableText(string name, Func<IDictionary<string, dynamic>> f)
        {
            return new UXEditableText(name, f());
        }

        #endregion

    }
}

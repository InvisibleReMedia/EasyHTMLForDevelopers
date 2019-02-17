using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UXFramework
{
    public class UXCombo : UXControl
    {

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UXCombo()
        {
        }

        /// <summary>
        /// Creates elements
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="e">elements</param>
        public UXCombo(string name, IDictionary<string, dynamic> e)
            : base(name, e)
        {
        }

        #endregion

        #region Overriden Methods

        /// <summary>
        /// Connect for interoperability C#/Web
        /// </summary>
        public override void Connect(WebBrowser web)
        {
            base.Connect(web);
            HtmlElement e = web.Document.GetElementById(this.GetProperty("Id").Value);
            if (e != null)
            {
                e.Click += UXCombo_Click;
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
                e.Click -= UXCombo_Click;
            }

        }

        /// <summary>
        /// Delegate to click
        /// </summary>
        /// <param name="sender">html element</param>
        /// <param name="e">args</param>
        private void UXCombo_Click(object sender, HtmlElementEventArgs e)
        {
            this.UpdateOne();
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UXFramework
{
    public class UXSelectableText : UXReadOnlyText
    {

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public UXSelectableText()
        {
        }

        /// <summary>
        /// Creates elements
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="e">elements</param>
        public UXSelectableText(string name, IDictionary<string, dynamic> e)
            : base(name, e)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the RefIndex
        /// </summary>
        public int RefIndex
        {
            get { return this.Get("RefIndex", 0).Value; }
        }

        #endregion
        #region Overriden Methods

        /// <summary>
        /// Connect for interoperability C#/Web
        /// </summary>
        public override void Connect(WebBrowser web)
        {
            base.Connect(web);
            HtmlElement e = web.Document.GetElementById(this.Id);
            if (e != null)
            {
                e.MouseEnter += UXSelectableText_MouseEnter;
            }
        }

        /// <summary>
        /// Disconnect for interoperability C#/Web
        /// </summary>
        public override void Disconnect(WebBrowser web)
        {
            base.Disconnect(web);
            HtmlElement e = web.Document.GetElementById(this.Id);
            if (e != null)
            {
                e.Click -= UXSelectableText_MouseEnter;
            }

        }

        /// <summary>
        /// Delegate to click
        /// </summary>
        /// <param name="sender">html element</param>
        /// <param name="e">args</param>
        private void UXSelectableText_MouseEnter(object sender, HtmlElementEventArgs e)
        {
            this.UpdateOne();
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Create selectable text
        /// </summary>
        /// <param name="data">data to show</param>
        /// <param name="ui">ui properties</param>
        public static UXSelectableText CreateUXSelectableText(Marshalling.MarshallingHash data, Marshalling.MarshallingHash ui)
        {
            UXSelectableText ux = new UXSelectableText();
            ux.Bind(data);
            ux.Bind(ui);
            return ux;
        }

        /// <summary>
        /// Create UXSelectableText
        /// </summary>
        /// <param name="f">function to enter data</param>
        /// <returns>marshalling</returns>
        public static UXSelectableText CreateUXSelectableText(string name, Func<IDictionary<string, dynamic>> f)
        {
            return new UXSelectableText(name, f());
        }

        #endregion

    }
}

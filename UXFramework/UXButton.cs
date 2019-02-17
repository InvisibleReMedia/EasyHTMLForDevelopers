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
    public class UXButton : UXControl
    {

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UXButton()
        {
        }

        /// <summary>
        /// Creates elements
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="e">elements</param>
        public UXButton(string name, IDictionary<string, dynamic> e)
            : base(name, e)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Id
        /// </summary>
        public string Id
        {
            get { return this.Get("Id", string.Empty).Value; }
        }

        /// <summary>
        /// Gets the text
        /// </summary>
        public string Text
        {
            get { return this.Get("Text", string.Empty).Value; }
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
                e.Click += UXButton_Click;
            }

        }

        /// <summary>
        /// Disconnect for interoperability C#/Web
        /// </summary>
        /// <param name="web">web browser</param>
        public override void Disconnect(WebBrowser web)
        {
            base.Disconnect(web);
            HtmlElement e = web.Document.GetElementById(this.GetProperty("Id").Value);
            if (e != null)
            {
                e.Click -= UXButton_Click;
            }
        }

        /// <summary>
        /// Delegate to click
        /// </summary>
        /// <param name="sender">html element</param>
        /// <param name="e">args</param>
        private void UXButton_Click(object sender, HtmlElementEventArgs e)
        {
            this.UpdateOne();
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Create a box
        /// </summary>
        /// <param name="data">data to show</param>
        /// <param name="ui">ui properties</param>
        public static UXButton CreateUXButton(Marshalling.MarshallingHash data, Marshalling.MarshallingHash ui)
        {
            UXButton button = new UXButton();
            button.Bind(data);
            button.Bind(ui);
            return button;
        }

        /// <summary>
        /// Create UXButton
        /// </summary>
        /// <param name="f">function to enter data</param>
        /// <returns>marshalling</returns>
        public static UXButton CreateUXButton(string name, Func<IDictionary<string, dynamic>> f)
        {
            return new UXButton(name, f());
        }


        #endregion
    }
}

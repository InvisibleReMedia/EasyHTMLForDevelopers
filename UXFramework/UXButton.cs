using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UXFramework
{
    public class UXButton : UXControl
    {

        #region Fields

        /// <summary>
        /// Text for button
        /// </summary>
        private string textButton;
        /// <summary>
        /// Id
        /// </summary>
        private string id;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UXButton()
        {
        }

        /// <summary>
        /// Constructor with init
        /// <param name="id">id</param>
        /// <param name="title">title</param>
        /// </summary>
        public UXButton(string id, string title)
        {
            this.id = id;
            this.textButton = title;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Id object
        /// </summary>
        public string Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        /// <summary>
        /// Gets or sets the button text
        /// </summary>
        public string ButtonText
        {
            get { return this.textButton; }
            set { this.textButton = value; }
        }

        #endregion

        #region Overriden Methods

        /// <summary>
        /// Connect for interoperability C#/Web
        /// </summary>
        public override void Connect()
        {
            base.Connect();
            HtmlElement e = this.GetWebBrowser().Document.GetElementById(this.id);
            if (e != null)
            {
                e.Click += UXButton_Click;
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

    }
}

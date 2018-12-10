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

        /// <summary>
        /// Colors for buttons
        /// </summary>
        private string rollBackColor;
        private string rollColor;
        private string clickBorderColor;

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

        /// <summary>
        /// Gets or sets the roll back color
        /// </summary>
        public string RollBackColor
        {
            get { return this.rollBackColor; }
            set { this.rollBackColor = value; }
        }

        /// <summary>
        /// Gets or sets the roll color
        /// </summary>
        public string RollColor
        {
            get { return this.rollColor; }
            set { this.rollColor = value; }
        }

        /// <summary>
        /// Gets or sets the click border color
        /// </summary>
        public string ClickBorderColor
        {
            get { return this.clickBorderColor; }
            set { this.clickBorderColor = value; }
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
            HtmlElement e = web.Document.GetElementById(this.id);
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

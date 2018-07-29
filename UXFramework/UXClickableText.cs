using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UXFramework
{
    /// <summary>
    /// UX with a clickable text
    /// </summary>
    public class UXClickableText : UXControl
    {

        #region Fields

        /// <summary>
        /// Text to print
        /// </summary>
        private string text;
        /// <summary>
        /// Id
        /// </summary>
        private string id;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="t">static text</param>
        public UXClickableText(string t)
        {
            this.text = t;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the readonly text content
        /// </summary>
        public string Text
        {
            get { return this.text; }
            set { this.text = value; }
        }

        /// <summary>
        /// Gets or sets the Id object
        /// </summary>
        public string Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        #endregion

    }
}

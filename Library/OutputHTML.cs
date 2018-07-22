using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{

    /// <summary>
    /// Output html source
    /// </summary>
    public class OutputHTML
    {

        #region Fields

        private StringBuilder html = new StringBuilder();
        private StringBuilder css = new StringBuilder();
        private StringBuilder javascript = new StringBuilder();
        private StringBuilder javascriptOnLoad = new StringBuilder();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the HTML builder
        /// </summary>
        public StringBuilder HTML
        {
            get { return this.html; }
        }

        /// <summary>
        /// Gets the CSS builder
        /// </summary>
        public StringBuilder CSS
        {
            get { return this.css; }
        }

        /// <summary>
        /// Gets the JavaScript builder
        /// </summary>
        public StringBuilder JavaScript
        {
            get { return this.javascript; }
        }

        /// <summary>
        /// Gets the JavaScript On Load builder
        /// </summary>
        public StringBuilder JavaScriptOnLoad
        {
            get { return this.javascriptOnLoad; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Append additional CSS
        /// multiple CSS-style properties are overriden
        /// each last CSS-style property is prioritized for all objects
        /// </summary>
        /// <param name="cssAdditional">css to add</param>
        public void AppendCSS(List<CodeCSS> cssAdditional)
        {
            cssAdditional.ForEach(a => { this.CSS.Append(a.GenerateCSS(false, true, true) + Environment.NewLine); });
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UXFramework.BeamConnections;

namespace UXFramework
{
    /// <summary>
    /// UX with a clickable text
    /// </summary>
    public class UXClickableText : UXControl
    {

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UXClickableText()
        {
        }

        /// <summary>
        /// Creates elements
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="e">elements</param>
        public UXClickableText(string name, IDictionary<string, dynamic> e)
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
        #region Static Methods

        /// <summary>
        /// Create clickable text
        /// </summary>
        /// <param name="data">data to show</param>
        /// <param name="ui">ui properties</param>
        public static UXClickableText CreateUXClickableText(Marshalling.MarshallingHash data, Marshalling.MarshallingHash ui)
        {
            UXClickableText ux = new UXClickableText();
            ux.Bind(data);
            ux.Bind(ui);
            return ux;
        }

        /// <summary>
        /// Create UXClickableText
        /// </summary>
        /// <param name="f">function to enter data</param>
        /// <returns>marshalling</returns>
        public static UXClickableText CreateUXClickableText(string name, Func<IDictionary<string, dynamic>> f)
        {
            return new UXClickableText(name, f());
        }

        #endregion
    
    }
}

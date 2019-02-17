using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UXFramework
{
    public class UXReadOnlyText : UXControl
    {

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="t">static text</param>
        public UXReadOnlyText()
        {
        }

        /// <summary>
        /// Creates elements
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="e">elements</param>
        public UXReadOnlyText(string name, IDictionary<string, dynamic> e)
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
        /// Create read only text
        /// </summary>
        /// <param name="parent">parent ui</param>
        /// <param name="data">data to show</param>
        /// <param name="ui">ui properties</param>
        public static UXReadOnlyText CreateUXReadOnlyText(Marshalling.MarshallingHash data, Marshalling.MarshallingHash ui)
        {
            UXReadOnlyText ux = new UXReadOnlyText();
            ux.Bind(data);
            ux.Bind(ui);
            return ux;
        }

        #endregion

    }
}

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

        #region Fields

        private string text;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="t">static text</param>
        public UXReadOnlyText(string t)
        {
            this.text = t;
            this.Add(this.text);
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

        #endregion

        #region Static Methods

        public static void CreateUXReadOnlyText(UXControl parent, Marshalling.MarshallingHash data, Marshalling.MarshallingHash ui)
        {
            UXReadOnlyText ux = new UXReadOnlyText(data["Text"].Value);
            ux.Construct(data, ui);
            if (data["childs"].Value is Marshalling.MarshallingList)
                CreateUXControl(ux, data["childs"].Value, ui[data["name"].Value]);
            parent.Add(ux);
        }

        #endregion

    }
}

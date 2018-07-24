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

    }
}

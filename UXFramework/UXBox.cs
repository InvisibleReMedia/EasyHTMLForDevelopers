using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UXFramework
{
    public class UXBox : UXControl
    {

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UXBox()
        {
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Create a box
        /// </summary>
        /// <param name="data">data to show</param>
        /// <param name="ui">ui properties</param>
        public static UXBox CreateUXBox(Marshalling.MarshallingHash data, Marshalling.MarshallingHash ui)
        {
            UXBox box = new UXBox();
            box.Construct(data, ui);
            return box;
        }

        #endregion

    }
}

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

        /// <summary>
        /// Creates elements
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="e">elements</param>
        public UXBox(string name, IDictionary<string, dynamic> e)
            : base(name, e)
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
            box.Bind(data);
            box.Bind(ui);
            return box;
        }

        /// <summary>
        /// Create UXBox
        /// </summary>
        /// <param name="f">function to enter data</param>
        /// <returns>marshalling</returns>
        public static UXBox CreateUXBox(string name, Func<IDictionary<string, dynamic>> f)
        {
            return new UXBox(name, f());
        }

        #endregion

    }
}

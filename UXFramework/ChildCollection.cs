using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UXFramework
{
    /// <summary>
    /// Child collection
    /// </summary>
    public class ChildCollection : Marshalling.MarshallingList
    {

        #region Constructor

        public ChildCollection(string name, IEnumerable<dynamic> r) : base(name, r) { }

        #endregion

        #region Properties

        /// <summary>
        /// Gets Values
        /// </summary>
        public new IEnumerable<IUXObject> Values
        {
            get { return this.Values; }
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Create ChildCollection
        /// </summary>
        /// <param name="f">function to enter data</param>
        /// <returns>marshalling</returns>
        public static ChildCollection CreateChildCollection(string name, Func<IEnumerable<dynamic>> f)
        {
            return new ChildCollection(name, f());
        }

        #endregion

    }
}

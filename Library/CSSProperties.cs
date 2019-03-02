using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    /// <summary>
    /// Contains a list of properties
    /// </summary>
    [Serializable]
    public class CSSProperties : Marshalling.PersistentDataObject
    {

        #region Fields

        /// <summary>
        /// Index name for css list
        /// </summary>
        public static readonly string idName = "id";

        #endregion

        #region Constructor

        /// <summary>
        /// Create id
        /// </summary>
        /// <param name="prefix">#, . ou nothing</param>
        /// <param name="value">value</param>
        public CSSProperties(string prefix, string value)
        {
            this.Set(idName, prefix + value);
        }

        #endregion

    }
}

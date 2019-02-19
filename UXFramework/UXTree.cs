using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UXFramework
{
    public class UXTree : UXTable
    {

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public UXTree()
        {
        }

        /// <summary>
        /// Creates elements
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="e">elements</param>
        public UXTree(string name, IDictionary<string, dynamic> e)
            : base(name, e)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if this item is root
        /// </summary>
        public bool IsRoot
        {
            get
            {
                if (this.Exists("IsRoot"))
                    return this.GetProperty("IsRoot").Value;
                else
                    return false;
            }
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Create editable text
        /// </summary>
        /// <param name="data">data to show</param>
        /// <param name="ui">ui properties</param>
        public static UXTree CreateUXTree(Marshalling.MarshallingHash data, Marshalling.MarshallingHash ui)
        {
            UXTree tree = new UXTree();
            tree.Bind(data);
            tree.Bind(ui);
            return tree;
        }

        /// <summary>
        /// Create UXTree
        /// </summary>
        /// <param name="f">function to enter data</param>
        /// <returns>marshalling</returns>
        public static UXTree CreateUXTree(string name, Func<IDictionary<string, dynamic>> f)
        {
            return new UXTree(name, f());
        }

        #endregion
    }
}

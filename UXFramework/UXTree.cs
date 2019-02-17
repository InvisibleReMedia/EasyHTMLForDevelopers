using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UXFramework
{
    public class UXTree : UXControl
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
            foreach (Marshalling.IMarshalling m in tree.GetProperty("childs").Values)
            {
                tree.Add(m.Value);
            }
            return tree;
        }

        #endregion
    }
}

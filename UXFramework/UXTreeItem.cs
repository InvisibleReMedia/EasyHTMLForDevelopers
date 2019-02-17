using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UXFramework
{
    /// <summary>
    /// An item in a tree
    /// </summary>
    public class UXTreeItem : UXControl
    {

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UXTreeItem()
        {
        }

        /// <summary>
        /// Creates elements
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="e">elements</param>
        public UXTreeItem(string name, IDictionary<string, dynamic> e)
            : base(name, e)
        {
        }

        #endregion

        #region Properties

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
        /// Create editable text
        /// </summary>
        /// <param name="data">data to show</param>
        /// <param name="ui">ui properties</param>
        public static UXTreeItem CreateUXTreeItem(Marshalling.MarshallingHash data, Marshalling.MarshallingHash ui)
        {
            UXTreeItem treeItem = new UXTreeItem();
            treeItem.Bind(data);
            treeItem.Bind(ui);
            foreach (Marshalling.IMarshalling m in treeItem.GetProperty("childs").Values)
            {
                treeItem.Add(m.Value);
            }
            return treeItem;
        }

        #endregion

    }
}

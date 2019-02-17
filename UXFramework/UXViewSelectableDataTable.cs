using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UXFramework
{
    /// <summary>
    /// Data table selectable
    /// </summary>
    public class UXViewSelectableDataTable : UXViewDataTable
    {

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public UXViewSelectableDataTable()
        {
        }

        /// <summary>
        /// Creates elements
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="e">elements</param>
        public UXViewSelectableDataTable(string name, IDictionary<string, dynamic> e)
            : base(name, e)
        {
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Create view data table
        /// </summary>
        /// <param name="data">data to show</param>
        /// <param name="ui">ui properties</param>
        public static UXViewSelectableDataTable CreateUXViewSelectableDataTable(Marshalling.MarshallingHash data, Marshalling.MarshallingHash ui)
        {
            UXViewSelectableDataTable table = new UXViewSelectableDataTable();
            table.Bind(data);
            table.Bind(ui);
            foreach (Marshalling.IMarshalling m in table.GetProperty("childs").Values)
            {
                table.Add(m.Value);
            }
            return table;
        }

        #endregion
    }
}

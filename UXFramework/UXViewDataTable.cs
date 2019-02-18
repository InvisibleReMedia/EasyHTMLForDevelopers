using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UXFramework
{
    /// <summary>
    /// View for a data table
    /// </summary>
    public class UXViewDataTable : UXTable
    {

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public UXViewDataTable()
        {
        }

        /// <summary>
        /// Creates elements
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="e">elements</param>
        public UXViewDataTable(string name, IDictionary<string, dynamic> e)
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
        public static UXViewDataTable CreateUXViewDataTable(Marshalling.MarshallingHash data, Marshalling.MarshallingHash ui)
        {
            UXViewDataTable table = new UXViewDataTable();
            table.Bind(data);
            table.Bind(ui);
            foreach (Marshalling.IMarshalling m in table.GetProperty("childs").Values)
            {
                table.Add(m.Value);
            }
            return table;
        }

        /// <summary>
        /// Create UXViewDataTable
        /// </summary>
        /// <param name="f">function to enter data</param>
        /// <returns>marshalling</returns>
        public static UXViewDataTable CreateUXViewDataTable(string name, Func<IDictionary<string, dynamic>> f)
        {
            return new UXViewDataTable(name, f());
        }

        #endregion
    
    }
}

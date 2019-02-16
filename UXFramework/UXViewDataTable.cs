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
        /// <param name="columnSize">column size</param>
        /// <param name="lineSize">line size</param>
        /// <param name="id">id</param>
        public UXViewDataTable(uint columnSize, uint lineSize, string id) : base(columnSize, lineSize, id)
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
            UXViewDataTable ux = new UXViewDataTable(data["ColumnCount"].Value, data["LineCount"].Value, data["id"].Value);
            ux.Construct(data, ui);
            return ux;
        }

        #endregion
    
    }
}

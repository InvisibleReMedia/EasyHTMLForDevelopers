using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UXFramework
{
    /// <summary>
    /// Bring a row in a table
    /// </summary>
    public class UXRow : UXControl
    {

        #region Fields

        /// <summary>
        /// Row index
        /// </summary>
        private static readonly string rowIndexName = "rowIndex";
        /// <summary>
        /// Column count
        /// </summary>
        private static readonly string columnCountName = "columnCount";


        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public UXRow(uint rowIndex, uint columnCount, string id)
        {
            this.Set(rowIndexName, rowIndex);
            this.Set(columnCountName, columnCount);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the row index
        /// </summary>
        public uint RowIndex
        {
            get { return this.Get(rowIndexName, 0); }
            set { this.Set(rowIndexName, value);  }
        }

        /// <summary>
        /// Gets the column count
        /// </summary>
        public uint ColumnCount
        {
            get { return this.Get(columnCountName, 0); }
            set { this.Set(columnCountName, value); }
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Create UXRow
        /// </summary>
        /// <param name="data">data hash</param>
        /// <param name="ui">ui properties</param>
        /// <returns>row</returns>
        public static UXRow CreateUXRow(Marshalling.MarshallingHash data, Marshalling.MarshallingHash ui)
        {
            UXRow row = new UXRow(data["RowIndex"].Value, data["ColumnCount"].Value, data["Id"].Value);
            row.Construct(data, ui[data["Id"].Value]);
            Marshalling.MarshallingList cells = data["Childs"].Value;
            foreach (IUXObject obj in cells.Values)
            {
                row.Add(obj);
            }
            return row;
        }

        #endregion

    }
}

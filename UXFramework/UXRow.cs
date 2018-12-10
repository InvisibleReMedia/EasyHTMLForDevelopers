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
        public UXRow(uint rowIndex, uint columnCount)
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

        #region Methods

        /// <summary>
        /// Bind sequence
        /// </summary>
        /// <param name="p">marshalling object</param>
        public virtual void Bind(Marshalling.IMarshalling p)
        {
            // construction de la ligne
            uint columnIndex = 0;
            if (p is Marshalling.MarshallingHash)
            {
                Marshalling.MarshallingHash hash = p as Marshalling.MarshallingHash;
                foreach (Marshalling.IMarshalling m in hash.Values)
                {
                    // construction d'une nouvelle case
                    UXCell cell = new UXCell(RowIndex, columnIndex);
                    this.Children.Add(cell);
                    if (m is IUXObject)
                    {
                        cell.Children.Add(m as IUXObject);
                    }
                    else
                    {
                        UXReadOnlyText t = new UXReadOnlyText(m.Value.ToString());
                        cell.Children.Add(t);
                    }
                    ++columnIndex;
                }
            }
            else if (p is Marshalling.MarshallingList)
            {
                Marshalling.MarshallingList list = p as Marshalling.MarshallingList;
                for (int index = 0; index < list.Count; ++index)
                {
                    // construction d'une nouvelle case
                    UXCell cell = new UXCell(RowIndex, columnIndex);
                    this.Children.Add(cell);
                    Marshalling.IMarshalling m = list[index];
                    if (m is IUXObject)
                    {
                        cell.Children.Add(m as IUXObject);
                    }
                    else
                    {
                        UXReadOnlyText t = new UXReadOnlyText(m.Value.ToString());
                        cell.Children.Add(t);
                    }
                    ++columnIndex;

                }
            }
            else if (p is IUXObject)
            {
                // construction d'une nouvelle case
                UXCell cell = new UXCell(RowIndex, 0);
                this.Children.Add(cell);
                cell.Children.Add(p as IUXObject);
            }
        }

        #endregion

    }
}
